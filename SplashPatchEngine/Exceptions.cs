using System;

namespace SplashPatchEngine
{
    /// <summary>
    /// Auto-Locate has failed
    /// </summary>
    public class FailedAutoLocate : Exception
    {
        /// <summary>
        /// Auto-Locate has failed
        /// </summary>
        /// <param name="name"></param>
        public FailedAutoLocate(string name)
            : base(String.Format("Could not locate any of the provided files for {0} in any of the typical locations. Please manually locate folder (Use Try Catch for this)", name))
        {

        }
    }
    /// <summary>
    /// The selected program's version is not currently compatible
    /// </summary>
    public class IncompatibleVersion : Exception
    {
        /// <summary>
        /// The selected program's version is not currently compatible
        /// </summary>
        public IncompatibleVersion(string loc)
            : base(String.Format("Program found in \"{0}\" is detected as being compatible with this version of SplashPatch Engine.", loc.ToString()))
        {

        }
    }
    /// <summary>
    /// A file that is necessary for the selected program's splash screen is missing
    /// </summary>
    public class MissingRequiredFile : Exception
    {
        /// <summary>
        /// A file that is necessary for the selected program's splash screen is missing
        /// </summary>
        public MissingRequiredFile(string name, string dir)
            : base(String.Format("Could not locate required file \"{0}\" in \"{1}\". Please verify this is a compatible copy and / or manually locate a valid copy.", name, dir))
        {

        }
        /// <summary>
        /// A file that is necessary for the selected program's splash screen is missing
        /// </summary>
        public MissingRequiredFile(string dir)
            : base(String.Format("Could not locate unknown required file in \"{0}\". Please verify this is a compatible copy and / or manually locate a valid copy.", dir))
        {

        }
    }

    /// <summary>
    /// The image's dimensions do not match a splash screen of the selected program
    /// </summary>
    public class InvalidDimensions : Exception
    {
        /// <summary>
        /// The image's dimensions do not match a splash screen of the selected program
        /// </summary>
        public InvalidDimensions(int[] given, int[] x, int[] xx, int[] xxx)
            : base(String.Format("Invalid dimensions ({0} x {1}) Must be either {2} x {3}, {4} x {5} or {6} x {7}", given[0], given[1], x[0], x[1], xx[0], xx[1], xxx[0], xxx[1]))
        {

        }
        /// <summary>
        /// The image's dimensions do not match a splash screen of the selected program
        /// </summary>
        public InvalidDimensions(int[] given, int[] x, int[] xx)
            : base(String.Format("Invalid dimensions ({0} x {1}) Must be either {2} x {3}, or {4} x {5}", given[0], given[1], x[0], x[1], xx[0], xx[1]))
        {

        }
        /// <summary>
        /// The image's dimensions do not match a splash screen of the selected program
        /// </summary>
        public InvalidDimensions(int[] given, int[] x)
            : base(String.Format("Invalid dimensions ({0} x {1}) Must be {2} x {3}", given[0], given[1], x[0], x[1]))
        {

        }
    }

    /// <summary>
    /// The selected image is not in fact a PNG
    /// </summary>
    public class InvalidPNG : Exception
    {
        /// <summary>
        /// The selected image is not in fact a PNG
        /// </summary>
        public InvalidPNG()
            : base("The given image is not a PNG")
        {

        }
        /// <summary>
        /// The selected image is not in fact a PNG
        /// </summary>
        public InvalidPNG(string name)
            : base(String.Format("\"{0}\" is not a PNG", name))
        {

        }
    }
    /// <summary>
    /// Photoshop Extract simply failed (unknown)
    /// </summary>
    public class FailedPhotoshopExtract : Exception
    {
        /// <summary>
        /// Photoshop Extract simply failed (unknown)
        /// </summary>
        public FailedPhotoshopExtract(string ex)
            : base(String.Format("PShop.CC.IconResources has failed to extract, {0}", ex))
        {

        }
    }
    /// <summary>
    /// Photoshop Pack operation simply failed (unknown)
    /// </summary>
    public class FailedPhotoshopPack : Exception
    {
        /// <summary>
        /// Photoshop Pack operation simply failed (unknown)
        /// </summary>
        public FailedPhotoshopPack(string ex)
            : base(String.Format("PShop.CC.IconResources has failed to pack, {0}", ex))
        {

        }
    }
    /// <summary>
    /// The selected program is not valid
    /// </summary>
    public class InvalidProgram : Exception
    {
        /// <summary>
        /// The selected program is not valid
        /// </summary>
        public InvalidProgram(string value)
            : base(String.Format("The Program string value \"{0}\" is invalid.", value))
        {

        }
    }
}
