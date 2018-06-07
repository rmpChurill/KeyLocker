namespace KeyLocker.Lib
{
    using System.Windows.Forms;

    public class CustomDataGridViewCellColumn : DataGridViewColumn
    {
        public CustomDataGridViewCellColumn(string displayName)
        {
            this.CellTemplate = new CustomDataGridViewCell();
            this.HeaderCell = new CustomDataGridViewColumnHeaderCell(displayName);
        }
    }
}
