using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using KeyLocker.Common;

namespace KeyLocker
{
    public partial class MainForm : DialogBase
    {
        public MainForm()
        {
            InitializeComponent();
            Data.DataChanged += this.OnDataChanged;
        }

        private void OnDataChanged()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = new BindingList<Entry>(Data.Entries);
        }

        private void OnShown(object sender, System.EventArgs e)
        {
            var authorized = true;

            if (string.IsNullOrEmpty(Settings.Instance.SaltedPasswordHash))
            {
                authorized = Util.CreateNewPassword();
            }
            else
            {
                authorized = Util.RequestPassword();
            }

            if (authorized)
            {
                Data.Load();
            }
            else
            {
                this.Close();
            }
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            Data.Save();
            Settings.Instance.Save();
        }

        private void OnAddClicked(object sender, System.EventArgs e)
        {
            HandleAdd();
        }

        private void HandleAdd()
        {
            using (var dialog = new EditDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Data.Entries.Add(dialog.Entry);
                    this.OnDataChanged();
                }
            }
        }

        private void OnDeleteClicked(object sender, System.EventArgs e)
        {
            HandleDelete();
        }

        private void HandleDelete()
        {
            if (this.ValidSelection(out var index))
            {
                Data.Entries.RemoveAt(index);
                this.OnDataChanged();
            }
        }

        private void OnShowClicked(object sender, System.EventArgs e)
        {
            HandleShow();
        }

        private void HandleShow()
        {
            if (this.ValidSelection(out var index))
            {
                MessageBox.Show(Data.Entries[index].Password);
            }
        }

        private void OnEditClicked(object sender, System.EventArgs e)
        {
            HandleEdit();
        }

        private void HandleEdit()
        {
            if (this.ValidSelection(out var index))
            {
                using (var dialog = new EditDialog(Data.Entries[index]))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Data.Entries[index] = dialog.Entry;
                        this.OnDataChanged();
                    }
                }
            }
        }

        private void OnCopyClicked(object sender, System.EventArgs e)
        {
            HandleCopy();
        }

        private void HandleCopy()
        {
            if (this.ValidSelection(out var index))
            {
                Clipboard.SetText(Data.Entries[index].Password);
            }
        }

        private bool ValidSelection(out int index)
        {
            if (this.dataGridView1.SelectedRows.Count == 1)
            {
                index = this.dataGridView1.SelectedRows[0].Index;
                return true;
            }

            index = -1;
            return false;
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            using (var dialog = new SettingsDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.Instance.CopyFrom(dialog.Settings);
                    Settings.Instance.Save();
                }
            }
        }

        private void OnCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.HandleEdit();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.HandleEdit();
                    break;
                case Keys.F2:
                    this.HandleEdit();
                    break;
                case Keys.F3:
                    HandleShow();
                    break;
                case Keys.Delete:
                    HandleDelete();
                    break;
                case Keys.C:
                    if (e.Modifiers == Keys.Control)
                    {
                        HandleCopy();
                    }
                    break;
                default:
                    break;
            }
        }

        private void HandleCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
            {

            }
        }
    }
}
