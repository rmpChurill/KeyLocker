namespace KeyLocker.Utility.Console.Validation
{
    using System;
    using System.Linq;

    using KeyLocker.Utility.Console;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob die Eingabe einem Wert einer Enum entspricht.
    /// </summary>
    public class EnumValidator<T> : IInputValidator
        where T : Enum
    {
        /// <summary>
        /// Die Namen der Enum-Werte.
        /// </summary>
        private readonly string[] names;

        /// <summary>
        /// Inituialisiert eine neue Instanz.
        /// </summary>
        public EnumValidator()
        {
            this.names = Enum.GetNames(typeof(T));
        }

        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (!this.names.Contains(userInput))
            {
                Console.Write($"Input must be one of ");
                ConsoleHelper.WriteAll(this.names);
                Console.WriteLine();

                return false;
            }

            return true;
        }
    }
}