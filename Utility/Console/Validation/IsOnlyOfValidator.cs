namespace KeyLocker.Utility.Console.Validation
{
    using System;
    using System.Linq;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob die Eingabe nur aus einer Liste von erlaubten Zeichen besteht.
    /// </summary>
    public class IsOnlyOfValidator : IInputValidator
    {
        /// <summary>
        /// Die erlaubten Zeichen. 
        /// </summary>
        private readonly string allowedCharacters;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="allowedCharacters">Die erlaubten Zeichen.</param>
        public IsOnlyOfValidator(string allowedCharacters)
        {
            this.allowedCharacters = (string)allowedCharacters.Clone();
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="allowedCharacters">Die erlaubten Zeichen.</param>
        public IsOnlyOfValidator(char[] allowedCharacters)
        {
            this.allowedCharacters = new string(allowedCharacters);
        }

        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (userInput.Any(i => !this.allowedCharacters.Contains(i)))
            {
                Console.WriteLine($"Input must only contain the following characters: {this.allowedCharacters}!");

                return false;
            }

            return true;
        }
    }
}