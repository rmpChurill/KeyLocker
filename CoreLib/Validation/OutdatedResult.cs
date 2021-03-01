namespace KeyLocker.CoreLib.Validation
{
    using System;

    /// <summary>
    /// Eine Implementierung von <see cref="IValidationResult"/>, die anzeigt
    /// </summary>
    public class OutdatedResult : IValidationResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="outdatedSince">Die Zeit, seit der das Passwort veraltet ist.</param>
        public OutdatedResult(TimeSpan outdatedSince)
        {
            this.Description = $"Outdated since {outdatedSince}";
        }

        /// <inheritdoc/>
        public string Description
        {
            get;
        }
    }
}
