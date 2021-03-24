namespace KeyLocker.Utility.Console
{
    using System.Collections.Generic;

    /// <summary>
    /// Implementierende Klassen stellen Funkktionen bereit, die zur Auovervollständigung dienen.
    /// </summary>
    public interface IAutocompleter
    {
        /// <summary>
        /// Holt alle Autovervollständigungsoptionen die für die Eingabe <paramref name="input"/> verfügbar sind.
        /// </summary>
        /// <param name="input">Die bisherige Eingabe.</param>
        /// <returns>Eine Auflistung von Autovervollständigungsoptionen.</returns>
        IEnumerable<string> GetAutocCompleteOptions(string input);
    }
}
