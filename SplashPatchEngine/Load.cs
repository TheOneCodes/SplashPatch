using System.Collections.Generic;

namespace SplashPatchEngine
{
    /// <summary>
    /// SplashPatchEngine Load tasks
    /// </summary>
    public class Load
    {
        /// <summary>
        /// Get the necessary variables for a specific program
        /// </summary>
        /// <param name="program">The name or identifier of a supported Adobe Creative Cloud program</param>
        /// <returns></returns>
        public static ((string display, string full, string shorten) name, string directory, string[] files, string[] masks, bool PNG, string helpText) Variables(string program)
        {
            switch (program.ToLower())
            {
                case "ae":
                case "after_effects":
                case "after effects":
                    return (AE.name, AE.directory, AE.files, AE.masks, false, AE.helpText);
                case "au":
                case "audition":
                    return (AU.name, AU.directory, AU.files, AU.masks, false, AU.helpText);
                case "ch":
                case "character_animator":
                case "character animator":
                    return (CH.name, CH.directory, CH.files, CH.masks, false, CH.helpText);
                case "dw":
                case "dreamweaver":
                    return (DW.name, DW.directory, DW.files, DW.masks, false, DW.helpText);
                case "an":
                case "animate":
                    return (AN.name, AN.directory, AN.files, AN.masks, false, AN.helpText);
                case "ai":
                case "illustrator":
                    return (AI.name, AI.directory, AI.files, null, true, AI.helpText);
                case "me":
                case "media_encoder":
                case "media encoder":
                    return (ME.name, ME.directory, ME.files, ME.masks, false, ME.helpText);
                case "ps":
                case "photoshop":
                    return (PS.name, PS.directory, PS.files, null, false, PS.helpText);
                case "pl":
                case "prelude":
                    return (PL.name, PL.directory, PL.files, null, true, PL.helpText);
                case "pr":
                case "premiere_pro":
                case "premiere pro":
                    return (PR.name, PR.directory, PR.files, null, true, PR.helpText);
                default:
                    throw new InvalidProgram(program);
            }
        }
        /// <summary>
        /// Get a list of currently compatible software
        /// </summary>
        /// <returns>List of Tuple of display name, full name, 2 letter shortened and help text</returns>
        public static List<(string display, string full, string shorten, string helpText)> List()
        {
            List<(string display, string full, string shorten, string helpText)> returning = new List<(string display, string full, string shorten, string helpText)>();
            (string display, string full, string shorten, string helpText) hold;
            hold = (AE.name.display, AE.name.full, AE.name.shorten, AE.helpText);
            returning.Add(hold);
            hold = (AN.name.display, AN.name.full, AN.name.shorten, AN.helpText);
            returning.Add(hold);
            hold = (AU.name.display, AU.name.full, AU.name.shorten, AU.helpText);
            returning.Add(hold);
            hold = (CH.name.display, CH.name.full, CH.name.shorten, CH.helpText);
            returning.Add(hold);
            hold = (DW.name.display, DW.name.full, DW.name.shorten, DW.helpText);
            returning.Add(hold);
            hold = (AI.name.display, AI.name.full, AI.name.shorten, AI.helpText);
            returning.Add(hold);
            hold = (ME.name.display, ME.name.full, ME.name.shorten, ME.helpText);
            returning.Add(hold);
            hold = (PS.name.display, PS.name.full, PS.name.shorten, PS.helpText);
            returning.Add(hold);
            hold = (PL.name.display, PL.name.full, PL.name.shorten, PL.helpText);
            returning.Add(hold);
            hold = (PR.name.display, PR.name.full, PR.name.shorten, PR.helpText);
            returning.Add(hold);
            return returning;
        }

    }
}
