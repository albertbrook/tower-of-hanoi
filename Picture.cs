using System.Drawing;
using System.Windows.Forms;

namespace TowerOfHanoi
{
    internal class Picture : PictureBox
    {
        private static Picture picture;

        private readonly Rectangle[] pillars = new Rectangle[3];
        private readonly Rectangle[] blocks = new Rectangle[Settings.BlockCount];
        private readonly int[] indexs = new int[Settings.BlockCount];

        private Picture()
        {
            Size = Settings.ScreenSize;
            Paint += new PaintEventHandler(Picture_Paint);
            CreateShape();
        }

        internal static Picture GetPicture()
        {
            if (picture == null)
                picture = new Picture();
            return picture;
        }

        internal Rectangle[] GetPillars() { return pillars; }

        internal Rectangle[] GetBlocks() { return blocks; }

        internal int[] GetIndexs() { return indexs; }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(Settings.ScreenSize.Width, Settings.ScreenSize.Height);
            Graphics g = Graphics.FromImage(bitmap);

            SolidBrush b = new SolidBrush(Settings.PillarColor);
            foreach (Rectangle pillar in pillars)
                g.FillRectangle(b, pillar);

            b.Color = Settings.BlockColor;
            foreach (Rectangle block in blocks)
                g.FillRectangle(b, block);

            g.Dispose();
            Image = bitmap;
        }

        private void CreateShape()
        {
            for (int i = 0; i < pillars.Length; i++)
            {
                int x = Settings.ScreenSize.Width / (pillars.Length + 1) * (i + 1) - (Settings.PillarSize.Width >> 1);
                int y = Settings.ScreenSize.Height - Settings.PillarSize.Height;
                pillars[i].Location = new Point(x, y);
                pillars[i].Size = Settings.PillarSize;
            }
            for (int i = 0; i < blocks.Length; i++)
                blocks[i].Size = new Size(Settings.BlockWidths[i], Settings.BlockHeight);
            ReBlockLocations();
        }

        internal void ReBlockLocations()
        {
            int[] sinkCount = new int[pillars.Length];
            foreach (int index in indexs)
                sinkCount[index]++;
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].X = pillars[indexs[i]].X + ((pillars[indexs[i]].Width - Settings.BlockWidths[i]) >> 1);
                blocks[i].Y = Settings.ScreenSize.Height - Settings.BlockHeight * sinkCount[indexs[i]]--;
            }
        }
    }
}
