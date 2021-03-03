namespace KeyLocker.Console.Validation
{
    using System;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob die Eingabe eine Zahl ist.
    /// </summary>
    public class IntValidator : IInputValidator
    {
        /// <summary>
        /// Das erlaubte inklusive Minimum.
        /// </summary>
        private int min;

        /// <summary>
        /// Das erlaubte inklusive Maximum.
        /// </summary>
        private int max;

        /// <summary>
        /// Initialisiert eine neue Instanz, die sämtliche Eingaben erlaubt, die einen Integer darstellen.
        /// </summary>
        public IntValidator() : this(int.MinValue, int.MaxValue)
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz, die sämtliche Eingaben erlaubt, die einen 
        /// Integer zwischen 0 und <paramref name="max"/> (inklusive) darstellen.
        /// </summary>
        /// <param name="max">Das inklusive Maximum.</param>
        public IntValidator(int max) : this(0, max)
        {

        }
        /// <summary>
        /// Initialisiert eine neue Instanz, die sämtliche Eingaben erlaubt, die einen 
        /// Integer zwischen <paramref name="min"/> und <paramref name="max"/> (beide inklusive) darstellen.
        /// </summary>
        /// <param name="min">Das inklusive Minimum</param>
        /// <param name="max">Das inklusive Maximum.</param>
        public IntValidator(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (!int.TryParse(userInput, out var number))
            {
                Console.WriteLine("Please enter a number!");

                return false;
            }

            if (number < this.min || number > this.max)
            {
                Console.WriteLine($"The value must be between {this.min} and {this.max}!");

                return false;
            }

            return true;
        }
    }
}