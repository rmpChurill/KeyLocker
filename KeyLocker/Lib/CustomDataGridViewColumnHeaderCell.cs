using System.Windows.Forms;

namespace KeyLocker.Lib
{
    internal class CustomDataGridViewColumnHeaderCell : DataGridViewColumnHeaderCell
    {
        public CustomDataGridViewColumnHeaderCell(string displayName)
        {
            this.Value = displayName;
        }
    }
}