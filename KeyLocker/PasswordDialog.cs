using KeyLocker.Common;

namespace KeyLocker
{
    public partial class PasswordDialog : DialogBase
    {
        public PasswordDialog()
        {
            InitializeComponent();
        }

        public string HashedPassword
        {
            get
            {
                return Util.ComputeSaltedHash(this.passwordBox.Text);
            }
        }
    }
}
