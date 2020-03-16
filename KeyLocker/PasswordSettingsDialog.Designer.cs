namespace KeyLocker
{
    partial class PasswordSettingsDialog
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
            this.maxLengthTextBox = new System.Windows.Forms.TextBox();
            this.minLengthTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.specialCharactersComboBox = new System.Windows.Forms.ComboBox();
            this.digitsComboBox = new System.Windows.Forms.ComboBox();
            this.upperCaseCharsComboBox = new System.Windows.Forms.ComboBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.forbiddenCharactersCheckbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.decayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.oldPwWarningCheckBox = new System.Windows.Forms.CheckBox();
            this.decayTimeUnitComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.specialCharactersTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.decayTimeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // maxLengthTextBox
            // 
            this.maxLengthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maxLengthTextBox.Location = new System.Drawing.Point(9, 230);
            this.maxLengthTextBox.Name = "maxLengthTextBox";
            this.maxLengthTextBox.Size = new System.Drawing.Size(394, 20);
            this.maxLengthTextBox.TabIndex = 4;
            // 
            // minLengthTextBox
            // 
            this.minLengthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minLengthTextBox.Location = new System.Drawing.Point(6, 191);
            this.minLengthTextBox.Name = "minLengthTextBox";
            this.minLengthTextBox.Size = new System.Drawing.Size(394, 20);
            this.minLengthTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Upper case letters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Digits";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Special characters";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Min length";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Max length";
            // 
            // specialCharactersComboBox
            // 
            this.specialCharactersComboBox.AllowDrop = true;
            this.specialCharactersComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.specialCharactersComboBox.FormattingEnabled = true;
            this.specialCharactersComboBox.Location = new System.Drawing.Point(6, 112);
            this.specialCharactersComboBox.Name = "specialCharactersComboBox";
            this.specialCharactersComboBox.Size = new System.Drawing.Size(394, 21);
            this.specialCharactersComboBox.TabIndex = 2;
            // 
            // digitsComboBox
            // 
            this.digitsComboBox.AllowDrop = true;
            this.digitsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.digitsComboBox.FormattingEnabled = true;
            this.digitsComboBox.Location = new System.Drawing.Point(6, 72);
            this.digitsComboBox.Name = "digitsComboBox";
            this.digitsComboBox.Size = new System.Drawing.Size(394, 21);
            this.digitsComboBox.TabIndex = 1;
            // 
            // upperCaseCharsComboBox
            // 
            this.upperCaseCharsComboBox.AllowDrop = true;
            this.upperCaseCharsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperCaseCharsComboBox.FormattingEnabled = true;
            this.upperCaseCharsComboBox.Location = new System.Drawing.Point(6, 32);
            this.upperCaseCharsComboBox.Name = "upperCaseCharsComboBox";
            this.upperCaseCharsComboBox.Size = new System.Drawing.Size(394, 21);
            this.upperCaseCharsComboBox.TabIndex = 0;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.Location = new System.Drawing.Point(12, 412);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(406, 23);
            this.resetButton.TabIndex = 6;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.OnResetClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(343, 441);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(262, 441);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.specialCharactersTextbox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.forbiddenCharactersCheckbox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.maxLengthTextBox);
            this.groupBox1.Controls.Add(this.minLengthTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.upperCaseCharsComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.digitsComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.specialCharactersComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 298);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Password Criteria";
            // 
            // forbiddenCharactersCheckbox
            // 
            this.forbiddenCharactersCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.forbiddenCharactersCheckbox.Location = new System.Drawing.Point(6, 269);
            this.forbiddenCharactersCheckbox.Name = "forbiddenCharactersCheckbox";
            this.forbiddenCharactersCheckbox.Size = new System.Drawing.Size(394, 20);
            this.forbiddenCharactersCheckbox.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Forbidden Characters";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.decayTimeNumericUpDown);
            this.groupBox3.Controls.Add(this.oldPwWarningCheckBox);
            this.groupBox3.Controls.Add(this.decayTimeUnitComboBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 316);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(406, 90);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Password Lifetime";
            // 
            // decayTimeNumericUpDown
            // 
            this.decayTimeNumericUpDown.Location = new System.Drawing.Point(6, 32);
            this.decayTimeNumericUpDown.Name = "decayTimeNumericUpDown";
            this.decayTimeNumericUpDown.Size = new System.Drawing.Size(113, 20);
            this.decayTimeNumericUpDown.TabIndex = 9;
            // 
            // oldPwWarningCheckBox
            // 
            this.oldPwWarningCheckBox.AutoSize = true;
            this.oldPwWarningCheckBox.Checked = true;
            this.oldPwWarningCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.oldPwWarningCheckBox.Location = new System.Drawing.Point(6, 58);
            this.oldPwWarningCheckBox.Name = "oldPwWarningCheckBox";
            this.oldPwWarningCheckBox.Size = new System.Drawing.Size(98, 17);
            this.oldPwWarningCheckBox.TabIndex = 8;
            this.oldPwWarningCheckBox.Text = "Warn when old";
            this.oldPwWarningCheckBox.UseVisualStyleBackColor = true;
            // 
            // decayTimeUnitComboBox
            // 
            this.decayTimeUnitComboBox.AllowDrop = true;
            this.decayTimeUnitComboBox.FormattingEnabled = true;
            this.decayTimeUnitComboBox.Location = new System.Drawing.Point(125, 31);
            this.decayTimeUnitComboBox.Name = "decayTimeUnitComboBox";
            this.decayTimeUnitComboBox.Size = new System.Drawing.Size(69, 21);
            this.decayTimeUnitComboBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mark passwords as old after";
            // 
            // specialCharactersTextbox
            // 
            this.specialCharactersTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.specialCharactersTextbox.Location = new System.Drawing.Point(6, 152);
            this.specialCharactersTextbox.Name = "specialCharactersTextbox";
            this.specialCharactersTextbox.Size = new System.Drawing.Size(394, 20);
            this.specialCharactersTextbox.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Special Characters";
            // 
            // PasswordSettingsDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(430, 477);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen";
            this.Shown += new System.EventHandler(this.OnShown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.decayTimeNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox maxLengthTextBox;
        private System.Windows.Forms.TextBox minLengthTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox specialCharactersComboBox;
        private System.Windows.Forms.ComboBox digitsComboBox;
        private System.Windows.Forms.ComboBox upperCaseCharsComboBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox decayTimeUnitComboBox;
        private System.Windows.Forms.CheckBox oldPwWarningCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown decayTimeNumericUpDown;
        private System.Windows.Forms.TextBox forbiddenCharactersCheckbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox specialCharactersTextbox;
        private System.Windows.Forms.Label label8;
    }
}