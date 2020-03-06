using System.Drawing;

namespace TowerOfHanoi
{
    internal class Settings
    {
        internal static Size ScreenSize { get; set; }
        internal static Color ScreenColor { get; set; }

        internal static Size PillarSize { get; set; }
        internal static Color PillarColor { get; set; }

        internal static int BlockCount { get; set; }
        internal static Color BlockColor { get; set; }
        internal static int[] BlockWidths { get; set; }
        internal static int BlockHeight { get; set; }

        static Settings()
        {
            ScreenSize = new Size(1000, 600);
            ScreenColor = Color.Black;
            PillarSize = new Size(30, 500);
            PillarColor = Color.White;
            BlockCount = 5;
            BlockColor = Color.White;
            BlockWidths = SetBlockWidths();
            BlockHeight = 50;
        }

        private Settings() { }

        private static int[] SetBlockWidths()
        {
            int[] blockWidths = new int[BlockCount];
            for (int i = 0; i < BlockCount; i++)
                blockWidths[i] = PillarSize.Width + 40 * (i + 1);
            return blockWidths;
        }
    }
}
