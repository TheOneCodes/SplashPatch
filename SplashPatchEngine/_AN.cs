using System;

namespace SplashPatchEngine
{
    #region Animate specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class AN
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("Animate", "Adobe Animate", "AN");
        public static string helpText = string.Format("Requires dll patch of 3 Images in \"Animate.exe\"{0}P_SPLASH_N.PNG (750x500){0}P_SPLASH_N_AT_3TO2X.PNG (1125x750){0}P_SPLASH_N_AT_2X.PNG (1500x1000){0}Dimensions MUST match and images MUST be PNG format", Environment.NewLine);           
        public static string directory = string.Format("{0} *",name.full);
        public static string[] files = { "\\Animate.exe" };
        public static string[] masks = { "P_SPLASH_N", "P_SPLASH_N_AT_3TO2X", "P_SPLASH_N_AT_2X" };
        #endregion
    }
    #endregion
}
