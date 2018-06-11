namespace KeyLocker.Lib
{
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class CustomDataGridViewValidationCell : DataGridViewTextBoxCell
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

            if (this.State == DataGridViewElementStates.Visible && this.Value != null)
            {
                var validationItems = ((IElementValidator)this.Value).Validate();

                var xOffset = 0;

                foreach (var validationItem in validationItems)
                {
                    graphics.DrawImage(validationItem.Icon, xOffset, clipBounds.Y);
                }
            }
        }

        public override object Clone()
        {
            var baseClone = base.Clone();
            var res = new CustomDataGridViewValidationCell();

            foreach (var property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (property.GetMethod != null && property.SetMethod != null)
                {
                    property.SetValue(res, property.GetValue(baseClone));
                }
            }

            foreach (var field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic))
            {
                field.SetValue(res, field.GetValue(baseClone));
            }

            return res;
        }
    }
}
