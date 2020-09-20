using System;
using System.Collections.Generic;
using System.Text;

namespace SplashPatchEngine
{
    #region Audition specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class AU
    {
        #region set DEFAULT STRINGS
        public static string directory = "Adobe Audition *";
        public static string[] files = { "\\auui.dll" };
        public static string[] masks = { "AU_CC*_SPLASH", "AU_CC*_SPLASH_AT_3TO2X", "AU_CC*_SPLASH_AT_2X" };
        #endregion
        //The rest is all common
    }
    #endregion
}
