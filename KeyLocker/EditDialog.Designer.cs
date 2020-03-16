namespace KeyLocker
{
    partial class EditDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.showCharsCheckBox = new System.Windows.Forms.CheckBox();
            this.validatePasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.validationTextBox = new System.Windows.Forms.TextBox();
            this.randomPasswordButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.useDefaultSettingsCheckBox = new System.Windows.Forms.CheckBox();
            this.editCustomSettingsButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(458, 299);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(539, 299);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(6, 32);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(170, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Comment (optional)";
            // 
            // commentTextBox
            // 
            this.commentTextBox.Location = new System.Drawing.Point(6, 71);
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(170, 20);
            this.commentTextBox.TabIndex = 1;
            // 
            // showCharsCheckBox
            // 
            this.showCharsCheckBox.AutoSize = true;
            this.showCharsCheckBox.Location = new System.Drawing.Point(182, 151);
            this.showCharsCheckBox.Name = "showCharsCheckBox";
            this.showCharsCheckBox.Size = new System.Drawing.Size(102, 17);
            this.showCharsCheckBox.TabIndex = 4;
            this.showCharsCheckBox.Text = "Show Password";
            this.showCharsCheckBox.UseVisualStyleBackColor = true;
            this.showCharsCheckBox.CheckedChanged += new System.EventHandler(this.OnShowPasswordCheckedChanged);
            // 
            // validatePasswordCheckBox
            // 
            this.validatePasswordCheckBox.AutoSize = true;
            this.validatePasswordCheckBox.Checked = true;
            this.validatePasswordCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.validatePasswordCheckBox.Location = new System.Drawing.Point(6, 19);
            this.validatePasswordCheckBox.Name = "validatePasswordCheckBox";
            this.validatePasswordCheckBox.Size = new System.Drawing.Size(114, 17);
            this.validatePasswordCheckBox.TabIndex = 6;
            this.validatePasswordCheckBox.Text = "Activate Validation";
            this.validatePasswordCheckBox.UseVisualStyleBackColor = true;
            this.validatePasswordCheckBox.CheckedChanged += new System.EventHandler(this.OnValidatePasswordCheckeChanged);
            // 
            // validationTextBox
            // 
            this.validationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.validationTextBox.Location = new System.Drawing.Point(6, 42);
            this.validationTextBox.Multiline = true;
            this.validationTextBox.Name = "validationTextBox";
            this.validationTextBox.Size = new System.Drawing.Size(275, 204);
            this.validationTextBox.TabIndex = 7;
            this.validationTextBox.TabStop = false;
            // 
            // randomPasswordButton
            // 
            this.randomPasswordButton.Location = new System.Drawing.Point(6, 175);
            this.randomPasswordButton.Name = "randomPasswordButton";
            this.randomPasswordButton.Size = new System.Drawing.Size(170, 23);
            this.randomPasswordButton.TabIndex = 5;
            this.randomPasswordButton.Text = "Generate Random Password";
            this.randomPasswordButton.UseVisualStyleBackColor = true;
            this.randomPasswordButton.Click += new System.EventHandler(this.OnRandomPasswordClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.useDefaultSettingsCheckBox);
            this.groupBox1.Controls.Add(this.validationTextBox);
            this.groupBox1.Controls.Add(this.editCustomSettingsButton);
            this.groupBox1.Controls.Add(this.validatePasswordCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(327, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 281);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Validation";
            // 
            // useDefaultSettingsCheckBox
            // 
            this.useDefaultSettingsCheckBox.AutoSize = true;
            this.useDefaultSettingsCheckBox.Location = new System.Drawing.Point(196, 256);
            this.useDefaultSettingsCheckBox.Name = "useDefaultSettingsCheckBox";
            this.useDefaultSettingsCheckBox.Size = new System.Drawing.Size(85, 17);
            this.useDefaultSettingsCheckBox.TabIndex = 10;
            this.useDefaultSettingsCheckBox.Text = "Use defaults";
            this.useDefaultSettingsCheckBox.UseVisualStyleBackColor = true;
            this.useDefaultSettingsCheckBox.CheckedChanged += new System.EventHandler(this.OnUseDefaultSettingsCheckedChanged);
            // 
            // editValidationSettingsButton
            // 
            this.editCustomSettingsButton.Location = new System.Drawing.Point(6, 252);
            this.editCustomSettingsButton.Name = "editValidationSettingsButton";
            this.editCustomSettingsButton.Size = new System.Drawing.Size(184, 23);
            this.editCustomSettingsButton.TabIndex = 11;
            this.editCustomSettingsButton.Text = "Edit validation settings";
            this.editCustomSettingsButton.UseVisualStyleBackColor = true;
            this.editCustomSettingsButton.Click += new System.EventHandler(this.OnEditSettingsClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.loginTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.passwordTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nameTextBox);
            this.groupBox2.Controls.Add(this.randomPasswordButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.commentTextBox);
            this.groupBox2.Controls.Add(this.showCharsCheckBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 281);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(6, 110);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(170, 20);
            this.loginTextBox.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Login";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(6, 149);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(170, 20);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.OnPasswordChanged);
            // 
            // EditDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(626, 334);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditDialog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Shown += new System.EventHandler(this.OnShown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.CheckBox showCharsCheckBox;
        private System.Windows.Forms.CheckBox validatePasswordCheckBox;
        private System.Windows.Forms.TextBox validationTextBox;
        private System.Windows.Forms.Button randomPasswordButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button editCustomSettingsButton;
        private System.Windows.Forms.CheckBox useDefaultSettingsCheckBox;
    }
}