namespace KeyLocker.Lib
{
    using System.ComponentModel;
    using System.Windows.Forms;

    public class CustomDataGridView<T> : DataGridView
    {
        public CustomDataGridView()
        {
            this.DoubleBuffered = true;

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
