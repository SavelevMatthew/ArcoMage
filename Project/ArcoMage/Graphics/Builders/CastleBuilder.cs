using System.Drawing;
using System.Windows.Forms;
using ArcoMage.Properties;

namespace ArcoMage.Graphics.Builders
{
    public static class CastleBuilder
    {
        public static PictureBox BuildTower(int height, AnchorStyles anchors)
        {
            return CreateBuilding(Resources.tower, height, anchors);
        }

        public static PictureBox BuildWall(int height, AnchorStyles anchors)
        {
            return CreateBuilding(Resources.wall, height, anchors);
        }

        public static PictureBox CreateBuilding(Image img, int height, AnchorStyles anchors)
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
    }
}
