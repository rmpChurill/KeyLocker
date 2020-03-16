using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeyLocker.Common;

namespace KeyLocker
{
    public partial class PasswordSettingsDialog : DialogBase
    {
        private PasswordSettings settings;

        public PasswordSettingsDialog(PasswordSettings settings)
        {
            this.InitializeComponent();
            this.settings = new PasswordSettings(settings);
        }

        public PasswordSettings Settings
        {
            get
            {
                return this.settings;
            }
        }

        private void OnShown(object sender, EventArgs e)
        {
            this.minLengthTextBox.DataBindings.Add(nameof(this.minLengthTextBox.Text), this.settings, nameof(this.settings.MinLength));
            this.maxLengthTextBox.DataBindings.Add(nameof(this.maxLengthTextBox.Text), this.settings, nameof(this.settings.MaxLength));

            foreach (var usage in Enum.GetValues(typeof(Usage)))
            {
                this.digitsComboBox.Items.Add(usage);
                this.upperCaseCharsComboBox.Items.Add(usage);
                this.specialCharactersComboBox.Items.Add(usage);
            }

            foreach (var value in Enum.GetValues(typeof(TimeUnit)))
            {
                this.decayTimeUnitComboBox.Items.Add(value);
            }

            this.digitsComboBox.DataBindings.Add(nameof(this.digitsComboBox.SelectedItem), this.settings, nameof(this.settings.Digits));
            this.upperCaseCharsComboBox.DataBindings.Add(nameof(this.upperCaseCharsComboBox.SelectedItem), this.settings, nameof(this.settings.UpperCaseChars));
            this.specialCharactersComboBox.DataBindings.Add(nameof(this.specialCharactersComboBox.SelectedItem), this.settings, nameof(this.settings.SpecialCharacters));

            this.specialCharactersTextbox.DataBindings.Add(nameof(this.specialCharactersTextbox.Text), this.settings, nameof(this.settings.SpecialCharacters));
            this.forbiddenCharactersCheckbox.DataBindings.Add(nameof(this.forbiddenCharactersCheckbox.Text), this.settings, nameof(this.settings.ForbiddenCharacters));
            this.decayTimeNumericUpDown.DataBindings.Add(nameof(this.decayTimeNumericUpDown.Value), this.settings, nameof(this.settings.DecayTime));
            this.decayTimeUnitComboBox.DataBindings.Add(nameof(this.decayTimeUnitComboBox.SelectedItem), this.settings, nameof(this.settings.DecayTimeUnit));
            this.oldPwWarningCheckBox.DataBindings.Add(nameof(this.oldPwWarningCheckBox.Checked), this.settings, nameof(this.settings.WarnIfOld));
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            this.settings.CopyFrom(new PasswordSettings());
        }

        private class UsageWrapper
        {
            private Usage value;

            internal UsageWrapper(Usage value)
            {
                this.value = value;
            }

            public Usage Value
            {
                get
                {
                    return this.value;
                }
            }

            public string Text
            {
                get
                {
                    return this.value.ToString();
                }
            }
        }
    }
}
