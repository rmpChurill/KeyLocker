namespace KeyLocker.Lib
{
    using System.ComponentModel;
    using System.Windows.Forms;

    public class CustomDataGridView<T> : DataGridView
    {
        public CustomDataGridView()
        {
            this.DoubleBuffered = true;
            this.AllowUserToResizeRows = false;
            this.AllowUserToOrderColumns = false;
            this.RowHeadersVisible = false;
            this.AllowUserToAddRows = false;
            this.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.ColumnAdded += this.HandleColumnAdded;
        }

        private void HandleColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (typeof(IElementValidator).Equals(e.Column.ValueType))
            {
                e.Column.CellTemplate = new CustomDataGridViewValidationCell();
            }
        }
    }
}
