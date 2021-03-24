namespace KeyLocker.Utility.Console.Validation
{
    using System;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob die Eingabe nicht leer ist.
    /// </summary>
    public class NotEmptyValidator : IInputValidator
    {
        /// <summary>
        /// Das zu validierende Ziel. 
        /// Wird am Anfangg einer potentiellen Fehlermeldung ausgegeben.
        /// </summary>
        private readonly string target;

        /// <summary>
        /// Initialisiert eine neue Instanz.
        /// </summary>
        /// <param name="target">Das zu validierende Ziel. Wird am Anfangg einer potentiellen Fehlermeldung ausgegeben.</param>
        public NotEmptyValidator(string target)
        {
            this.target = target;
        }

        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine($"{this.target} must not be empty!");
                
                return false;
            }

            return true;
        }
    }
}