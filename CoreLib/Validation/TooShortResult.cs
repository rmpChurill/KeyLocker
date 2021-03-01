namespace KeyLocker.CoreLib.Validation
{
    /// <summary>
    /// Eine Implementierung von <see cref="IValidationResult"/>, die anzeigt, dass das Passwort zu kurz ist.
    /// </summary>
    public class TooShortResult : IValidationResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="min">Die minimale Länge für das Passwort.</param>
        /// <param name="actual">Die tatsächliche Länge des Passworts.</param>
        public TooShortResult(int min, int actual)
        {
            this.Description = $"Too short by {actual - min}, min is {min}";
        }

        /// <inheritdoc/>
        public string Description
        {
            get;
        }
    }
}
