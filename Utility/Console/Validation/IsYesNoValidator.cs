namespace KeyLocker.Utility.Console.Validation
{
    using System;

    using KeyLocker.Utility.Console;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob die Eingabe y, Y, n oder N ist. (Ja/Nein)
    /// </summary>
    public class IsYesNoValidator : IInputValidator
    {
        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (userInput != "y" && 
                userInput != "Y" &&
                userInput != "n" &&
                userInput != "N")
            {
                Console.WriteLine("Please enter only y/n!");
                
                return false;
            }

            return true;
        }
    }
}