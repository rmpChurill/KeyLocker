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
            this.InitializeComponent();
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
            bool authorized;
            if (string.IsNullOrEmpty(AppSettings.Instance.SaltedPasswordHash))
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
            AppSettings.Instance.Save();
        }

        private void OnAddClicked(object sender, System.EventArgs e)
        {
            this.HandleAdd();
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
            this.HandleDelete();
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
            this.HandleShow();
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
            this.HandleEdit();
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
            this.HandleCopy();
        }

        private void HandleCopy()
        {
            if (this.ValidSelection(out var index))
            {
                Clipboard.SetText(Data.Instance.FilteredEntries[index].Password);
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
            using(var dialog = new AppSettingsEditDialog())
            {
                dialog.ShowDialog();
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

            var handled = true;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.HandleEdit();
                    break;
                case Keys.F2:
                    this.HandleEdit();
                    break;
                case Keys.F3:
                    this.HandleShow();
                    break;
                case Keys.Delete:
                    this.HandleDelete();
                    break;
                case Keys.C:
                    if (e.Modifiers == Keys.Control)
                    {
                        this.HandleCopy();
                    }
                    else
                    {
                        handled = false;
                    }
                    break;
                default:
                    handled = false;
                    break;
            }

            e.Handled = handled;
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
            if (e.KeyCode == Keys.Enter)
            {
                this.HandleEdit();
            }
        }

        private void OutdatedFilterChecked(object sender, EventArgs e)
        {

        }

        private void WeakFilterChecked(object sender, EventArgs e)
        {

        }

        private void UnfittingFilterChecked(object sender, EventArgs e)
        {

        }
    }
}
