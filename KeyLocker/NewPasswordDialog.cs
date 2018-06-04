using KeyLocker.Common;

namespace KeyLocker
{
    public partial class NewPasswordDialog : DialogBase
    {
        public NewPasswordDialog()
        {
            InitializeComponent();
        }

        public string Password
        {
            get
            {
                return this.pw2TextBox.Text;
            }
        }

        private void HandlePw1Changed(object sender, System.EventArgs e)
        {
            this.ValidateInput();
        }

        private void HandlePw2Changed(object sender, System.EventArgs e)
        {
            this.ValidateInput();
        }

        private void ValidateInput()
        {
            if(string.Equals(this.pw1TextBox.Text, this.pw2TextBox.Text))
            {
                this.errorLabel.Text = string.Empty;
                this.okButton.Enabled = true;
            }
            else
            {
                this.errorLabel.Text = "Passwords don't match";
                this.okButton.Enabled = false;
            }
        }
    }
}
