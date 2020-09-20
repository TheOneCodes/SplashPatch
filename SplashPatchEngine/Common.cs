using System;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using ResourceHacker;
using System.Collections.Generic;
using System.Globalization;

namespace SplashPatchEngine
{
    internal class common
    {
        #region set DEFAULT PROGRAM FILES FOLDERS WITHOUT DRIVE LETTER
        public static string amd_64 = "Program Files\\Adobe\\";
        public static string i386 = "Program Files (x86)\\Adobe\\";
        #endregion

        #region set DEFAULT EXPECTED DIMENSIONS OF IMAGES
        public static readonly int[,] dimensions = new int[3, 2] { { 750, 500 }, { 1125, 750 }, { 1500, 1000 } };
        #endregion

        #region VERSION
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

        //remove me
        #region LOCATE
        //now using tuples for both year and location
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
                        if (Directory.Exists(d.Name + amd_64 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + amd_64 + dir, files);
                                return (d.Name + amd_64 + dir, i.ToString(), ver);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                        else if (Directory.Exists(d.Name + i386 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + i386 + dir, files);
                                return (d.Name + i386 + dir, i.ToString(), ver);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                        dir = name.Replace(" *", " CC " + i);
                        if (Directory.Exists(d.Name + amd_64 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + amd_64 + dir, files);
                                return (d.Name + amd_64 + dir, i.ToString(), ver);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                        else if (Directory.Exists(d.Name + i386 + dir))
                        {
                            try
                            {
                                var ver = Verify(d.Name + i386 + dir, files);
                                return (d.Name + i386 + dir, i.ToString(), ver);
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

        //me too
        #region VERIFY
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

        #region COPY
        public static void Copy(string location, string[] files, string to, bool change = false, string version = null, int[] SPLASH = null, int[] SPLASH_AT_3TO2X = null, int[] SPLASH_AT_2X = null)
        {
            //set the default dimensions (Adobe Creative Cloud 2020) as c# is annoying and can't in one line
            #region set DEFAULTS
            if (SPLASH == null)
            {
                SPLASH = new int[] { 750, 500 };
            }
            if (SPLASH_AT_3TO2X == null)
            {
                SPLASH_AT_3TO2X = new int[] { 1125, 750 };
            }
            if (SPLASH_AT_2X == null)
            {
                SPLASH_AT_2X = new int[] { 1500, 1000 };
            }
            #endregion

            if (change)
            {
                //is it really a PNG
                #region verify PNG and dimensions
                var provider = new FileExtensionContentTypeProvider();

                string contentType;

                if (provider.TryGetContentType(to, out contentType))
                {
                    if (contentType == "image/png")
                    {
                        int[] dimensions = new int[2];
                        var buff = new byte[32];
                        using (var d = File.OpenRead(to))
                        {
                            d.Read(buff, 0, 32);
                        }
                        const int wOff = 16;
                        const int hOff = 20;
                        dimensions[0] = BitConverter.ToInt32(new[] { buff[wOff + 3], buff[wOff + 2], buff[wOff + 1], buff[wOff + 0], }, 0);
                        dimensions[1] = BitConverter.ToInt32(new[] { buff[hOff + 3], buff[hOff + 2], buff[hOff + 1], buff[hOff + 0], }, 0);

                        #region change resource
                        if (dimensions[0] == SPLASH[0] && dimensions[1] == SPLASH[1])
                        {
                            //masks[0] must be the regular SPLASH
                            Backup(location + files[0]);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Copy(to, location + files[0], true);
                        }
                        else if (dimensions[0] == SPLASH_AT_3TO2X[0] && dimensions[1] == SPLASH_AT_3TO2X[1])
                        {
                            //masks[1] must be the medium SPLASH
                            Backup(location + files[1]);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Copy(to, location + files[1], true);
                        }
                        else if (dimensions[0] == SPLASH_AT_2X[0] && dimensions[1] == SPLASH_AT_2X[1])
                        {
                            //masks[2] must be the large SPLASH
                            Backup(location + files[2]);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Copy(to, location + files[2], true);
                        }
                        else
                        {
                            throw new InvalidDimensions(dimensions, SPLASH, SPLASH_AT_3TO2X, SPLASH_AT_2X);
                        }
                        #endregion

                    }
                    else
                    {
                        throw new InvalidPNG(to);
                    }
                }
                #endregion

            }
            else
            {
                foreach (string f in files)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    File.Copy(location + f, to + f, true);
                }
            }
        }
        #endregion

        #region SAVE
        public static void Save(string dll, string to, string[] masks, string version = "2020")
        {
            #region get PNGs
            foreach (string m in masks)
            {
                if (masks[0] == "SPLASHNORMAL")
                {
                    Get.PNG(dll, to, m.Replace("*", version), "1033");
                }
                else
                {
                    Get.PNG(dll, to, m.Replace("*", version));
                }
            }
            #endregion
        }
        #endregion

        #region BACKUP
        public static void Backup(string origional, string ending = ".old")
        {
            #region backup TO ".old"
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Copy(origional, origional + ending, true);
            #endregion
        }
        #endregion

        #region REPLACE
        public static void Change(string dll, string to, string[] masks, string version = "2020", int[] SPLASH = null, int[] SPLASH_AT_3TO2X = null, int[] SPLASH_AT_2X = null)
        {
            //set the default dimensions (Adobe Creative Cloud 2020) as c# is annoying and can't in one line
            #region set DEFAULTS
            if (SPLASH == null)
            {
                SPLASH = new int[] { 750, 500 };
            }
            if (SPLASH_AT_3TO2X == null)
            {
                SPLASH_AT_3TO2X = new int[] { 1125, 750 };
            }
            if (SPLASH_AT_2X == null)
            {
                SPLASH_AT_2X = new int[] { 1500, 1000 };
            }
            #endregion
            //change the PNG resource of specified dll
            #region attempt REPLACE PNG
            //is it really a PNG
            #region verify PNG and dimensions
            var provider = new FileExtensionContentTypeProvider();

            string contentType;

            if (provider.TryGetContentType(to, out contentType))
            {
                if (contentType == "image/png")
                {
                    int[] dimensions = new int[2];
                    var buff = new byte[32];
                    using (var d = File.OpenRead(to))
                    {
                        d.Read(buff, 0, 32);
                    }
                    const int wOff = 16;
                    const int hOff = 20;
                    dimensions[0] = BitConverter.ToInt32(new[] { buff[wOff + 3], buff[wOff + 2], buff[wOff + 1], buff[wOff + 0], }, 0);
                    dimensions[1] = BitConverter.ToInt32(new[] { buff[hOff + 3], buff[hOff + 2], buff[hOff + 1], buff[hOff + 0], }, 0);

                    #region change resource
                    if (masks[0] == "SPLASHNORMAL")
                    {
                        #region reg, med or large
                        if (dimensions[0] == SPLASH[0] && dimensions[1] == SPLASH[1])
                        {
                            //masks[0] must be the regular SPLASH
                            Backup(dll);
                            Set.PNG(dll, to, masks[0].Replace("*", version), "1033");
                        }
                        else if (dimensions[0] == SPLASH_AT_3TO2X[0] && dimensions[1] == SPLASH_AT_3TO2X[1])
                        {
                            //masks[1] must be the medium SPLASH
                            Backup(dll);
                            Set.PNG(dll, to, masks[1].Replace("*", version), "1033");
                        }
                        else if (dimensions[0] == SPLASH_AT_2X[0] && dimensions[1] == SPLASH_AT_2X[1])
                        {
                            //masks[2] must be the large SPLASH
                            Backup(dll);
                            Set.PNG(dll, to, masks[2].Replace("*", version), "1033");
                        }
                        else
                        {
                            throw new InvalidDimensions(dimensions, SPLASH, SPLASH_AT_3TO2X, SPLASH_AT_2X);
                        }
                        #endregion
                    }
                    else
                    {
                        #region reg, med or large
                        if (dimensions[0] == SPLASH[0] && dimensions[1] == SPLASH[1])
                        {
                            //masks[0] must be the regular SPLASH
                            Backup(dll);
                            Set.PNG(dll, to, masks[0].Replace("*", version));
                        }
                        else if (dimensions[0] == SPLASH_AT_3TO2X[0] && dimensions[1] == SPLASH_AT_3TO2X[1])
                        {
                            //masks[1] must be the medium SPLASH
                            Backup(dll);
                            Set.PNG(dll, to, masks[1].Replace("*", version));
                        }
                        else if (dimensions[0] == SPLASH_AT_2X[0] && dimensions[1] == SPLASH_AT_2X[1])
                        {
                            //masks[2] must be the large SPLASH
                            Backup(dll);
                            Set.PNG(dll, to, masks[2].Replace("*", version));
                        }
                        else
                        {
                            throw new InvalidDimensions(dimensions, SPLASH, SPLASH_AT_3TO2X, SPLASH_AT_2X);
                        }
                        #endregion
                    }
                    #endregion

                }
                else
                {
                    //I'm not bothering to check and convert JPEGs or whatever yet
                    throw new InvalidPNG(to);
                }
            }
            #endregion

            #endregion

        }
        #endregion
    }
}
