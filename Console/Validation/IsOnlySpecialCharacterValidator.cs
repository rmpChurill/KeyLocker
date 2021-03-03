namespace KeyLocker.Console.Validation
{
    using System;
    using System.Linq;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob die Eingabe nur aus Sonderzeichen besteht.
    /// </summary>
    public class IsOnlySpecialCharactersValidator : IInputValidator
    {
        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (userInput.Any(i => !Definitions.SpecialCharacters.Contains(i)))
            {
                Console.WriteLine($"Input must only contain the following characters: {string.Concat(Definitions.SpecialCharacters)}!");

                return false;
            }

            return true;
        }
    }
}