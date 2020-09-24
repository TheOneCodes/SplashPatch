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
using System.Threading;
using System.Windows.Threading;
#endregion

namespace SplashPatch
{
    public partial class Wizard : Form
    {
        #region initialize
        Form splash = new Splash();
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
            foreach((string display, string full, string shorten, string helpText) i in SplashPatchEngine.Load.List())
            {
                appSelect.Items.Add(i.display);
            }
            #endregion
            Application.DoEvents();
            if (Control.ModifierKeys != Keys.Shift)
                System.Threading.Thread.Sleep(1000);

            splash.Text = "Loading";
            helpText.Visible = false;
            Height = 110;
            MaximumSize = new System.Drawing.Size(99999,110);
            #region set appSelect DEFAULT
            appSelect.Text = "Photoshop";
            Application.DoEvents();
            if (Control.ModifierKeys != Keys.Shift)
                System.Threading.Thread.Sleep(500);
            #endregion
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            splash.Hide();
        }
        #endregion

        #region close
        private void Wizard_Closing(object sender, FormClosingEventArgs e)
        {
            if (!Enabled)
            {
                e.Cancel = true;
            }
        }

        private void Wizard_Closed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
        #endregion

        #region change INDEX OF appSelect
        private void appSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region long SWITCH CASE

            foreach((string display, string full, string shorten, string helpText) i in SplashPatchEngine.Load.List())
            {
                if (appSelect.Text == i.display)
                {
                    helpText.Text = i.helpText;
                }
            }
            if (helpText.Text == "")
            {
                MessageBox.Show("mama mia");
            }
            
            /*
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
            */
            #endregion
        }
        #endregion

        #region click SAVE button
        private void saveButton_Click(object sender, EventArgs e)
        {
            #region try Save
            try
            {
                progressData.show = true;
                Thread thread = new Thread(() =>
                {
                    progress = new ProgressWPF();
                    progressData.Change += progress.ChangeStuff;
                    progress.Show();
                    Dispatcher.Run();
                });
                thread.IsBackground = false;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                Enabled = false;
                Save(appSelect.Text);
            }
            #endregion

            #region ouch
            catch (Exception ex)
            {
                helpText.Text = ex.ToString();
            }
            #endregion
            Enabled = true;
            progressData.show = false;
            progressData.Change -= progress.ChangeStuff;
        }
        #endregion

        #region click CHANGE button
        private void changeButton_Click(object sender, EventArgs e)
        {
            #region try CHANGE
            try
            {
                progressData.show = true;
                Thread thread = new Thread(() =>
                {
                    progress = new ProgressWPF();
                    progressData.Change += progress.ChangeStuff;
                    progress.Show();
                    Dispatcher.Run();
                });
                thread.IsBackground = false;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                Enabled = false;
                Change(appSelect.Text);
            }
            #endregion

            #region ouch
            catch (Exception ex)
            {
                helpText.Text = ex.ToString();
            }
            #endregion
            Enabled = true;
            progressData.show = false;
            progressData.Change -= progress.ChangeStuff;
        }
        #endregion

        #region SAVE
        bool Save(string program)
        {
            progressData.title = program + " Save";

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
                    progressData.label = String.Format("{0}Attempting to auto-locate {1}",Environment.NewLine, program);
                    var located = Actions.Locate(info.directory, info.files);
                    from = located.location;
                    year = located.version;
                    auto = true;
                    progressData.label = String.Format("located {1} at {0}\"{2}\"", Environment.NewLine, program, from);
                    helpText.Text = String.Format("located {0} at \"{1}\"", program, from);
                }
                catch (FailedAutoLocate)
                {
                    //it's ok, we handled it.
                    from = string.Empty;
                    helpText.Text = String.Format("Failed to auto-locate {1}{0}please loacte it manually now", Environment.NewLine, program);
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
                progressData.label = String.Format("Please select the current {0} installation location", program);
                folderDialog.Description = String.Format("Locate the current {0} installation", program);
                //result = ;
                progressData.dialog = true;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    progressData.label = String.Format("Verifying selection for required {0} files", program);
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
                                progressData.label = String.Format("Installation of {1} at{0}\"{2}\"{0}is either to old or too new for this program", Environment.NewLine, program, folderDialog.SelectedPath);
                                helpText.Text = String.Format("Installation of {0} at \"{1}\" is either to old or too new for this program", program, folderDialog.SelectedPath);
                                return false;
                            }
                        }
                    }
                    catch (MissingRequiredFile)
                    {
                        progressData.label = String.Format("Invalid installation at{0}\"{1}\"{0}(Missing required file)", Environment.NewLine, folderDialog.SelectedPath);
                        helpText.Text = String.Format("Invalid installation at \"{0}\" (Missing required file)", folderDialog.SelectedPath);
                        return false;
                    }
                }
                else
                {
                    progressData.dialog = false;
                    progressData.label = "Dialog box closed";
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
            progressData.label = String.Format("Please select where to save the currently applied {0} splash images", program);

            progressData.dialog = true;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                progressData.dialog = false;
                to = folderDialog.SelectedPath;
                helpText.Text = String.Format("Saving {0} splash to \"{1}\"", program, to);
            }
            else
            {
                progressData.dialog = false;
                progressData.label = "Dialog box closed";
                helpText.Text = "dialog box closed";
                return false;
            }
            #endregion

            #region "program".Save()
            try
            {
                progressData.label = String.Format("Saving {1} splash to{0}\"{2}\"{0}please wait", Environment.NewLine, program, to);
                Actions.Save(program, from, to, info.files, info.masks, year);
            }
            catch (Exception ex)
            {
                progressData.label = String.Format("{1} splash save failed!", Environment.NewLine, program);
                progressData.label = ex.ToString();
                helpText.Text = ex.ToString();
                return false;
            }
            #endregion

            #region result
            progressData.label = String.Format("{0}Success, opening directory", Environment.NewLine);
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
                progressData.label = String.Format("{0}Failed to open directory", Environment.NewLine);
                helpText.Text += Environment.NewLine + "Failed to open directory";
            }
            #endregion
            return true;
        }
        #endregion

        #region CHANGE
        private bool Change(string program)
        {
            progressData.title = program + " Change";

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
                    progressData.label = String.Format("Attempting to auto-locate {0}", program);
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
                progressData.label = String.Format("Please select the current {0} installation location", program);
                folderDialog.Description = String.Format("Locate the current {0} installation", program);
                //result = ;
                progressData.dialog = true;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    progressData.dialog = false;
                    progressData.label = String.Format("Verifying selection for required {0} files", program);
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
                    progressData.label = "Dialog box closed";
                    helpText.Text = "dialog box closed";
                    return false;
                }
                #endregion
            }
            #endregion

            #region locate PNG
            fileDialog.Filter = "Portable Network Graphic image (PNG)|*.png";
            fileDialog.RestoreDirectory = true;

            progressData.label = String.Format("Please select a PNG for {0}'s splash image", program);

            progressData.dialog = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                progressData.dialog = false;
                to = fileDialog.FileName;
                helpText.Text = String.Format("Replacing {1} splash with{0}\"{2}\"", Environment.NewLine, program, to);
            }
            else
            {
                progressData.dialog = false;
                progressData.label = "Dialog box closed";
                helpText.Text = "dialog box closed";
                return false;
            }
            #endregion

            #region replace PNG (differing methods)
            try
            {
                progressData.label = String.Format("Changing {1} splash to{0}\"{2}\"{0} please wait", Environment.NewLine, program, to);
                Actions.Change(program, from, to, info.files, info.masks, year);
            }
            catch (Exception ex)
            {
                progressData.label = String.Format("{0}{1} splash change failed!", Environment.NewLine, program, to);
                progressData.label = ex.ToString();
                helpText.Text = ex.ToString();
                return false;
            }
            #endregion

            #region ok?
            progressData.label = String.Format("{0}{1} splash change success!", Environment.NewLine, program);
            helpText.Text += Environment.NewLine + "Success";
            return true;
            #endregion
        }
        #endregion

        #region ProgressBar
        public static progressData progressData = new progressData();
        ProgressWPF progress = new ProgressWPF();
        #endregion

        #region About
        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not yet implemented");
        }
        #endregion

        private void linkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (helpText.Visible)
            {
                helpText.Visible = false;
                MaximumSize = new System.Drawing.Size(99999, 110);
                MinimumSize = new System.Drawing.Size(400, 110);
                Height = 110;
                linkMore.Text = "Show More";
            } else
            {
                helpText.Visible = true;
                MaximumSize = new System.Drawing.Size(99999, 999999);
                MinimumSize = new System.Drawing.Size(400, 200);
                Height = 393;
                linkMore.Text = "Show Less";
            }
        }
    }

    #region progressData
    public class progressData
    {
        #region NO NULL!!!!!!
        public progressData()
        {
            Change += Bruh;
        }

        public void Bruh(object sender, EventArgs e)
        {
            //do fuck all (basically a catch all)
        }
        #endregion

        #region EVENT
        public event EventHandler Change;
        #endregion

        #region VARIABLE
        private (bool show, string title, string label, bool dialog) _progressData = (false, "loading", "loading", false);
        #endregion

        #region GET/SETS
        public bool show
        {
            get
            {
                return _progressData.show;
            }

            set
            {
                _progressData.show = value;
                Change(this, EventArgs.Empty);
            }
        }
        public string title
        {
            get
            {
                return _progressData.title;
            }

            set
            {
                _progressData.title = value;
                Change(this, EventArgs.Empty);
            }
        }
        public string label
        {
            get
            {
                return _progressData.label;
            }

            set
            {
                _progressData.label = value;
                Change(this, EventArgs.Empty);
            }
        }
        public bool dialog
        {
            get
            {
                return _progressData.dialog;
            }

            set
            {
                _progressData.dialog = value;
                Change(this, EventArgs.Empty);
            }
        }
        #endregion
    }
    #endregion
}