using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace SplashPatch
{
    public partial class Progress : Form
    {
        //private string currentEvent;

        public Progress()
        {
            InitializeComponent();
        }

        private void WaitASec(object sender, FormClosingEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}
