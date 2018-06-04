using System.Windows.Forms;

namespace KeyLocker.Common
{
    public class DialogBase : Form
    {
        public DialogBase()
        {
            this.KeyUp += this.HandleKeyUp;
        }

        protected virtual void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                e.Handled = true;
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                e.Handled = true;
                this.Close();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DialogBase
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "DialogBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }
    }
}
