namespace KeyLocker.Console.Validation
{
    using System;
    using System.Linq;
    using KeyLocker.CoreLib;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob keine andere Instanz 
    /// von <see cref="Entry"/> denselben Namen nutzt.
    /// </summary>
    public class UniqueNameValidator : IInputValidator
    {
        /// <summary>
        /// Die ausführende <see cref="KeyLockerCore"/>-Instanz.
        /// </summary>
        private KeyLockerCore core;

        /// <summary>
        /// Initialisiert eine neue Instanz.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="KeyLockerCore"/>-Instanz.</param>
        public UniqueNameValidator(KeyLockerCore core)
        {
            this.core = core;
        }

        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (this.core.Entries.Any(i => i.Name == userInput))
            {
                Console.WriteLine($"The name {userInput} is already in use!");

                return false;
            }

            return true;
        }
    }
}