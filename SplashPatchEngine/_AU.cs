using System;
using System.Collections.Generic;
using System.Text;

namespace SplashPatchEngine
{
    #region Audition specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class AU
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("Audition", "Adobe Audition", "AU");
        public static string helpText = string.Format("Requires dll patch of 3 Images in \"auui.dll\"{0}AU_CC2020_SPLASH.PNG (750x500){0}AU_CC2020_SPLASH_AT_3TO2X.PNG (1125x750){0}AU_CC2020_SPLASH_AT_2X.PNG (1500x1000){0}Dimensions MUST match and images MUST be PNG format", Environment.NewLine);
        public static string directory = string.Format("{0} *", name.full);
        public static string[] files = { "\\auui.dll" };
        public static string[] masks = { "AU_CC*_SPLASH", "AU_CC*_SPLASH_AT_3TO2X", "AU_CC*_SPLASH_AT_2X" };
        #endregion
        //The rest is all common
    }
    #endregion
}
