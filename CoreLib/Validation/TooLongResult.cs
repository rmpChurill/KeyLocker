namespace KeyLocker.CoreLib.Validation
{
    /// <summary>
    /// Eine Implementierung von <see cref="IValidationResult"/>, die anzeigt, dass das Passwort zu lang ist.
    /// </summary>
    public class TooLongResult : IValidationResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="max">Die maximale Länge für das Passwort.</param>
        /// <param name="actual">Die tatsächliche Länge des Passworts.</param>
        public TooLongResult(int max, int actual)
        {
            this.Description = $"Too long by {max - actual}, max is {max}";
        }

        /// <inheritdoc/>
        public string Description
        {
            get;
        }
    }
}
