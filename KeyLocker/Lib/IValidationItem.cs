using System.Drawing;

namespace KeyLocker.Lib
{
    public interface IValidationItem
    {
        string Description
        {
            get;
        }

        int Severity
        {
            get;
        }

        Image Icon
        {
            get;
        }
    }
}
