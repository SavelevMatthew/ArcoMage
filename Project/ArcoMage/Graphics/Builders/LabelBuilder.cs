using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArcoMage.Graphics.Builders
{
    public static class LabelBuilder
    {
        public static Label CreateHealthBar(string name, int health)
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

        public static TableLayoutPanel CreateLabelList(string name, List<Tuple<string, string>> content, ContentAlignment align, Font font, Color bg)
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
    }
}
