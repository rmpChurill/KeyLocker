namespace KeyLocker.Lib
{
    using System.Drawing;
    using System.Windows.Forms;

    public class CustomDataGridViewCell : DataGridViewCell
    {
        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex, 
            DataGridViewElementStates cellState, 
            object value,
            object formattedValue,
            string errorText, 
            DataGridViewCellStyle cellStyle, 
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            graphics.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cellBounds);
        }

        public override object Clone()
        {
            return new CustomDataGridViewCell();
        }
    }
}
