using System;

namespace SplashPatchEngine
{
    #region After Effects specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class AE
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("After Effects", "Adobe After Effects", "AE");
        public static string helpText = string.Format("Requires dll patch of 3 Images in \"AfterFXLib.dll\"{0}AE_SPLASH.PNG (750x500){0}AE_SPLASH_AT_3TO2X.PNG (1125x750){0}AE_SPLASH_AT_2X.PNG (1500x1000){0}Dimensions MUST match and images MUST be PNG format",Environment.NewLine);           
        public static string directory = string.Format("{0} *",name.full);
        public static string[] files = { "\\Support Files\\AfterFXLib.dll" };
        public static string[] masks = { "AE_SPLASH", "AE_SPLASH_AT_3TO2X", "AE_SPLASH_AT_2X" };
        #endregion
    }
    #endregion
}
