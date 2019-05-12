using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ArcoMage.Graphics
{
    class Window : Form
    {
        private readonly Color _player1Color = Color.CadetBlue;
        private readonly Color _player2Color = Color.Brown;
        private readonly Dictionary<string, string> _moveKeys = new Dictionary<string, string>
        {
            ["Left"] = "a",
            ["Right"] = "d",
            ["Enter"] = "\r",
            ["Space"] = " "
        };
        private readonly Game _game;
        private const AnchorStyles LeftBottom = AnchorStyles.Left | AnchorStyles.Bottom;
        private const AnchorStyles RightBottom = AnchorStyles.Right | AnchorStyles.Bottom;
        public Window(Game game)
        {
            _game = game;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeParams();
            DrawForm();
            game.Status = Game.Condition.InGame;
        }

        public void InitializeParams()
        {
            BackColor = Color.FromArgb(128, 207, 17);
            ClientSize = new Size(860, 500);
            Name = "Arcomage ver 1.0";
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            var key = e.KeyChar.ToString().ToLower();
            if (_moveKeys.ContainsValue(key))
            {
                if (key == _moveKeys["Left"])
                {
                    _game.CurrentPlayer.CursorLeft();
                }
                else if (key == _moveKeys["Right"])
                {
                    _game.CurrentPlayer.CursorRight();
                }
                else if (key == " " || _game.CurrentPlayer.Deck[_game.CurrentPlayer.Cursor].CanBeDropped(_game.CurrentPlayer))
                {
                    var card = (key == _moveKeys["Enter"])
                        ? _game.CurrentPlayer.DropCard()
                        : _game.CurrentPlayer.DestroyCard();
                    _game.CurrentPlayer.TakeResources(card.Cost);
                    card.Drop()(_game.CurrentPlayer, _game.GetOpponent());
                    _game.UpdateResources();
                    _game.SwapPlayers();
                }
                UpdateForm();
            }
        }

        private void UpdateForm()
        {
            var c = Controls[0];
            c.Controls["tower1"].Text = _game.Player1.Castle.TowerHealth.ToString();
            c.Controls["tower2"].Text = _game.Player2.Castle.TowerHealth.ToString();
            c.Controls["wall1"].Text = _game.Player1.Castle.WallHealth.ToString();
            c.Controls["wall2"].Text = _game.Player2.Castle.WallHealth.ToString();
            foreach (var res in _game.Player1.Resources)
            {
                var format = $"{res.Key}:\n{res.Value.Count} (+{res.Value.Source})";
                c.Controls["res1"].Controls[res.Key].Text = format;
            }
            foreach (var res in _game.Player2.Resources)
            {
                var format = $"{res.Key}:\n{res.Value.Count} (+{res.Value.Source})";
                c.Controls["res2"].Controls[res.Key].Text = format;
            }

            for (var i = 1; i < 7; i++)
            {
                c.Controls["card" + i].BackColor = (_game.CurrentPlayer.Cursor == i - 1)
                    ? (_game.CurrentPlayer == _game.Player1) ? _player1Color : _player2Color
                    : Color.Transparent;
                c.Controls["card" + i].Controls["name"].Text = _game.CurrentPlayer.Deck[i - 1].Description;
                c.Controls["card" + i].Controls["name"].BackColor = _game.CurrentPlayer.Deck[i - 1].Color;
                foreach (var cost in _game.CurrentPlayer.Deck[i - 1].Cost)
                {
                    var format = $"{cost.Key}: {cost.Value}";
                    c.Controls["card" + i].Controls["costs"].Controls[cost.Key].Text = format;
                }
            }

            c.Controls["card" + (_game.CurrentPlayer.Cursor + 1)].BackColor =
                (_game.CurrentPlayer == _game.Player1) ? _player1Color : _player2Color;
        }

        private void DrawForm()
        {
            Controls.Clear();
            SuspendLayout();
            var window = new TableLayoutPanel()
            {
                Height = ClientSize.Height,
                Width = ClientSize.Width,
                BackgroundImage = Properties.Resources.BG,
                BackgroundImageLayout = ImageLayout.Stretch,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };
            var height = (int)(window.Height * 0.85);
            var castle1 = CreateBuilding(Properties.Resources.tower, height, LeftBottom);
            var wall1 = CreateBuilding(Properties.Resources.wall, height, LeftBottom);
            var castle2 = CreateBuilding(Properties.Resources.tower, height, RightBottom);
            var wall2 = CreateBuilding(Properties.Resources.wall, height, RightBottom);

            for (var i = 0; i < 8; i++)
                window.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4f));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 58));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 7));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            // Castles building
            window.Controls.Add(castle2, 6, 0);
            window.Controls.Add(castle1, 1, 0);
            window.Controls.Add(wall1, 2, 0);
            window.Controls.Add(wall2, 5, 0);
            // HP buildings
            window.Controls.Add(DrawHealthLabel("tower1", _game.Player1.Castle.TowerHealth), 1, 1);
            window.Controls.Add(DrawHealthLabel("tower2", _game.Player2.Castle.TowerHealth), 6, 1);
            window.Controls.Add(DrawHealthLabel("wall1", _game.Player1.Castle.WallHealth), 2, 1);
            window.Controls.Add(DrawHealthLabel("wall2", _game.Player2.Castle.WallHealth), 5, 1);
            // Resources building
            window.Controls.Add(DrawResources("res1", _game.Player1.Resources, ContentAlignment.TopLeft, _player1Color),
                0, 0);
            window.Controls.Add(DrawResources("res2", _game.Player1.Resources, ContentAlignment.TopRight, _player2Color),
                7, 0);
            // Drawing Deck
            DrawDeck(_game.CurrentPlayer, window, (_game.CurrentPlayer == _game.Player1) ? _player1Color : _player2Color);



            Controls.Add(window);
            ResumeLayout(false);

        }

        public PictureBox CreateBuilding(Image img, int height, AnchorStyles anchors)
        {
            return new PictureBox
            {
                Image = img,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Anchor = anchors,
                Height = height,
            };
        }

        public Label DrawHealthLabel(string name, int health)
        {
            return new Label
            {
                Name = name,
                Text = health.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
                Dock = DockStyle.Fill
            };
        }

        public TableLayoutPanel DrawResources(string name, Dictionary<string, Resource> res, ContentAlignment align, Color bg)
        {
            return CreateLabelList(name, res
                .Select(e => Tuple.Create(e.Key, $"{e.Key}:\n{e.Value.Count} (+{e.Value.Source})"))
                .ToList(), align, new Font("Arial", 12, FontStyle.Bold), bg);
        }

        public TableLayoutPanel CreateLabelList(string name, List<Tuple<string, string>> content, ContentAlignment align, Font font, Color bg)
        {
            var result = new TableLayoutPanel
            {
                Name = name,
                BackColor = bg,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };
            var counter = 0;
            var h = 100 / content.Count;
            foreach (var e in content)
            {
                result.RowStyles.Add(new RowStyle(SizeType.Percent, h));
                result.Controls.Add(new Label
                {
                    Name = e.Item1,
                    Text = e.Item2,
                    TextAlign = align,
                    Font = font,
                    Dock = DockStyle.Fill
                }, 0, counter);
                counter++;
            }

            return result;
        }

        public void DrawDeck(Player player, TableLayoutPanel window, Color playerColor)
        {
            for (var i = 1; i < 7; i++)
            {
                var card = new TableLayoutPanel()
                {
                    Name = "card" + i,
                    Dock = DockStyle.Fill,
                    BackColor = player.Cursor == i - 1 ? playerColor : Color.Transparent,
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };
                card.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                card.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                card.Controls.Add(new Label
                {
                    Name = "name",
                    Text = player.Deck[i - 1].Description,
                    Dock = DockStyle.Fill,
                    BackColor = _game.CurrentPlayer.Deck[i - 1].Color,
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    MaximumSize = new Size(ClientSize.Width / 8 - 10, 0),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(5, 5, 5, 0)
                }, 0, 0);

                var costsFormat = player.Deck[i - 1].Cost
                    .Select(e => Tuple.Create(e.Key, $"{e.Key}: {e.Value}"))
                    .ToList();
                var costs = CreateLabelList("costs", costsFormat, ContentAlignment.MiddleLeft,
                    new Font("Arial", 8, FontStyle.Bold), _game.CurrentPlayer.Deck[i - 1].Color);
                costs.Padding = new Padding(0);
                costs.Margin = new Padding(5, 0, 5, 5);
                costs.MaximumSize = new Size(ClientSize.Width / 8 - 10, 0);
                card.Controls.Add(costs, 0, 1);

                window.Controls.Add(card, i, 3);
            }
        }

        public static Color GetRandomColor()
        {
            var rnd = new Random();
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
}
