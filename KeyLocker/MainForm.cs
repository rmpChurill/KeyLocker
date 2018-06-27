using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace KeyLocker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Data.Instance.DataChanged += this.HandleDataChanged;
        }

        private void HandleDataChanged()
        {
            this.RefreshDisplayedData();
        }

        private void RefreshDisplayedData()
        {
            this.entryDataGridView.DataSource = null;

            if (string.IsNullOrEmpty(this.searchTextBox.Text))
            {
                Data.Instance.RemoveFilter();
            }
            else
            {
                Data.Instance.ApplyFilter(new EntryNameFilter(this.searchTextBox.Text));
            }

            this.entryDataGridView.ClearSelection();
            this.entryDataGridView.Rows.Clear();
            this.entryDataGridView.DataSource = new BindingList<Entry>(Data.Instance.FilteredEntries);
            this.entryDataGridView.Refresh();
        }

        private IList<Entry> Filter(IList<Entry> entries)
        {
            if (string.IsNullOrEmpty(this.searchTextBox.Text))
            {
                return entries;
            }

            var res = new List<Entry>(entries.Count);

            foreach (var entry in entries)
            {
                if (entry.Name.ToLower().Contains(this.searchTextBox.Text.ToLower()))
                {
                    res.Add(entry);
                }
            }

            return res;
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
                Data.Instance.Load();
                Data.Instance.Check();
            }
            else
            {
                this.Close();
            }
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            Data.Instance.Save();
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
                    Data.Instance.Entries.Add(dialog.Entry);
                    this.HandleDataChanged();
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
                Data.Instance.Entries.RemoveAt(index);
                this.HandleDataChanged();
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
                MessageBox.Show(Data.Instance.Entries[index].Password);
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
                using (var dialog = new EditDialog(Data.Instance.FilteredEntries[index]))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Data.Instance.Entries[index] = dialog.Entry;
                        this.HandleDataChanged();
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
                Clipboard.SetText(Data.Instance.Entries[index].Password);
            }
        }

        private bool ValidSelection(out int index)
        {
            if (this.entryDataGridView.SelectedRows.Count == 1)
            {
                index = this.entryDataGridView.SelectedRows[0].Index;
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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }

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
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
            {

            }
        }

        private void HandleSearchTextChanged(object sender, EventArgs e)
        {
            this.HandleDataChanged();
        }

        private void HandleSearchTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.HandleEdit();
            }
        }
    }
}
