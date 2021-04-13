using System.Collections.Generic;

namespace KeyLocker.Console
{
    /// <summary>
    /// Hauptklasse des Konsolenprogramms.
    /// </summary>
    public class Program
    {
        private class Autocompleter : KeyLocker.Utility.Console.IAutocompleter
        {
            public IEnumerable<string> GetAutocCompleteOptions(string input)
            {
                yield return "A";
                yield return "B";
                yield return "C";
            }
        }

        /// <summary>
        /// Haupteinsprungspunkt des Konsolenprogramms.
        /// </summary>
        public static void Main()
        {
            // new ConsoleCore().Run();

            Utility.Console.ConsoleHelper.Prompt("> ", new Utility.Console.ConsolePromptOptions()
            {
                Autocompleter =
            });
        }
    }
}
