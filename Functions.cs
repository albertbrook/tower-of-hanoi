using System.Drawing;
using System.Windows.Forms;

namespace TowerOfHanoi
{
    internal class Functions
    {
        private static Functions functions;

        private readonly Picture picture;
        private readonly Rectangle[] pillars;
        private readonly Rectangle[] blocks;
        private readonly int[] indexs;

        private bool move = false;
        private int moveIndex;
        private Point origin;
        private Point gap;

        private Functions()
        {
            picture = Picture.GetPicture();
            pillars = picture.GetPillars();
            blocks = picture.GetBlocks();
            indexs = picture.GetIndexs();
            picture.MouseDown += new MouseEventHandler(Picture_MouseDown);
            picture.MouseUp += new MouseEventHandler(Picture_MouseUp);
            picture.MouseMove += new MouseEventHandler(Picture_MouseMove);
        }

        internal static Functions GetFunctions()
        {
            if (functions == null)
                functions = new Functions();
            return functions;
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < blocks.Length; i++)
                if (blocks[i].Contains(e.Location))
                {
                    move = true;
                    moveIndex = i;
                    origin = blocks[moveIndex].Location;
                    gap = new Point(origin.X - e.X, origin.Y - e.Y);
                    return;
                }
        }

        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
            int pillarIndex = GetPillarIndex();
            if (pillarIndex != -1 && IsFristBlock() && IsCanMove(pillarIndex))
            {
                indexs[moveIndex] = pillarIndex;
                picture.ReBlockLocations();
                return;
            }
            blocks[moveIndex].Location = origin;
        }

        private int GetPillarIndex()
        {
            Rectangle move = blocks[moveIndex];
            for (int i = 0; i < pillars.Length; i++)
                if (move.X + move.Width > pillars[i].X && move.X < pillars[i].X + pillars[i].Width)
                    return i;
            return -1;
        }

        private bool IsFristBlock()
        {
            for (int i = 0; i < indexs.Length; i++)
                if (indexs[i] == indexs[moveIndex])
                {
                    if (i == moveIndex)
                        return true;
                    break;
                }
            return false;
        }

        private bool IsCanMove(int pillarIndex)
        {
            for (int i = 0; i < indexs.Length; i++)
                if (indexs[i] == pillarIndex && moveIndex > i)
                    return false;
            return true;
        }

        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                blocks[moveIndex].Location = new Point(e.X + gap.X, e.Y + gap.Y);
                int pillarIndex = GetPillarIndex();
                if (pillarIndex != indexs[moveIndex] && IsFristBlock() && IsCanMove(pillarIndex))
                    picture.SetGrayIndex(pillarIndex);
            }
        }
    }
}
