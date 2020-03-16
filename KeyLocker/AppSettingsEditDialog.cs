using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace KeyLocker
{
    public partial class AppSettingsEditDialog : Form
    {
        public AppSettingsEditDialog()
        {
            this.InitializeComponent();
        }

        private void HandleEditDefaultSettingsClicked(object sender, EventArgs e)
        {
            using (var dialog = new PasswordSettingsDialog(AppSettings.Instance))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AppSettings.Instance.CopyFrom(dialog.Settings);
                    AppSettings.Instance.Save();
                }
            }
        }

        private void HandleChangePasswordClicked(object sender, EventArgs e)
        {
            Util.CreateNewPassword();
        }

        private void HandleDeleteDataClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deleting all your data irreversible. Are you sure?") == DialogResult.Yes)
            {
                Data.Instance.Entries.Clear();
                Data.Instance.Save();
            }
        }

        private void HandleExportClicked(object sender, EventArgs e)
        {
            this.saveFileDialog.AddExtension = true;
            this.saveFileDialog.DefaultExt = ".csv";
            this.saveFileDialog.Filter = "csv-Tabelle|*.csv";

            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var writer = new StreamWriter(this.saveFileDialog.FileName);

                try
                {
                    writer.WriteLine(string.Format("{0};{1};{2};{3};", nameof(Entry.Name), nameof(Entry.Login), nameof(Entry.Password), nameof(Entry.Comment)));

                    foreach (var entry in Data.Instance.Entries)
                    {
                        writer.WriteLine(string.Format("{0};{1};{2};{3};", entry.Name, entry.Login, entry.Password, entry.Comment));
                    }
                }
                catch (IOException ex)
                {
                    ErrorHandler.HandleError(ex);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }
        }

        private void HandleImportClicked(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "csv-Tabelle|*.csv";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var lines = File.ReadAllLines(this.openFileDialog1.FileName);

                for (var i = 1; i < lines.Length; i++)
                {
                    Data.Instance.Entries.Add(new Entry(lines[i]));
                }
            }
        }

        private void HandleBackupClicked(object sender, EventArgs e)
        {
            File.Copy(Directories.DataFile, Path.Combine(Directories.DataDir, string.Format("Backup{0}", DateTime.Now.Ticks)));
        }

        private void HandleShowDirectoryClicked(object sender, EventArgs e)
        {
            Process.Start(Directories.DataDir);
        }
    }
}
