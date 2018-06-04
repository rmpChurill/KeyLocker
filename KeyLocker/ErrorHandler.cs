using System;
using System.Windows.Forms;

namespace KeyLocker
{
    public static class ErrorHandler
    {
        public static void HandleError(Exception e)
        {
            MessageBox.Show(string.Format("An Exception of Type {0} occurred: \n\"{1}\"", e.GetType().Name, e.Message), "Exception");
        }
    }
}
