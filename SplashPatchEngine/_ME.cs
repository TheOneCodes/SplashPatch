using System;
using System.Collections.Generic;
using System.Text;

namespace SplashPatchEngine
{
    #region Media Encoder specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class ME
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("Media Encoder", "Adobe Media Encoder", "ME");
        public static string helpText = string.Format("Requires exe patch of 3 Images in \"Adobe Media Encoder.exe\"{0}LAUNCHAMEBACKGROUND.PNG (750x500){0}LAUNCHAMEBACKGROUND_AT_3TO2X.PNG (1125x750){0}LAUNCHAMEBACKGROUND_AT_2X.PNG (1500x1000){0}Dimensions MUST match and images MUST be PNG format", Environment.NewLine);
        public static string directory = string.Format("{0} *", name.full);
        public static string[] files = { "\\Adobe Media Encoder.exe" };
        public static string[] masks = { "LAUNCHAMEBACKGROUND", "LAUNCHAMEBACKGROUND_AT_3TO2X", "LAUNCHAMEBACKGROUND_AT_2X" };
        #endregion

        //The rest is all common
    }
    #endregion
}
