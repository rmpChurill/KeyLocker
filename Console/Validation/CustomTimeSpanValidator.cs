namespace KeyLocker.Console.Validation
{
    using System;

    using KeyLocker.Utility;

    /// <summary>
    /// Implementierung von <see cref="IInputValidator"/>, die prüft, ob die Eingabe eine Zeitspanne darstellt.
    /// </summary>
    public class CustomTimeSpanValidator : IInputValidator
    {
        /// <inheritdoc/>
        public bool IsValid(string userInput)
        {
            if (!CustomTimeSpan.TryParse(userInput, out var _))
            {
                Console.WriteLine($"Input must be a time span in format [duration][unit] where duration must be number and unit is one of s (seconds), m (minutes), h (hours), d (days), W (weeks), M (months), Y (years).");

                return false;
            }

            return true;
        }
    }
}