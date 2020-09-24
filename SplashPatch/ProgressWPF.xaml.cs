using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Configuration;
using System.Collections.Specialized;

namespace SplashPatch
{
    public partial class ProgressWPF : Window
    {
        #region initialize
        public ProgressWPF()
        {
            InitializeComponent();
        }
        #endregion

        #region no icon/close
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private void Loaded_Form(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        #endregion

        #region timer (load UI on UI thread)
        bool change = false;

        int closing = 0;

        void timer_Tick(object sender, EventArgs e)
        {
            //only if actually changed
            if (change)
            {
                //do we show the progress box?
                if (Wizard.progressData.show)
                {
                    //set UI
                    Title = Wizard.progressData.title;
                    label.Text = Wizard.progressData.label;
                    Topmost = !Wizard.progressData.dialog;
                    progressBar.IsIndeterminate = true;
                    progressBar.Value = 0;
                    
                    change = false;
                }
                else
                {
                    //do some text stuff
                    Title = Wizard.progressData.title;
                    label.Text = Wizard.progressData.label;
                    Topmost = !Wizard.progressData.dialog;
                    progressBar.IsIndeterminate = false;
                    progressBar.Value = 100;
                    if (closing > 50)
                    {
                        change = false;
                        Close();
                    } else
                    {
                        closing++;
                        //Title = "DEBUG: closing in " + (50 - closing).ToString();
                        change = true;
                    }
                }
            }
        }
        #endregion

        #region HANDLE changes
        public void ChangeStuff(object sender, EventArgs e)
        {
            change = true;
        }
        #endregion
    }
}
