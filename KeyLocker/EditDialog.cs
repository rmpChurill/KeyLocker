using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeyLocker.Common;

namespace KeyLocker
{
    public partial class EditDialog : DialogBase
    {
        private Entry entry;

        public EditDialog() :
            this(null)
        { }

        public EditDialog(Entry entry)
        {
            InitializeComponent();

            if (entry == null)
            {
                this.entry = new Entry();
            }
            else
            {
                this.entry = new Entry(entry);
            }
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
            this.passwordTextBox.PasswordChar = this.showCharsCheckBox.Checked ? default(char) : '*';
            this.validationTextBox.Enabled = this.validatePasswordCheckBox.Checked;

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
            var pw = RandomGenerator.Generate();
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
            if(this.validatePasswordCheckBox.Checked)
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

            if(messages.Count == 0)
            {
                messages.Add("Alles Ok");
            }

            this.validationTextBox.Lines = messages.ToArray();
        }

        private void ValidateLength(string text, List<string> messages)
        {
            if(text.Length < Settings.Instance.MinLength)
            {
                messages.Add(string.Format("The password must be at least {0} characters long!", Settings.Instance.MinLength));
            }

            if (text.Length >= Settings.Instance.MaxLength)
            {
                messages.Add(string.Format("The password must not be longer than {0} characters!", Settings.Instance.MaxLength));
            }
        }

        private void ValidateSpecialCharacters(string text, List<string> messages)
        {
            if(Settings.Instance.SpecialCharacters == Usage.Forbid)
            {
                for(var i = 0; i < text.Length; i++)
                {
                    if(Definitions.SpecialCharacters.IndexOf(text[i]) != -1)
                    {
                        messages.Add(string.Format("Must not contain special character '{0}'!", text[i]));
                    }
                }
            }
            else if(Settings.Instance.SpecialCharacters == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.SpecialCharacters.ToCharArray()) == -1)
                {
                    messages.Add(string.Format("Missing special character! ({0})", Definitions.SpecialCharacters));
                }
            }
        }

        private void ValidateUpperCaseCharacters(string text, List<string> messages)
        {
            if (Settings.Instance.UpperCaseChars == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.UpperCaseChars.IndexOf(text[i]) != -1)
                    {
                        messages.Add(string.Format("Must not contain uppercase character '{0}'!", text[i]));
                    }
                }
            }
            else if (Settings.Instance.UpperCaseChars == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.UpperCaseChars.ToCharArray()) == -1)
                {
                    messages.Add(string.Format("Missing uppercase character! ({0})", Definitions.UpperCaseChars));
                }
            }
        }

        private void ValidateDigits(string text, List<string> messages)
        {
            if (Settings.Instance.Digits == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.Digits.IndexOf(text[i]) != -1)
                    {
                        messages.Add(string.Format("Must not contain digit '{0}'!", text[i]));
                    }
                }
            }
            else if (Settings.Instance.Digits == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.Digits.ToCharArray()) == -1)
                {
                    messages.Add(string.Format("Missing digit! ({0})", Definitions.Digits));
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
    }
}
