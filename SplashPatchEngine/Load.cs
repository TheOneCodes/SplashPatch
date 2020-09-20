namespace SplashPatchEngine
{
    public class Load
    {
        public static (string fullName, string directory, string[] files, string[] masks, bool PNG) Variables(string program)
        {
            switch (program.ToLower())
            {
                case "ae":
                case "after_effects":
                case "after effects":
                    return ("Adobe After Effects", AE.directory, AE.files, AE.masks, false);
                case "au":
                case "audition":
                    return ("Adobe Audition", AU.directory, AU.files, AU.masks, false);
                case "ch":
                case "character_animator":
                case "character animator":
                    return ("Adobe Character Animator", CH.directory, CH.files, CH.masks, false);
                case "dw":
                case "dreamweaver":
                    return (program, DW.directory, DW.files, DW.masks, false);
                case "ai":
                case "illustrator":
                    return (program, AI.directory, AI.files, null, true);
                case "me":
                case "media_encoder":
                case "media encoder":
                    return (program, ME.directory, ME.files, ME.masks, false);
                case "ps":
                case "photoshop":
                    return (program, PS.directory, PS.files, null, false);
                case "pl":
                case "prelude":
                    return (program, PL.directory, PL.files, null, true);
                case "pr":
                case "premiere_pro":
                case "premiere pro":
                    return (program, PR.directory, PR.files, null, true);
                default:
                    throw new InvalidProgram(program);
            }
        }
    }
}
