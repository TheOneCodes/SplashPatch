namespace SplashPatch
{
    partial class Wizard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wizard));
            this.appSelect = new System.Windows.Forms.ComboBox();
            this.appLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.helpText = new System.Windows.Forms.TextBox();
            this.manualCheck = new System.Windows.Forms.CheckBox();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.AboutButton = new System.Windows.Forms.Button();
            this.progressUpdater = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // appSelect
            // 
            this.appSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appSelect.FormattingEnabled = true;
            this.appSelect.Location = new System.Drawing.Point(98, 12);
            this.appSelect.Name = "appSelect";
            this.appSelect.Size = new System.Drawing.Size(227, 23);
            this.appSelect.Sorted = true;
            this.appSelect.TabIndex = 0;
            this.appSelect.SelectedIndexChanged += new System.EventHandler(this.appSelect_SelectedIndexChanged);
            // 
            // appLabel
            // 
            this.appLabel.Location = new System.Drawing.Point(12, 12);
            this.appLabel.Name = "appLabel";
            this.appLabel.Size = new System.Drawing.Size(80, 23);
            this.appLabel.TabIndex = 1;
            this.appLabel.Text = "Select App";
            this.appLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveButton.Location = new System.Drawing.Point(12, 325);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changeButton.Location = new System.Drawing.Point(331, 325);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(75, 23);
            this.changeButton.TabIndex = 2;
            this.changeButton.Text = "Change";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // helpText
            // 
            this.helpText.AcceptsReturn = true;
            this.helpText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpText.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.helpText.Location = new System.Drawing.Point(12, 41);
            this.helpText.Multiline = true;
            this.helpText.Name = "helpText";
            this.helpText.ReadOnly = true;
            this.helpText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.helpText.Size = new System.Drawing.Size(394, 278);
            this.helpText.TabIndex = 3;
            this.helpText.Text = "This is where the help text goes";
            // 
            // manualCheck
            // 
            this.manualCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.manualCheck.Location = new System.Drawing.Point(94, 325);
            this.manualCheck.Name = "manualCheck";
            this.manualCheck.Size = new System.Drawing.Size(231, 23);
            this.manualCheck.TabIndex = 4;
            this.manualCheck.Text = "manual path";
            this.manualCheck.UseVisualStyleBackColor = true;
            // 
            // fileDialog
            // 
            this.fileDialog.DefaultExt = "png";
            // 
            // AboutButton
            // 
            this.AboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutButton.Location = new System.Drawing.Point(331, 12);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(75, 23);
            this.AboutButton.TabIndex = 5;
            this.AboutButton.Text = "About";
            this.AboutButton.UseVisualStyleBackColor = true;
            // 
            // progressUpdater
            // 
            this.progressUpdater.WorkerSupportsCancellation = true;
            this.progressUpdater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ProgressBar_DoWork);
            // 
            // Wizard
            // 
            this.ClientSize = new System.Drawing.Size(418, 360);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.manualCheck);
            this.Controls.Add(this.helpText);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.appLabel);
            this.Controls.Add(this.appSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(290, 190);
            this.Name = "Wizard";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashPatch";
            this.Load += new System.EventHandler(this.Wizard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label appLabel;
        private System.Windows.Forms.ComboBox appSelect;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.TextBox helpText;
        private System.Windows.Forms.CheckBox manualCheck;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button AboutButton;
        private System.ComponentModel.BackgroundWorker progressUpdater;
    }
}

