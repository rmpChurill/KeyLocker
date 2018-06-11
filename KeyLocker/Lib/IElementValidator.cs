using System.Collections.Generic;

namespace KeyLocker.Lib
{
    public interface IElementValidator
    {
        IValidationItem[] Validate();
    }
}
