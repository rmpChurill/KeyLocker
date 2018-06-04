using System;
using System.IO;
using System.Windows.Forms;
using KeyLocker.Common;

namespace KeyLocker
{
    public partial class SettingsDialog : DialogBase
    {
        private Settings settings;

        public SettingsDialog()
        {
            InitializeComponent();
            this.settings = new Settings(Settings.Instance);
        }

        public Settings Settings
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

            var values = Enum.GetValues(typeof(Usage));
            var objects = new object[values.Length];

            for (var i = 0; i < objects.Length; i++)
            {
                objects[i] = values.GetValue(i);
            }

            this.digitsComboBox.Items.AddRange(objects);
            this.digitsComboBox.DataBindings.Add(nameof(this.digitsComboBox.SelectedValue), this.settings, nameof(this.settings.Digits));
            this.upperCaseCharsComboBox.Items.AddRange(objects);
            this.upperCaseCharsComboBox.DataBindings.Add(nameof(this.upperCaseCharsComboBox.SelectedValue), this.settings, nameof(this.settings.UpperCaseChars));
            this.specialCharactersComboBox.Items.AddRange(objects);
            this.specialCharactersComboBox.DataBindings.Add(nameof(this.specialCharactersComboBox.SelectedValue), this.settings, nameof(this.settings.SpecialCharacters));
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            this.settings.CopyFrom(Settings.DefaultSettings);
        }

        private void HandleChangePasswordClicked(object sender, EventArgs e)
        {
            Util.CreateNewPassword();
        }

        private void HandleDeleteDataClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deleting all your data irreversible. Are you sure?") == DialogResult.Yes)
            {
                Data.Entries.Clear();
                Data.Save();
            }
        }

        private void HandleExportClicked(object sender, EventArgs args)
        {
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var writer = new StreamWriter(this.saveFileDialog.FileName);

                try
                {
                    writer.WriteLine(string.Format("{0};{1};{2};{3};", nameof(Entry.Name), nameof(Entry.Login), nameof(Entry.Password), nameof(Entry.Comment)));
                    
                    foreach(var entry in Data.Entries)
                    {
                        writer.WriteLine(string.Format("{0};{1};{2};{3};", entry.Name, entry.Login, entry.Password, entry.Comment));
                    }
                }
                catch(IOException e)
                {
                    ErrorHandler.HandleError(e);
                }
                finally
                {
                    if(writer != null)
                    {
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }
        }
    }
}
