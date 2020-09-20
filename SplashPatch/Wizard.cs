/*
 * SplashPatch Wizard
 * TheOneCode, September 2020
 * 
 * currently only tested with 2020 versions
 * 
 * requires SplashPatch Engine
 */

#region using/ inclusions
using System;
using System.Windows.Forms;
using System.IO;
using SplashPatchEngine;
using System.Diagnostics;
using System.Collections.Generic;
using SplashPatch.Properties;
using System.Globalization;
using System.Configuration;
using System.Threading.Tasks;
#endregion

#region unnecesary inclusions removed because why not
/*
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PShop.PShop.CC2017;
*/
#endregion

namespace SplashPatch
{
    public partial class Wizard : Form
    {

        Form splash = new Splash();

        #region initialize
        public Wizard()
        {
            splash.Text = "loading";
            splash.Show();
            Application.DoEvents();
            splash.Text = "Checking for EULA Agreement";
            #region check for EULA
            //Settings.Default.EULA = false;
            //Settings.Default.Save();
            if (Settings.Default.EULA == false)
            {
                splash.Text = "Please accept the SplashPatch EULA to begin";
                var license = new EULA();
                license.ShowDialog();
                if (license.DialogResult == DialogResult.OK)
                {
                    Settings.Default.EULA = true;
                    Settings.Default.Save();
                } else
                {
                    Settings.Default.EULA = false;
                    Settings.Default.Save();
                    Environment.Exit(69);
                }
            } else
            {
                Application.DoEvents();
                if (Control.ModifierKeys != Keys.Shift)
                    System.Threading.Thread.Sleep(3000);
            }
            #endregion

            #region check for Resource Hacker
            splash.Text = "Checking for Resource Hacker";
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "rh.dll"))
            {
                var dnl = new ResourceHackerDownload();
                dnl.SetMissing("rh.dll", "Resource Hacker");
                dnl.ShowDialog();

                if (dnl.DialogResult != DialogResult.OK)
                {
                    MessageBox.Show("Resource hacker is required (rh.dll)");
                    Environment.Exit(420);
                }

            } else
            {
                Application.DoEvents();
                if (Control.ModifierKeys != Keys.Shift)
                    System.Threading.Thread.Sleep(1000);
            }
            #endregion

            splash.Text = "Initializing components";
            InitializeComponent();

            #region populate appSelect
            appSelect.Items.AddRange(new object[] {
            "After Effects",
            "Audition",
            "Character Animator",
            "Dreamweaver",
            "Illustrator",
            "Media Encoder",
            "Photoshop",
            "Prelude",
            "Premiere Pro"});
            #endregion
            Application.DoEvents();
            if (Control.ModifierKeys != Keys.Shift)
                System.Threading.Thread.Sleep(1000);

            splash.Text = "Loading";
            #region set appSelect DEFAULT
            appSelect.Text = "Photoshop";
            Application.DoEvents();
            if (Control.ModifierKeys != Keys.Shift)
                System.Threading.Thread.Sleep(500);
            #endregion
        }
        #endregion

        string prg;

        #region change INDEX OF appSelect
        private void appSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region long SWITCH CASE
            prg = appSelect.Text;
            switch (appSelect.Text)
            {
                case "After Effects":
                    helpText.Text = " ===============    After Effects    =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Requires dll patch of 3 Images in \"AfterFXLib.dll\"" + Environment.NewLine + "AE_SPLASH.PNG (750x500)" + Environment.NewLine + "AE_SPLASH_AT_3TO2X.PNG (1125x750)" + Environment.NewLine + "AE_SPLASH_AT_2X.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
                case "Audition":
                    helpText.Text = " ===============      Audition       =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Requires dll patch of 3 Images in \"auui.dll\"" + Environment.NewLine + "AU_CC2020_SPLASH.PNG (750x500)" + Environment.NewLine + "AU_CC2020_SPLASH_AT_3TO2X.PNG (1125x750)" + Environment.NewLine + "AU_CC2020_SPLASH_AT_2X.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
                case "Character Animator":
                    helpText.Text = " ===============  Character Animator =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Requires exe patch of 3 Images in \"character animator.exe\"" + Environment.NewLine + "CH_CC2020_SPLASH.PNG (750x500)" + Environment.NewLine + "CH_CC2020_SPLASH_AT_3TO2X.PNG (1125x750)" + Environment.NewLine + "CH_CC2020_SPLASH_AT_2X.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
                case "Dreamweaver":
                    helpText.Text = " ===============     Dreamweaver     =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Requires dll patch of 3 Images in each culture code's \"Resources.dll\"" + Environment.NewLine + "SPLASHNORMAL.PNG (750x500)" + Environment.NewLine + "SPLASHNORMAL_AT_3TO2X.PNG (1125x750)" + Environment.NewLine + "SPLASHNORMAL_AT_2X.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
                case "Illustrator":
                    helpText.Text = " ===============     Illustrator     =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Copies 3 images to the correct PNG directory" + Environment.NewLine + "ai_cc_splash.PNG (750x500)" + Environment.NewLine + "ai_cc_splash@3to2x.PNG (1125x750)" + Environment.NewLine + "ai_cc_splash@2x.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
                case "Media Encoder":
                    helpText.Text = " ===============    Media Encoder    =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Requires exe patch of 3 Images in \"Adobe Media Encoder.exe\"" + Environment.NewLine + "LAUNCHAMEBACKGROUND.PNG (750x500)" + Environment.NewLine + "LAUNCHAMEBACKGROUND_AT_3TO2X.PNG (1125x750)" + Environment.NewLine + "LAUNCHAMEBACKGROUND_AT_2X.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
                case "Photoshop":
                    helpText.Text = " ===============      Photoshop      =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Requires patch of 3 files" + Environment.NewLine + "\"IconResources.idx\"" + Environment.NewLine + "\"PSIconsHighRes.dat\"" + Environment.NewLine + "\"PSIconsLowRes.dat\"" + Environment.NewLine + "With the 2 versions of Splash1080Background_s0.png" + Environment.NewLine + "LOW @ (750x500)" + Environment.NewLine + "HIGH @ (1500x1000)" + Environment.NewLine + "as well as a modification to \"SplashBoxTextBackground\" in \"UIColors.txt\" so text does not have a white background";
                    break;
                case "Prelude":
                    helpText.Text = " ===============       Prelude       =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Copies 3 images to the correct PNG directory" + Environment.NewLine + "pl_splash.PNG (750x500)" + Environment.NewLine + "pl_splash@3to2x.PNG (1125x750)" + Environment.NewLine + "pl_splash@2x.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
                case "Premiere Pro":
                    helpText.Text = " ===============     Premiere Pro    =============== " + Environment.NewLine + Environment.NewLine;
                    helpText.Text += "Copies 3 images to the correct PNG directory" + Environment.NewLine + "pr_splash.PNG (750x500)" + Environment.NewLine + "pr_splash@3to2x.PNG (1125x750)" + Environment.NewLine + "pr_splash@2x.PNG (1500x1000)" + Environment.NewLine + "Dimensions MUST match and images MUST be PNG format";
                    break;
            }
            #endregion
        }
        #endregion

        #region click SAVE button
        private void saveButton_Click(object sender, EventArgs e)
        {
            #region try Save
            try
            {
                progressUpdater.RunWorkerAsync();
                Enabled = false;
                Save(appSelect.Text);
            }
            #endregion

            #region ouch
            catch (FailedAutoLocate)
            {
                helpText.Text = "this should never catch here";
            }
            catch (Exception ex)
            {
                helpText.Text = ex.ToString();
            }
            Enabled = true;
            progressData.show = false;
            #endregion
        }
        #endregion

        #region click CHANGE button
        private void changeButton_Click(object sender, EventArgs e)
        {
            #region try CHANGE
            try
            {
                Change(appSelect.Text);
            }
            #endregion

            #region ouch
            catch (Exception ex)
            {
                helpText.Text = ex.ToString();
            }
            #endregion
        }
        #endregion

        #region SAVE
        bool Save(string program)
        {
            progressData.title = program;

            #region declare VARIABLES
            //bool cont = true;
            bool auto = false;
            string from = string.Empty;
            string to = string.Empty;
            string year = string.Empty;
            //DialogResult result;
            var info = SplashPatchEngine.Load.Variables(program);
            #endregion

            #region auto LOCATE
            //Check box
            if (manualCheck.Checked == false)
            {
                #region Locate()
                try
                {
                    var located = Actions.Locate(info.directory, info.files);
                    from = located.location;
                    year = located.version;
                    auto = true;
                    progressData.label = String.Format("located {0} at \"{1}\"", program, from);
                    helpText.Text = String.Format("located {0} at \"{1}\"", program, from);
                }
                catch (FailedAutoLocate)
                {
                    //it's ok, we handled it.
                    from = string.Empty;
                    progressData.label = String.Format("Failed to auto-locate {0}, please loacte it manually now:", program);
                    helpText.Text = String.Format("Failed to auto-locate {0}, please loacte it manually now:", program);
                }
                #endregion
            }
            #endregion

            #region manual LOCATE
            if (!auto)
            {
                #region FOLDER DIALOG
                folderDialog.SelectedPath = Environment.SpecialFolder.ProgramFiles.ToString();
                folderDialog.UseDescriptionForTitle = true;
                progressData.label = String.Format("Locate the current {0} installation", program);
                folderDialog.Description = String.Format("Locate the current {0} installation", program);
                //result = ;
                progressData.dialog = true;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    progressData.dialog = false;
                    string loc = folderDialog.SelectedPath;
                    if (info.PNG)
                    {
                        loc += "\\PNG";
                    }
                    try
                    {
                        if (Actions.Verify(loc, info.files) != null)
                        {
                            from = loc;
                            try
                            {
                                year = Actions.Version(folderDialog.SelectedPath);
                            }
                            catch (IncompatibleVersion)
                            {
                                progressData.label = String.Format("Installation of {0} at \"{1}\" is either to old or too new for this program", program, folderDialog.SelectedPath);
                                helpText.Text = String.Format("Installation of {0} at \"{1}\" is either to old or too new for this program", program, folderDialog.SelectedPath);
                                return false;
                            }
                        }
                        else
                        {
                            helpText.Text = "error";
                            return false;
                        }
                    }
                    catch (MissingRequiredFile)
                    {
                        progressData.label = String.Format("Invalid installation at \"{0}\" (Missing required file)", folderDialog.SelectedPath);
                        helpText.Text = String.Format("Invalid installation at \"{0}\" (Missing required file)", folderDialog.SelectedPath);
                        return false;
                    }
                }
                else
                {
                    progressData.dialog = false;
                    progressData.label = "dialog box closed";
                    helpText.Text = "dialog box closed";
                    return false;
                }
                #endregion
            }
            #endregion

            #region save LOCATE
            folderDialog.UseDescriptionForTitle = true;
            folderDialog.Description = String.Format("Select where to save the current {0} splash as individual PNG files", program);
            //result = ;

            progressData.dialog = true;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                progressData.dialog = false;
                to = folderDialog.SelectedPath;
                progressData.label = String.Format("Saving {0} splash to \"{1}\"", program, to);
                helpText.Text = String.Format("Saving {0} splash to \"{1}\"", program, to);
            }
            else
            {
                progressData.dialog = false;
                progressData.label = "dialog box closed";
                helpText.Text = "dialog box closed";
                return false;
            }
            #endregion

            #region "program".Save()
            try
            {
                Actions.Save(program, from, to, info.files, info.masks, year);
            }
            catch (Exception ex)
            {
                progressData.label = ex.ToString();
                helpText.Text = ex.ToString();
                return false;
            }
            #endregion

            #region result
            helpText.Text += Environment.NewLine + "Success, opening directory";
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = to,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            catch
            {
                helpText.Text += Environment.NewLine + "Failed to open directory";
            }
            #endregion
            return true;
        }
        #endregion

        #region CHANGE
        private bool Change(string program)
        {
            #region declare VARIABLES
            string from = string.Empty;
            string to = string.Empty;
            bool auto = false;
            string year = string.Empty;
            //DialogResult result;
            var info = SplashPatchEngine.Load.Variables(program);
            #endregion

            #region auto LOCATE
            //Check box
            if (manualCheck.Checked == false)
            {
                #region Locate()
                try
                {
                    var located = Actions.Locate(info.directory, info.files);
                    from = located.location;
                    year = located.version;
                    auto = true;
                    helpText.Text = String.Format("located {0} at \"{1}\"", program, from);
                }
                catch (FailedAutoLocate)
                {
                    //it's ok, we handled it.
                    from = string.Empty;
                    helpText.Text = String.Format("Failed to auto-locate {0}, please loacte it manually now:", program);
                }
                #endregion
            }
            #endregion

            #region manual LOCATE
            if (!auto)
            {
                #region FOLDER DIALOG
                folderDialog.SelectedPath = Environment.SpecialFolder.ProgramFiles.ToString();
                folderDialog.UseDescriptionForTitle = true;
                folderDialog.Description = String.Format("Locate the current {0} installation", program);
                //result = ;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string loc = folderDialog.SelectedPath;
                    if (info.PNG)
                    {
                        loc += "\\PNG";
                    }
                    try
                    {
                        if (Actions.Verify(loc, info.files) != null)
                        {
                            from = loc;
                            try
                            {
                                year = Actions.Version(folderDialog.SelectedPath);
                                
                            }
                            catch (IncompatibleVersion)
                            {
                                helpText.Text = String.Format("Installation of {0} at \"{1}\" is either to old or too new for this program", program, folderDialog.SelectedPath);
                                return false;
                            }
                        }
                        else
                        {
                            helpText.Text = "error";
                            return false;
                        }
                    }
                    catch (MissingRequiredFile)
                    {
                        helpText.Text = String.Format("Invalid installation at \"{0}\" (Missing required file)", folderDialog.SelectedPath);
                        return false;
                    }
                }
                else
                {
                    helpText.Text = "dialog box closed";
                    return false;
                }
                #endregion
            }
            #endregion

            #region locate PNG
            fileDialog.Filter = "Portable Network Graphic image (PNG)|*.png";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                to = fileDialog.FileName;
            }
            else
            {
                helpText.Text = "dialog box closed";
                return false;
            }
            #endregion

            #region replace PNG (differing methods)
            try
            {
                Actions.Change(program, from, to, info.files, info.masks, year);
            }
            catch (Exception ex)
            {
                helpText.Text = ex.ToString();
                return false;
            }
            #endregion

            #region ok?
            helpText.Text += Environment.NewLine + "Success";
            return true;
            #endregion

        }
        #endregion

        private void Wizard_Load(object sender, EventArgs e)
        {
            splash.Hide();
        }

        private void disable(bool oi)
        {
            Enabled = oi;
        }

        public (bool show, string title, string label, bool dialog) progressData = (false, "loading", "loading", false);

        Progress progress = new Progress();

        //System.Threading.Timer frame;

        private void ProgressBar_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            progress = new Progress();
            progress.Text = progressData.title;
            progress.label.Text = progressData.label;
            progress.Show();
            progress.TopLevel = true;
            progressData.show = true;
            progress.Refresh();

            do
            {
                bool change = false;
                //System.Threading.Thread.Sleep(16);
                Task.Delay(16).Wait();
                if (progressData.dialog)
                {
                    progress.TopMost = false;
                } else
                {
                    progress.TopMost = true;
                }
                if (progressData.title != progress.Text)
                {
                    progress.Text = progressData.title;
                    change = true;
                }
                if (progressData.label != progress.label.Text)
                {
                    progress.label.Text = progressData.label;
                    change = true;
                }
                if (change)
                {
                    progress.Invalidate();
                    progress.Update();
                    progress.Refresh();
                }

            } while (progressData.show);
            progress.Close();

        }

    }
}
