/* Resource Hacker integrations for SplashPatch
 * 
 * Resource Hacker by Angus Johnson is used by SplashPatch to interact with exe and dll files for the soul purpose of swapping integrated PNG files
 * 
 * The SplashPatch installer will download Resource Hacker from angusj.com directly as per the Resource Hacker Terms
 */

using System;
using System.Diagnostics;

namespace ResourceHacker
{
    public class MissingRH : Exception
    {
        public MissingRH(string location)
            : base(String.Format("Resource hacker is required and is missing from \"{0}\".", location))
        {

        }
    }

    public class Get
    {
        public static string find = AppDomain.CurrentDomain.BaseDirectory + "rh.dll";

        public static string PNG(string dll, string save, string name, string annoying_number = "0")
        {
            Verify.RHdll(find);

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = "-open \"" + dll + "\" -save \"" + save + "\\" + name + ".png\" -action extract -mask PNG," + name + "," + annoying_number + " -log CON",
                    FileName = find,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            string val = "";

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                val += proc.StandardOutput.ReadLine().ToString();
            }

            return val;
        }
    }

    public class Verify
    {
        public static void RHdll(string find)
        {
            if(!System.IO.File.Exists(find))
            {
                throw new MissingRH(find);
            }
        }
    }

    public class Set
    {
        public static string find = AppDomain.CurrentDomain.BaseDirectory + "rh.dll";

        public static string PNG(string dll, string png, string name, string annoying_number = "0")
        {
            Verify.RHdll(find);
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = "-open \"" + dll + "\" -save \"" + dll + "\" -action modify -res \"" + png + "\" -mask PNG," + name + "," + annoying_number + " -log CON",
                    FileName = find,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            string val = "";

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                val += proc.StandardOutput.ReadLine().ToString();
            }

            return val;
        }
    }
}
