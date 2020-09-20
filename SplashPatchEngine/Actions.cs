using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace SplashPatchEngine
{
    public class Actions
    {
        public static void Save(string program, string from, string to, string[] files, string[] masks, string year)
        {
            switch (program.ToLower())
            {
                case "ps":
                case "photoshop":
                    PS.Save(from, to);
                    break;
                case "dw":
                case "dreamweaver":
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
                case "au":
                case "audition":
                case "ch":
                case "character_animator":
                case "character animator":
                case "me":
                case "media_encoder":
                case "media encoder":
                    common.Save(from + files[0], to, masks, year);
                    break;
                case "ai":
                case "illustrator":
                case "pl":
                case "prelude":
                case "pr":
                case "premiere_pro":
                case "premiere pro":
                    common.Copy(from, files, to, false, year);
                    break;
                default:
                    throw new InvalidProgram(program);
            }
        }

        public static void Change(string program, string from, string to, string[] files, string[] masks, string year)
        {
            switch (program.ToLower())
            {
                case "ps":
                case "photoshop":
                    PS.Change(from, to);
                    break;
                case "dw":
                case "dreamweaver":
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
                case "au":
                case "audition":
                case "ch":
                case "character_animator":
                case "character animator":
                case "me":
                case "media_encoder":
                case "media encoder":
                    common.Change(from + files[0], to, masks, year);
                    break;
                case "ai":
                case "illustrator":
                case "pl":
                case "prelude":
                case "pr":
                case "premiere_pro":
                case "premiere pro":
                    common.Copy(from, files, to, true, year);
                    break;
                default:
                    throw new InvalidProgram(program);
            }
        }

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
    }
}
