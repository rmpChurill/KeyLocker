namespace KeyLocker.Utility
{
    using System;

    /// <summary>
    /// Argumente für ein Event, dass die Änderung einer Eigenschaft beschreibt.
    /// </summary>
    public class PropertyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft.</param>
        public PropertyChangedEventArgs(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Der Name der geänderten Eigenschaft.
        /// </summary>
        public string PropertyName
        {
            get;
        }
    }
}
