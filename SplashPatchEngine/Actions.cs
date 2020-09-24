using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace SplashPatchEngine
{
    /// <summary>
    /// SplashPatchEngine Actions to be used by dependant application.
    /// </summary>
    public class Actions
    {
        #region Save
        /// <summary>
        /// <para>SplashPatchEngine Save Action</para>
        /// <para>This function will save the specified program's currently applied splash screen to multiple PNG files</para>
        /// </summary>
        /// <param name="program">Non-case sensitive name of the Adobe Creative Cloud Program, with space, underscore or 2 letter code (for example: "ae", "after_effects" or "after effects")</param>
        /// <param name="from">Full directory to the current installation location (for example: "C:\Program Files\Adobe\Adobe After Effects 2020" or C:\Program Files\Adobe\Adobe Photoshop 2017)</param>
        /// <param name="to">Full directory to where the user would like the splash images to save to (for example: "C:\Users\username\Pictures\" or "E:\splash images\")</param>
        /// <param name="files">Specified program's files array (not necessary for Photoshop)</param>
        /// <param name="masks">Specified program's masks array (not necessary for Photoshop, Illustrator, Prelude, Premiere Pro)</param>
        /// <param name="year">Specified program's release year (for some programs that require the year of release in mask, not necessary for Photoshop)</param>
        public static void Save(string program, string from, string to, string[] files, string[] masks, string year)
        {
            switch (program.ToLower())
            {
                case "ps":
                case "photoshop":
                    //Photoshop
                    PS.Save(from, to);
                    break;
                case "dw":
                case "dreamweaver":
                    //Dreamweaver
                    CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
                    foreach (CultureInfo cul in cinfo)
                    {
                        if (File.Exists(from + files[0].Replace("*", cul.Name.Replace("-", "_"))))
                        {
                            common.Save(from + files[0].Replace("*", cul.Name.Replace("-", "_")), to + "\\" + cul.Name.Replace("-", "_"), masks, year);
                        }
                    }
                    break;
                case "ae":
                case "after_effects":
                case "after effects":
                case "an":
                case "animate":
                case "au":
                case "audition":
                case "ch":
                case "character_animator":
                case "character animator":
                case "me":
                case "media_encoder":
                case "media encoder":
                    //After Effects, Audition, Character Animator, Media Encoder, Animate
                    common.Save(from + files[0], to, masks, year);
                    break;
                case "ai":
                case "illustrator":
                case "pl":
                case "prelude":
                case "pr":
                case "premiere_pro":
                case "premiere pro":
                    //Illustrator, Prelude, Premiere Pro
                    common.Copy(from, files, to, false, year);
                    break;
                default:
                    //None of the above
                    throw new InvalidProgram(program);
            }
        }
        #endregion

        #region Change
        /// <summary>
        /// <para>SplashPatchEngine Change Action</para>
        /// <para>This function will change the specified program's currently applied splash screen to the specified PNG</para>
        /// <para>It will only change one dimension at a time, only one PNG can and should be selected at a time.</para>
        /// </summary>
        /// <param name="program">Non-case sensitive name of the Adobe Creative Cloud Program, with space, underscore or 2 letter code (for example: "ae", "after_effects" or "after effects")</param>
        /// <param name="from">Full directory to the current installation location (for example: "C:\Program Files\Adobe\Adobe After Effects 2020" or C:\Program Files\Adobe\Adobe Photoshop 2017)</param>
        /// <param name="to">Full directory to the PNG of witch the use would like to change the splash to (for example: "C:\Users\username\Pictures\splash.png" or "E:\splash images\an image.png")</param>
        /// <param name="files">Specified program's files array (not necessary for Photoshop)</param>
        /// <param name="masks">Specified program's masks array (not necessary for Photoshop, Illustrator, Prelude, Premiere Pro)</param>
        /// <param name="year">Specified program's release year (for some programs that require the year of release in mask, not necessary for Photoshop)</param>
        public static void Change(string program, string from, string to, string[] files, string[] masks, string year)
        {
            switch (program.ToLower())
            {
                case "ps":
                case "photoshop":
                    //Photoshop
                    PS.Change(from, to);
                    break;
                case "dw":
                case "dreamweaver":
                    //Dreamweaver
                    CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
                    foreach (CultureInfo cul in cinfo)
                    {
                        if (File.Exists(from + files[0].Replace("*", cul.Name.Replace("-", "_"))))
                        {
                            common.Change(from + files[0].Replace("*", cul.Name.Replace("-", "_")), to, masks, year);
                        }
                    }
                    break;
                case "ae":
                case "after_effects":
                case "after effects":
                case "an":
                case "animate":
                case "au":
                case "audition":
                case "ch":
                case "character_animator":
                case "character animator":
                case "me":
                case "media_encoder":
                case "media encoder":
                    //After Effects, Audition, Character Animator, Media Encoder
                    common.Change(from + files[0], to, masks, year);
                    break;
                case "ai":
                case "illustrator":
                case "pl":
                case "prelude":
                case "pr":
                case "premiere_pro":
                case "premiere pro":
                    //Illustrator, Prelude, Premiere Pro
                    common.Copy(from, files, to, true, year);
                    break;
                default:
                    //None of the above
                    throw new InvalidProgram(program);
            }
        }
        #endregion

        #region Locate 
        /// <summary>
        /// SplashPatchEngine Locate Action
        /// This function will automatically locate the specified program's current install location
        /// </summary>
        /// <param name="name">Specified program's name string</param>
        /// <param name="files">Specified program's files array</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <term>location</term>
        /// <description>Full directory to the current Auto-Detected installation location (for example: "C:\Program Files\Adobe\Adobe After Effects 2020" or C:\Program Files\Adobe\Adobe Photoshop 2017)</description>
        /// </item>
        /// <item>
        /// <term>version</term>
        /// <description>Specified program's Auto-Detected release year</description>
        /// </item>
        /// <item>
        /// <term>culture_code</term>
        /// <description>List of the detected culture codes (null if not Dreamweaver)</description>
        /// </item>
        /// </list>
        /// </returns>
        public static (string location, string version, List<string> culture_code) Locate(string name, string[] files)
        {
            #region locate DIRECTORY WITH SPECIFIED PROGRAM name
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType.ToString() == "Fixed" && d.IsReady)
                {
                    for (int i = 2025; i > 2017; i--)
                    {
                        string dir = name.Replace(" *", " " + i);
                        if (Directory.Exists(d.Name + common.amd_64 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + common.amd_64 + dir, files);
                                return (d.Name + common.amd_64 + dir, i.ToString(), ver);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                        else if (Directory.Exists(d.Name + common.i386 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + common.i386 + dir, files);
                                return (d.Name + common.i386 + dir, i.ToString(), ver);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                        dir = name.Replace(" *", " CC " + i);
                        if (Directory.Exists(d.Name + common.amd_64 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + common.amd_64 + dir, files);
                                return (d.Name + common.amd_64 + dir, i.ToString(), ver);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                        else if (Directory.Exists(d.Name + common.i386 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + common.i386 + dir, files);
                                return (d.Name + common.i386 + dir, i.ToString(), ver);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                    }
                }
            }

            throw new FailedAutoLocate(name);
            #endregion
        }
        #endregion

        #region Verify
        /// <summary>
        /// <para>SplashPatchEngine Verify Action</para>
        /// <para>This function will automatically locate the specified program's current install location</para>
        /// </summary>
        /// <param name="d">Specified program's suspected location</param>
        /// <param name="files">Specified program's files array</param>
        /// <param name="region">True if Dreamweaver</param>
        /// <returns>Returns either a list of region culture codes or null</returns>
        public static List<string> Verify(string d, string[] files, bool region = false)
        {
            if (files[0].Contains("*"))
            {
                region = true;
            }

            #region check IF EXPECTED FILES EXIST
            if (region)
            {
                List<string> reg = new List<string>();

                CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);

                foreach (string s in files)
                {
                    foreach (CultureInfo cul in cinfo)
                    {
                        if (File.Exists(d + s.Replace("*", cul.Name.Replace("-", "_"))))
                        {
                            reg.Add(s.Replace("*", cul.Name.Replace("-", "_")));
                        }

                    }
                }

                if (reg.Count < files.Length)
                {
                    throw new MissingRequiredFile(d);
                }

                return reg;
            }
            else
            {
                foreach (string s in files)
                {
                    if (!File.Exists(d + s))
                    {
                        throw new MissingRequiredFile(s, d);
                    }
                }
                return new List<string> { null };
            }
            #endregion
        }
        #endregion

        #region Version
        /// <summary>
        /// <para>SplashPatchEngine Version Action</para>
        /// <para>This function will automatically detect the specified program's release year from it's directory</para>
        /// </summary>
        /// <param name="location">Specified program's location</param>
        /// <returns>Version year as string</returns>
        public static string Version(string location)
        {
            #region just a for loop
            for (int i = 2017; i < 2025; i++)
            {
                if (location.Contains(i.ToString()))
                {
                    return i.ToString();
                }
            }
            #endregion

            throw new IncompatibleVersion(location);
        }
        #endregion

    }
}
