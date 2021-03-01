namespace KeyLocker.CoreLib.Validation
{
    /// <summary>
    /// Implementierung von <see cref="IValidationResult"/>, die anzeigt, dass ein ungültiges Zeichen in einem Passwort enthalten war.
    /// </summary>
    public class InvalidCharacterResult : IValidationResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="c">The invalid character.</param>
        public InvalidCharacterResult(char c)
        {
            this.Description = $"Contains invalid disallowed character '{c}'";
        }

        /// <inheritdoc />
        public string Description
        {
            get;
        }
    }
}
