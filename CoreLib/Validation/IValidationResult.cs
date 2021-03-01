namespace KeyLocker.CoreLib.Validation
{
    /// <summary>
    /// Stellt das Ergebnis einer Validierung dar.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// Holt die Beschreibung des Validierungsergebnisses.
        /// </summary>
        public string Description
        {
            get;
        }
    }
}
