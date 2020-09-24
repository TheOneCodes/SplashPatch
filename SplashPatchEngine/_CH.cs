using System;
using System.Collections.Generic;
using System.Text;

namespace SplashPatchEngine
{
    #region Character Animator specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class CH
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("Character Animator", "Adobe Character Animator", "CH");
        public static string helpText = string.Format("Requires exe patch of 3 Images in \"character animator.exe\"{0}CH_CC2020_SPLASH.PNG (750x500){0}CH_CC2020_SPLASH_AT_3TO2X.PNG (1125x750){0}CH_CC2020_SPLASH_AT_2X.PNG (1500x1000){0}Dimensions MUST match and images MUST be PNG format", Environment.NewLine);
        public static string directory = string.Format("{0} *", name.full);
        public static string[] files = { "\\Support Files\\character animator.exe" };
        public static string[] masks = { "CH_CC*_SPLASH", "CH_CC*_SPLASH_AT_3TO2X", "CH_CC*_SPLASH_AT_2X" };
        #endregion

        //The rest is all common
    }
    #endregion
}
