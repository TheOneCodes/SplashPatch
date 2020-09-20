using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;

namespace SplashPatch
{
    public partial class Splash : Form
    {
        string words = "Open source software by TheOneCode" + Environment.NewLine + Environment.NewLine + "Artwork of András Arató. For more details and legal notices, go to the About SplashPatch screen." + Environment.NewLine + Environment.NewLine;
        Color text = Color.FromArgb(255, 142, 142, 142);
        Color background = Color.FromArgb(255, 44, 47, 51);

        public Splash()
        {
            InitializeComponent();
            SplashLabel.BackColor = background;
            SplashLabel.ForeColor = text;
            BackgroundImage = Properties.Resources.SPLASH;
        }

        public void ChangeWords(string trail)
        {
            SplashLabel.Text = words + trail;
        }

        private void textChange(object sender, EventArgs e)
        {
            ChangeWords(Text);
        }

        private void swerliesStart(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        private void swerliesEnd(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }
    }
}
