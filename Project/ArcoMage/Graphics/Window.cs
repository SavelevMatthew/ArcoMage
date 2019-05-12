using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework.Api;

namespace ArcoMage.Graphics
{
    class Window : Form
    {
        private const AnchorStyles LeftBottom = AnchorStyles.Left | AnchorStyles.Bottom;
        private const AnchorStyles RightBottom = AnchorStyles.Right | AnchorStyles.Bottom;
        public Window(Game game)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent(game);
        }

        private void InitializeComponent(Game game)
        {
            SuspendLayout();
            BackColor = Color.FromArgb(128, 207, 17);
            ClientSize = new System.Drawing.Size(860, 500);
            Name = "Arcomage ver 1.0";
            var window = new TableLayoutPanel()
            {
                Height = ClientSize.Height,
                Width = ClientSize.Width,
                BackgroundImage = Properties.Resources.BG,
                BackgroundImageLayout = ImageLayout.Stretch,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };
            var height = (int) (window.Height * 0.85);
            var castle1 = CreateBuilding(Properties.Resources.tower, height, LeftBottom);
            var wall1 = CreateBuilding(Properties.Resources.wall, height, LeftBottom);
            var castle2 = CreateBuilding(Properties.Resources.tower, height, RightBottom);
            var wall2 = CreateBuilding(Properties.Resources.wall, height, RightBottom);
            
            for (var i = 0; i < 8; i++)
                window.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4f));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 3));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 7));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            window.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            // Castles building
            window.Controls.Add(castle2, 6, 0);
            window.Controls.Add(castle1, 1, 0);
            window.Controls.Add(wall1, 2, 0);
            window.Controls.Add(wall2, 5, 0);
            // HP buildings
            window.Controls.Add(DrawHealthBar(game.TowerHealth,
                game.Player1.Castle.TowerHealth), 1, 1);
            window.Controls.Add(DrawHealthBar(game.TowerHealth,
                game.Player2.Castle.TowerHealth, true), 6, 1);
            window.Controls.Add(DrawHealthLabel(game.Player1.Castle.TowerHealth), 1, 2);
            window.Controls.Add(DrawHealthLabel(game.Player2.Castle.TowerHealth), 6, 2);

            window.Controls.Add(DrawHealthBar(game.WallHealth,
                game.Player1.Castle.WallHealth), 2, 1);
            window.Controls.Add(DrawHealthBar(game.WallHealth,
                game.Player2.Castle.WallHealth, true), 5, 1);
            window.Controls.Add(DrawHealthLabel(game.Player1.Castle.WallHealth), 2, 2);
            window.Controls.Add(DrawHealthLabel(game.Player2.Castle.WallHealth), 5, 2);
            // Resources building
            window.Controls.Add(DrawResources(game.Player1.Resources, ContentAlignment.TopLeft),
                0,0);
            window.Controls.Add(DrawResources(game.Player1.Resources, ContentAlignment.TopRight),
                7, 0);
            // Drawing Deck
            DrawDeck(game.Player1, window);



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

        public ProgressBar DrawHealthBar(int maxHealth, int currentHealth, bool reversed = false)
        {
            return new ProgressBar
            {
                Dock = DockStyle.Fill,
                Style = ProgressBarStyle.Continuous,
                BackColor = Color.Brown,
                ForeColor = Color.Green,
                Maximum = maxHealth,
                Value = currentHealth,
                RightToLeft = reversed ? RightToLeft.Yes : RightToLeft.No,
                RightToLeftLayout = reversed
            };
        }

        public Label DrawHealthLabel(int health)
        {
            return new Label
            {
                Text = health.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
                Dock = DockStyle.Fill
            };
        }

        public TableLayoutPanel DrawResources(Dictionary<string, Resource> res, ContentAlignment align)
        {
            return CreateLabelList(res
                .Select(e => $"{e.Key}:\n{e.Value.Count} (+{e.Value.Source})")
                .ToList(), align, new Font("Arial", 12, FontStyle.Bold));
        }

        public TableLayoutPanel CreateLabelList(List<string> content, ContentAlignment align, Font font)
        {
            var result = new TableLayoutPanel
            {
                BackColor = GetRandomColor(),
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };
            var counter = 0;
            foreach (var e in content)
            {
                result.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / content.Count));
                result.Controls.Add(new Label
                {
                    Text = e,
                    TextAlign = align,
                    Font = font,
                    Dock = DockStyle.Fill
                }, 0, counter);
                counter++;
            }

            return result;
        }

        public void DrawDeck(Player player, TableLayoutPanel window)
        {
            for (var i = 1; i < 7; i++)
            {
                var card = new TableLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    BackColor = player.Cursor == i - 1 ? Color.Red : Color.Transparent,
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };
                card.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                card.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                card.Controls.Add(new Label
                {
                    Text = player.Deck[i - 1].Description,
                    Dock = DockStyle.Fill,
                    BackColor = GetRandomColor(),
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    MaximumSize = new Size(ClientSize.Width / 8 - 10,0),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(5,5,5,0)
                }, 0, 0);
                var costsFormat = player.Deck[i - 1].Cost
                    .Select(e => $"{e.Key}: {e.Value}")
                    .ToList();
                var costs = CreateLabelList(costsFormat, ContentAlignment.MiddleLeft,
                    new Font("Arial", 8, FontStyle.Bold));
                costs.Padding = new Padding(0);
                costs.Margin = new Padding(5, 0, 5, 5);
                costs.MaximumSize = new Size(ClientSize.Width / 8 - 10, 0);
                card.Controls.Add(costs, 0, 1);

                window.Controls.Add(card, i, 4);
            }
        }

        public Color GetRandomColor()
        {
            var rnd = new Random();
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
}
