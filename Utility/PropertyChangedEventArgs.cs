namespace KeyLocker.Utility
{
    using System;

    /// <summary>
    /// Argumente für ein Event, dass die Änderung einer Eigenschaft beschreibt.
    /// </summary>
    public class PropertyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Der Name der geänderten Eigenschaftn.
        /// </summary>
        public string PropertyName
        {
            get;
            init;
        }
    }
}
