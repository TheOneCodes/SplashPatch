using System;
using System.Collections.Generic;
using System.Text;

namespace SplashPatchEngine
{
    #region Dreamweaver specific ̶f̶u̶n̶c̶t̶i̶o̶n̶s̶ ̶a̶n̶d variables
    internal class DW
    {
        #region set DEFAULT STRINGS
        public static string directory = "Adobe Dreamweaver *";
        public static string[] files = { "\\*\\Resources\\Resources.dll" }; //* is for culture codes (ex: en_US)
        public static string[] masks = { "SPLASHNORMAL", "SPLASHNORMAL_AT_3TO2X", "SPLASHNORMAL_AT_2X" };
        #endregion

        //The rest is all common
    }
    #endregion
}
