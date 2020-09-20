using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO.Compression;
using System.IO;

namespace SplashPatch
{
    public partial class ResourceHackerDownload : Form
    {
        public ResourceHackerDownload()
        {
            InitializeComponent();
        }

        public void SetMissing(string shorter, string longer)
        {
            missingLabel.Text = "SplashPatch has detected that \"" + shorter + "\" is missing, download " + longer + " automatically now?";
            if (shorter == "rh.dll")
            {
                downloadProgress.Style = ProgressBarStyle.Continuous;
                downloadButton.Click += new System.EventHandler(this.downloadResourceHacker);
            }
        }

        private void downloadResourceHacker(object sender, EventArgs e)
        {
            try
            {
                cancelButton.Enabled = false;
                downloadButton.Enabled = false;
                string location = Path.GetTempPath() + "SplashPatch\\RH\\rh.zip";
                if (Directory.Exists(Path.GetTempPath() + "SplashPatch\\RH"))
                {
                    Directory.Delete(Path.GetTempPath() + "SplashPatch\\RH", true);
                }
                Directory.CreateDirectory(Path.GetTempPath() + "SplashPatch\\RH");
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadResourceHackerPT2);
                webClient.DownloadFileAsync(new Uri("http://www.angusj.com/resourcehacker/resource_hacker.zip"), location);
                Text = "Downloading Resource Hacker from http://angusj.com/resourcehacker/";
                missingLabel.Text = Text;
            } catch
            {
                MessageBox.Show("Failed Download, check your network connection");
                Close();
            }
        }

        private void downloadResourceHackerPT2(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string location = Path.GetTempPath() + "SplashPatch\\RH";
                Text = "Extracting Resource Hacker";
                missingLabel.Text = Text;
                downloadProgress.Style = ProgressBarStyle.Marquee;
                downloadProgress.Value = 0;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                ZipFile.ExtractToDirectory(location + "\\rh.zip", location, true);
                Text = "Copying Resource Hacker";
                missingLabel.Text = Text;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Copy(location + "\\ResourceHacker.exe", AppDomain.CurrentDomain.BaseDirectory + "rh.dll");
                Text = "Verifying resources";
                missingLabel.Text = Text;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "rh.dll"))
                {
                    MessageBox.Show("Failed");
                    Close();
                }
                else
                {
                    Directory.Delete(Path.GetTempPath() + "SplashPatch\\RH", true);
                    DialogResult = DialogResult.OK;
                }
            } catch
            {
                MessageBox.Show("Failed Extraction");
                Close();
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgress.Value = e.ProgressPercentage;
            missingLabel.Text = Text + " " + e.ProgressPercentage + "%";
        }
    }
}
