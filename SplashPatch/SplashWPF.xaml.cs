using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
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
    public partial class SplashWPF : Window
    {
        string words = "Open source software by TheOneCode" + Environment.NewLine + Environment.NewLine + "Artwork of András Arató. For more details and legal notices, go to the About SplashPatch screen." + Environment.NewLine + Environment.NewLine;

        #region initialize
        public SplashWPF()
        {
            InitializeComponent();
            SplashImage.Source = ImageSourceFromBitmap(Properties.Resources.SPLASH);
            SplashImage.Stretch = Stretch.Fill;
            //SplashLabel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 0, 177, 64));
            ChangeWords("null");
            
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }               
        }
        #endregion
        public void ChangeWords(string trail)
        {
            SplashLabel.Text = words + trail;
        }
    }
}
