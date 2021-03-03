namespace KeyLocker.Console.Validation
{
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Eine Implementierung von <see cref="IInputValidator"/>, die mehrere andere <see cref="IInputValidator"/>
    /// durch logisches UND verknüpft.
    /// </summary>
    public class LogicalAndValidator : IInputValidator
    {
        /// <summary>
        /// Die zu prüfenden <see cref="IInputValidator"/>-Instanzen.
        /// </summary>
        private IEnumerable<IInputValidator> validators;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="validators">Die zu prüfenden <see cref="IInputValidator"/>-Instanzen.</param>
        public LogicalAndValidator(IEnumerable<IInputValidator> validators)
        {
            this.validators = validators;
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