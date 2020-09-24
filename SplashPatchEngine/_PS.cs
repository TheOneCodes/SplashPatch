using System;
using System.IO;
using PShop.PShop.CC2017;
using Microsoft.AspNetCore.StaticFiles;
using System.Drawing;

namespace SplashPatchEngine
{
    #region Photoshop specific functions and variables
    internal class PS
    {
        #region set DEFAULT STRINGS
        public static (string display, string full, string shorten) name = ("Photoshop", "Adobe Photoshop", "PS");
        public static string helpText = string.Format("Requires patch of 3 files{0}\"IconResources.idx\"{0}\"PSIconsHighRes.dat\"{0}\"PSIconsLowRes.dat\"{0}With the 2 versions of Splash1080Background_s0.png{0}{0}LOW @ (750x500){0}HIGH @ (1500x1000){0}as well as a modification to \"SplashBoxTextBackground\" in \"UIColors.txt\" so text does not have a white background", Environment.NewLine);
        public static string directory = string.Format("{0} *", name.full);
        public static string[] files = { "\\Required\\UiColors.txt", "\\Resources\\IconResources.idx", "\\Resources\\PSIconsHighRes.dat", "\\Resources\\PSIconsLowRes.dat" };
        //masks are OMITTED as Photoshop DOES NOT hide it's PNGs in .exe or .dll s, instead it's in a proprietary archive that must be extracted in a completely different manor!
        #endregion

        #region get SPECIFIC PIXEL COLOUR so that TEXT DOESN'T HAVE A DISGUSTING BACKGROUND!!!
        public static Color SplashColor(string f)
        {
            try
            {
                Bitmap bm = new Bitmap(f);
                return bm.GetPixel(bm.Width / 4, bm.Height / 2);
            }
            catch
            {
                return Color.White;
            }
        }
        #endregion

        #region SAVE --- !IMPORTANT! --- VERY SPECIFIC TO PHOTOSHOP
        internal static bool Save(string from, string to)
        {
            string location = Path.GetTempPath() + "SplashPatch\\Photoshop";

            //Filer.Extract(from + "\\resources", to);
            if (Directory.Exists(location))
            {
                Directory.Delete(location, true);
            }
            Filer.Extract(from + "\\Resources", location);
            if (File.Exists(to + "\\High.png"))
            {
                File.Delete(to + "\\High.png");
            }
            File.Copy(location + "\\High\\Splash1080Background_s0.png", to + "\\High.png");
            if (File.Exists(to + "\\Low.png"))
            {
                File.Delete(to + "\\Low.png");
            }
            File.Copy(location + "\\Low\\Splash1080Background_s0.png", to + "\\Low.png");
            if (Directory.Exists(location))
            {
                Directory.Delete(location, true);
            }
            return true;
        }
        #endregion

        #region CHANGE --- !IMPORTANT! --- VERY SPECIFIC TO PHOTOSHOP
        internal static void Change(string from, string to)
        {

            int[] LOW = { 750, 500 };
            int[] HIGH = { 1500, 1000 };

            string location = Path.GetTempPath() + "SplashPatch\\Photoshop";
            if (Directory.Exists(location))
            {
                Directory.Delete(location, true);
            }

            try
            {
                Filer.Extract(from + "\\Resources", location);
            }
            catch (Exception e)
            {
                throw new FailedPhotoshopExtract(e.ToString());
            }

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


                    //MessageBox.Show(dimensions[0] + " x " + dimensions[1]);
                    if (dimensions[0] == LOW[0] && dimensions[1] == LOW[1])
                    {
                        File.Copy(to, location + "\\Low\\Splash1080Background_s0.png", true);
                    }
                    else if (dimensions[0] == HIGH[0] && dimensions[1] == HIGH[1])
                    {
                        File.Copy(to, location + "\\High\\Splash1080Background_s0.png", true);
                    }
                    else
                    {
                        throw new InvalidDimensions(dimensions, LOW, HIGH);
                    }

                }
                else
                {
                    throw new InvalidPNG(to);
                }
            }

            foreach(string f in files)
            {
                common.Backup(from + f);
            }

            try
            {
                Filer.Pack(location, location + "\\Resources");
                foreach (string f in files)
                {
                    if (!f.EndsWith("txt"))
                    {
                        var source = new FileInfo(location + f);
                        if (File.Exists(from + f))
                        {
                            System.GC.Collect();
                            System.GC.WaitForPendingFinalizers();
                            File.Delete(from + f);
                        }
                        source.CopyTo(from + f, true);
                    }
                }

            }
            catch (Exception e)
            {
                throw new FailedPhotoshopPack(e.ToString());
            }

            /*
            if (Directory.Exists(location))
            {
                Directory.Delete(location, true);
            }
            */

            Color spc = SplashColor(to);

            string[] arrLine = File.ReadAllLines(from + files[0]);

            arrLine[1660 - 1] = "		[ " + spc.R + ", " + spc.G + ", " + spc.B + ", 1.0 ],";
            arrLine[1661 - 1] = "		[ 255, 255, 255, 0 ],";
            arrLine[1662 - 1] = "		[ 255, 255, 255, 0 ],";
            arrLine[1663 - 1] = "		[ 255, 255, 255, 0 ]";

            File.WriteAllLines(from + files[0], arrLine);
        }
        #endregion
    }
    #endregion
}
