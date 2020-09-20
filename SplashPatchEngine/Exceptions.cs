using System;

namespace SplashPatchEngine
{
    public class FailedAutoLocate : Exception
    {
        public FailedAutoLocate(string name)
            : base(String.Format("Could not locate any of the provided files for {0} in any of the typical locations. Please manually locate folder (Use Try Catch for this)", name))
        {

        }
    }

    public class IncompatibleVersion : Exception
    {
        public IncompatibleVersion(string loc)
            : base(String.Format("Program found in \"{0}\" is detected as being compatible with this version of SplashPatch Engine.", loc.ToString()))
        {

        }
    }

    public class MissingRequiredFile : Exception
    {
        public MissingRequiredFile(string name, string dir)
            : base(String.Format("Could not locate required file \"{0}\" in \"{1}\". Please verify this is a compatible copy and / or manually locate a valid copy.", name, dir))
        {

        }
        public MissingRequiredFile(string dir)
            : base(String.Format("Could not locate unknown required file in \"{0}\". Please verify this is a compatible copy and / or manually locate a valid copy.", dir))
        {

        }
    }

    public class InvalidDimensions : Exception
    {
        public InvalidDimensions(int[] given, int[] x, int[] xx, int[] xxx)
            : base(String.Format("Invalid dimensions ({0} x {1}) Must be either {2} x {3}, {4} x {5} or {6} x {7}", given[0], given[1], x[0], x[1], xx[0], xx[1], xxx[0], xxx[1]))
        {

        }
        public InvalidDimensions(int[] given, int[] x, int[] xx)
            : base(String.Format("Invalid dimensions ({0} x {1}) Must be either {2} x {3}, or {4} x {5}", given[0], given[1], x[0], x[1], xx[0], xx[1]))
        {

        }
        public InvalidDimensions(int[] given, int[] x)
            : base(String.Format("Invalid dimensions ({0} x {1}) Must be {2} x {3}", given[0], given[1], x[0], x[1]))
        {

        }
    }

    public class InvalidPNG : Exception
    {
        public InvalidPNG()
            : base("The given image is not a PNG")
        {

        }
        public InvalidPNG(string name)
            : base(String.Format("\"{0}\" is not a PNG", name))
        {

        }
    }

    public class FailedPhotoshopExtract : Exception
    {
        public FailedPhotoshopExtract(string ex)
            : base(String.Format("PShop.CC.IconResources has failed to extract, {0}", ex))
        {

        }
    }
    public class FailedPhotoshopPack : Exception
    {
        public FailedPhotoshopPack(string ex)
            : base(String.Format("PShop.CC.IconResources has failed to pack, {0}", ex))
        {

        }
    }
    public class InvalidProgram : Exception
    {
        public InvalidProgram(string value)
            : base(String.Format("The Program string value \"{0}\" is invalid.", value))
        {

        }
    }
}
