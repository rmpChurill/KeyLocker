using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeyLocker.Common;

namespace KeyLocker
{
    public partial class EditDialog : DialogBase
    {
        private Entry entry;
        private string originalPassword;

        public EditDialog() :
            this(null)
        { }

        public EditDialog(Entry entry)
        {
            this.InitializeComponent();

            if (entry == null)
            {
                this.entry = new Entry();
            }
            else
            {
                this.entry = new Entry(entry);
            }

            this.originalPassword = this.entry.Password;

            this.SetFormState();
        }

        public Entry Entry
        {
            get
            {
                return this.entry;
            }
        }

        private void SetFormState()
        {
            this.useDefaultSettingsCheckBox.Checked = !this.entry.HasCustomSettings;
            this.passwordTextBox.PasswordChar = this.showCharsCheckBox.Checked ? default : '*';
            this.validationTextBox.Enabled = this.validatePasswordCheckBox.Checked;
            this.editCustomSettingsButton.Enabled = !this.useDefaultSettingsCheckBox.Checked;

            if (this.validatePasswordCheckBox.Checked)
            {
                this.ValidatePassword();
            }
            else
            {
                this.validationTextBox.Clear();
            }
        }

        private void OnRandomPasswordClicked(object sender, EventArgs e)
        {
            var pw = RandomGenerator.Generate(this.entry.ApplicableSettings);
            this.passwordTextBox.Text = pw;
            this.entry.Password = pw;
        }

        private void OnShowPasswordCheckedChanged(object sender, EventArgs e)
        {
            this.SetFormState();
        }

        private void OnValidatePasswordCheckeChanged(object sender, EventArgs e)
        {
            this.SetFormState();
        }

        private void OnPasswordChanged(object sender, EventArgs e)
        {
            if (this.validatePasswordCheckBox.Checked)
            {
                this.ValidatePassword();
            }
        }

        private void ValidatePassword()
        {
            var messages = new List<string>();

            this.ValidateLength(this.passwordTextBox.Text, messages);
            this.ValidateDigits(this.passwordTextBox.Text, messages);
            this.ValidateUpperCaseCharacters(this.passwordTextBox.Text, messages);
            this.ValidateSpecialCharacters(this.passwordTextBox.Text, messages);
            this.ValidateForbiddenDigits(this.passwordTextBox.Text, messages);

            if (messages.Count == 0)
            {
                messages.Add("Alles Ok");
            }

            this.validationTextBox.Lines = messages.ToArray();
        }

        private void ValidateLength(string text, List<string> messages)
        {
            if (text.Length < this.entry.ApplicableSettings.MinLength)
            {
                messages.Add(string.Format("The password must be at least {0} characters long!", this.entry.ApplicableSettings.MinLength));
            }

            if (text.Length >= this.entry.ApplicableSettings.MaxLength)
            {
                messages.Add(string.Format("The password must not be longer than {0} characters!", this.entry.ApplicableSettings.MaxLength));
            }
        }

        private void ValidateSpecialCharacters(string text, List<string> messages)
        {
            if (this.entry.ApplicableSettings.SpecialCharacters == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.SpecialCharacters.IndexOf(text[i]) != -1)
                    {
                        messages.Add(string.Format("Must not contain special character '{0}'!", text[i]));
                    }
                }
            }
            else if (this.entry.ApplicableSettings.SpecialCharacters == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.SpecialCharacters.ToCharArray()) == -1)
                {
                    messages.Add(string.Format("Missing special character! ({0})", Definitions.SpecialCharacters));
                }
            }
        }

        private void ValidateUpperCaseCharacters(string text, List<string> messages)
        {
            if (this.entry.ApplicableSettings.UpperCaseChars == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.UpperCaseChars.IndexOf(text[i]) != -1)
                    {
                        messages.Add(string.Format("Must not contain uppercase character '{0}'!", text[i]));
                    }
                }
            }
            else if (this.entry.ApplicableSettings.UpperCaseChars == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.UpperCaseChars.ToCharArray()) == -1)
                {
                    messages.Add(string.Format("Missing uppercase character! ({0})", Definitions.UpperCaseChars));
                }
            }
        }

        private void ValidateDigits(string text, List<string> messages)
        {
            if (this.entry.ApplicableSettings.Digits == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.Digits.IndexOf(text[i]) != -1)
                    {
                        messages.Add(string.Format("Must not contain digit '{0}'!", text[i]));
                    }
                }
            }
            else if (this.entry.ApplicableSettings.Digits == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.Digits.ToCharArray()) == -1)
                {
                    messages.Add(string.Format("Missing digit! ({0})", Definitions.Digits));
                }
            }
        }

        private void ValidateForbiddenDigits(string text, List<string> messages)
        {
            if (this.entry.ApplicableSettings.ForbiddenCharacters.Length > 0)
            {
                foreach (var c in this.entry.ApplicableSettings.ForbiddenCharacters)
                {
                    if (text.IndexOf(c) >= 0)
                    {
                        messages.Add(string.Format("Forbidden digit found ({0})", c));
                    }
                }
            }
        }

        private void OnShown(object sender, EventArgs e)
        {
            this.passwordTextBox.DataBindings.Add(nameof(this.passwordTextBox.Text), this.entry, nameof(this.entry.Password));
            this.nameTextBox.DataBindings.Add(nameof(this.nameTextBox.Text), this.entry, nameof(this.entry.Name));
            this.commentTextBox.DataBindings.Add(nameof(this.commentTextBox.Text), this.entry, nameof(this.entry.Comment));
            this.loginTextBox.DataBindings.Add(nameof(this.loginTextBox.Text), this.entry, nameof(this.entry.Login));
            this.SetFormState();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            if (this.entry.Password != this.originalPassword)
            {
                this.entry.Date = DateTime.Now;
            }
        }

        private void OnUseDefaultSettingsCheckedChanged(object sender, EventArgs e)
        {
            if (this.useDefaultSettingsCheckBox.Checked)
            {
                this.UseDefaultSettings();
            }
            else
            {
                this.UseCustomSettings();
            }

            this.SetFormState();
        }

        private void OnEditSettingsClicked(object sender, EventArgs e)
        {
            this.UseCustomSettings();

            using (var dialog = new PasswordSettingsDialog(this.entry.CustomSettings))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.entry.CustomSettings.CopyFrom(dialog.Settings);
                }
            }
        }

        private void UseCustomSettings()
        {
            if (!this.entry.HasCustomSettings)
            {
                this.entry.CustomSettings = new PasswordSettings(AppSettings.Instance);
            }
        }

        private void UseDefaultSettings()
        {
            if (this.entry.HasCustomSettings)
            {
                this.entry.CustomSettings = null;
            }
        }
    }
}
