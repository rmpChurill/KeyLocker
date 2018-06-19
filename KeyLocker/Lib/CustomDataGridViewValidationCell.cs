namespace KeyLocker.Lib
{
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class CustomDataGridViewValidationCell : DataGridViewTextBoxCell
    {
        private const int ValidationIconSize = 16;

        private IValidationItem[] lastValidationState;

        public CustomDataGridViewValidationCell()
        {
        }

        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseMove(e);

            var offset = (this.OwningRow.Height - ValidationIconSize) / 2;

            if (this.lastValidationState != null &&
                e.Y >= offset &&
                e.Y <= offset + ValidationIconSize &&
                e.X > 0 &&
                e.X < this.lastValidationState.Length * ValidationIconSize)
            {
                this.ToolTipText = this.lastValidationState[e.X / ValidationIconSize].Description;
                this.DataGridView.ShowCellToolTips = true;
            }
        }

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

            if (value != null && value is IElementValidator validator)
            {
                var validationItems = validator.Validate();

                var xOffset = 0;
                var yOffset = (cellBounds.Height - ValidationIconSize) / 2;

                foreach (var validationItem in validationItems)
                {
                    graphics.DrawImage(validationItem.Icon, cellBounds.X + xOffset, cellBounds.Y + yOffset, ValidationIconSize, ValidationIconSize);
                    xOffset += ValidationIconSize;
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
