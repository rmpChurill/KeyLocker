namespace KeyLocker.Lib
{
    using System.ComponentModel;
    using System.Windows.Forms;

    public class CustomDataGridView<T> : DataGridView
    {
        public CustomDataGridView()
        {
            this.DoubleBuffered = true;

            foreach (var property in typeof(T).GetProperties())
            {
                var browseableAttribues = property.GetCustomAttributes(typeof(BrowsableAttribute), true);

                if (browseableAttribues.Length == 0 || ((BrowsableAttribute)browseableAttribues[0]).Browsable)
                {
                    var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    var displayName = displayNameAttributes.Length != 0 ? ((DisplayNameAttribute)displayNameAttributes[0]).DisplayName : property.Name;

                    this.Columns.Add(new CustomDataGridViewCellColumn(displayName));
                }
            }
        }
    }
}
