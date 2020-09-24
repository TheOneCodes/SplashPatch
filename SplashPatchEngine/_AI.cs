using System;
using System.Collections.Generic;
using System.Text;

namespace SplashPatchEngine
{
    #region Illustrator specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class AI
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("Illustrator", "Adobe Illustrator", "AI");
        public static string helpText = string.Format("Copies 3 images to the correct PNG directory{0}ai_cc_splash.PNG (750x500){0}ai_cc_splash@3to2x.PNG (1125x750){0}ai_cc_splash@2x.PNG (1500x1000){0}Dimensions MUST match and images MUST be PNG format", Environment.NewLine);
        public static string directory = string.Format("{0} *\\Support Files\\Contents\\Windows", name.full);
        public static string[] files = { "\\ai_cc_splash.png", "\\ai_cc_splash@3to2x.png", "\\ai_cc_splash@2x.png" };
        //public static string[] masks = null;
        #endregion

        //The rest is all common (easy copy)
    }
    #endregion
}
