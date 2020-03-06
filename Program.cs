using System.ComponentModel;
using System.Windows.Forms;

namespace TowerOfHanoi
{
    public class Program : Form
    {
        private readonly IContainer components = null;

        private Program()
        {
            Text = "Tower Of Hanoi - AlbertBrook";
            ClientSize = Settings.ScreenSize;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Settings.ScreenColor;

            Controls.Add(Picture.GetPicture());

            Functions.GetFunctions();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }
    }
}
