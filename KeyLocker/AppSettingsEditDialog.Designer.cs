namespace KeyLocker
{
    partial class AppSettingsEditDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.backupButton = new System.Windows.Forms.Button();
            this.editDefaultSettingsButton = new System.Windows.Forms.Button();
            this.importCsvButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.deleteAllDataButton = new System.Windows.Forms.Button();
            this.changePasswordButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.showDirectoryButton = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.showDirectoryButton);
            this.groupBox2.Controls.Add(this.backupButton);
            this.groupBox2.Controls.Add(this.editDefaultSettingsButton);
            this.groupBox2.Controls.Add(this.importCsvButton);
            this.groupBox2.Controls.Add(this.exportButton);
            this.groupBox2.Controls.Add(this.deleteAllDataButton);
            this.groupBox2.Controls.Add(this.changePasswordButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 218);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User";
            // 
            // backupButton
            // 
            this.backupButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backupButton.Location = new System.Drawing.Point(6, 77);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(251, 23);
            this.backupButton.TabIndex = 12;
            this.backupButton.Text = "Backup data";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.Click += new System.EventHandler(this.HandleBackupClicked);
            // 
            // editDefaultSettingsButton
            // 
            this.editDefaultSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDefaultSettingsButton.Location = new System.Drawing.Point(6, 193);
            this.editDefaultSettingsButton.Name = "editDefaultSettingsButton";
            this.editDefaultSettingsButton.Size = new System.Drawing.Size(251, 23);
            this.editDefaultSettingsButton.TabIndex = 11;
            this.editDefaultSettingsButton.Text = "Edit default password settings";
            this.editDefaultSettingsButton.UseVisualStyleBackColor = true;
            this.editDefaultSettingsButton.Click += new System.EventHandler(this.HandleEditDefaultSettingsClicked);
            // 
            // importCsvButton
            // 
            this.importCsvButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importCsvButton.Location = new System.Drawing.Point(6, 164);
            this.importCsvButton.Name = "importCsvButton";
            this.importCsvButton.Size = new System.Drawing.Size(251, 23);
            this.importCsvButton.TabIndex = 10;
            this.importCsvButton.Text = "Import csv";
            this.importCsvButton.UseVisualStyleBackColor = true;
            this.importCsvButton.Click += new System.EventHandler(this.HandleImportClicked);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(6, 135);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(251, 23);
            this.exportButton.TabIndex = 9;
            this.exportButton.Text = "Export csv";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.HandleExportClicked);
            // 
            // deleteAllDataButton
            // 
            this.deleteAllDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAllDataButton.Location = new System.Drawing.Point(6, 48);
            this.deleteAllDataButton.Name = "deleteAllDataButton";
            this.deleteAllDataButton.Size = new System.Drawing.Size(251, 23);
            this.deleteAllDataButton.TabIndex = 8;
            this.deleteAllDataButton.Text = "Delete All Data";
            this.deleteAllDataButton.UseVisualStyleBackColor = true;
            this.deleteAllDataButton.Click += new System.EventHandler(this.HandleDeleteDataClicked);
            // 
            // changePasswordButton
            // 
            this.changePasswordButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.changePasswordButton.Location = new System.Drawing.Point(6, 19);
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.Size = new System.Drawing.Size(251, 23);
            this.changePasswordButton.TabIndex = 7;
            this.changePasswordButton.Text = "Change Password";
            this.changePasswordButton.UseVisualStyleBackColor = true;
            this.changePasswordButton.Click += new System.EventHandler(this.HandleChangePasswordClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // showDirectoryButton
            // 
            this.showDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showDirectoryButton.Location = new System.Drawing.Point(6, 106);
            this.showDirectoryButton.Name = "showDirectoryButton";
            this.showDirectoryButton.Size = new System.Drawing.Size(251, 23);
            this.showDirectoryButton.TabIndex = 13;
            this.showDirectoryButton.Text = "Show directory";
            this.showDirectoryButton.UseVisualStyleBackColor = true;
            this.showDirectoryButton.Click += new System.EventHandler(this.HandleShowDirectoryClicked);
            // 
            // AppSettingsEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 242);
            this.Controls.Add(this.groupBox2);
            this.Name = "AppSettingsEditDialog";
            this.Text = "AppSettingsEditDialog";
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button importCsvButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button deleteAllDataButton;
        private System.Windows.Forms.Button changePasswordButton;
        private System.Windows.Forms.Button editDefaultSettingsButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.Button showDirectoryButton;
    }
}