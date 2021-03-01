namespace KeyLocker.CoreLib.Validation
{
    using System.Text;

    /// <summary>
    /// Implementierung von <see cref="IValidationResult"/>, die anzeigt, dass kein Zeichen aus einem Set von benötigten Zeichen im Passwort enthalten war.
    /// </summary>
    public class MissingRequiredCharacterResult : IValidationResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="requiredCharacters">Die benötigten Zeichen, von denen keines enthalten war.</param>
        public MissingRequiredCharacterResult(char[] requiredCharacters)
        {
            var sb = new StringBuilder("Doesn't contain any of [");

            foreach (var c in requiredCharacters)
            {
                sb.Append(c).Append(", ");
            }

            this.Description = sb.Remove(sb.Length - 2, 2).Append("]!").ToString();
        }

        /// <inheritdoc/>
        public string Description
        {
            get;
        }
    }
}
