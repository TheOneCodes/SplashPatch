using System;
using System.Collections.Generic;
using System.Text;

namespace SplashPatchEngine
{
    #region Dreamweaver specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class DW
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("Dreamweaver", "Adobe Dreamweaver", "DW");
        public static string helpText = string.Format("Requires dll patch of 3 Images in each culture code's \"Resources.dll\"{0}SPLASHNORMAL.PNG (750x500){0}SPLASHNORMAL_AT_3TO2X.PNG (1125x750){0}SPLASHNORMAL_AT_2X.PNG (1500x1000){0}Dimensions MUST match and images MUST be PNG format", Environment.NewLine);
        public static string directory = string.Format("{0} *", name.full);
        public static string[] files = { "\\*\\Resources\\Resources.dll" }; //* is for culture codes (ex: en_US)
        public static string[] masks = { "SPLASHNORMAL", "SPLASHNORMAL_AT_3TO2X", "SPLASHNORMAL_AT_2X" };
        #endregion

        //The rest is all common
    }
    #endregion
}
