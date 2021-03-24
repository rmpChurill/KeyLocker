namespace KeyLocker.Utility.Console.Validation
{
    using System.Collections.Generic;

    using KeyLocker.Utility.Console;

    /// <summary>
    /// Eine Implementierung von <see cref="IInputValidator"/>, die mehrere andere <see cref="IInputValidator"/>
    /// durch logisches UND verknüpft.
    /// </summary>
    public class LogicalAndValidator : IInputValidator
    {
        /// <summary>
        /// Die zu prüfenden <see cref="IInputValidator"/>-Instanzen.
        /// </summary>
        private readonly IEnumerable<IInputValidator> validators;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="validators">Die zu prüfenden <see cref="IInputValidator"/>-Instanzen.</param>
        public LogicalAndValidator(IEnumerable<IInputValidator> validators)
        {
            this.validators = new List<IInputValidator>(validators);
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="validators">Die zu prüfenden <see cref="IInputValidator"/>-Instanzen.</param>
        public LogicalAndValidator(params IInputValidator[] validators)
        {
            this.validators = new List<IInputValidator>(validators);
        }

        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            var res = true;

            foreach (var validator in this.validators)
            {
                res = res && validator.IsValid(userInput);
            }

            return res;
        }
    }
}