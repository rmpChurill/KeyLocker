namespace KeyLocker.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KeyLocker.Console.Commands;
    using KeyLocker.CoreLib;
    using KeyLocker.Utility;

    /// <summary>
    /// Stellt den Kern der Anwendung dar.
    /// </summary>
    public class ConsoleCore
    {
        /// <summary>
        /// Holt den Kern der Bibliothek, über den die Transaktionen ausgeführt werden.
        /// </summary>
        public KeyLockerCore? KeyLockerCore
        {
            get;
            set;
        }

        /// <summary>
        /// Der Pfad zur aktuell geladenen Datei.
        /// </summary>
        public string? FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt einen Wert, der angibt, ob die Anwendung weiterlaufen soll oder nicht.
        /// </summary>
        public bool Loop
        {
            get;
            set;
        }

        /// <summary>
        /// Startet die Ausführung des Konsolenprogramms in einer Endlosschleife.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Welcome to KeyLocker V 0.1");

            this.Loop = true;

            while (this.Loop)
            {
                var input = ConsoleHelper.Prompt(this.GenPrompt()).Trim();
                var actionToRun = default(ICommand);
                var actionArgument = string.Empty;

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                foreach (var action in KnownCommands.All)
                {
                    if (input.StartsWith(action.Command, StringComparison.OrdinalIgnoreCase) &&
                        (input.Length == action.Command.Length || char.IsWhiteSpace(input[action.Command.Length])))
                    {
                        actionToRun = action;
                        actionArgument = input[Math.Max(input.Length - 1, action.Command.Length)..];

                        break;
                    }

                    var alias = action.Alias;

                    if (alias != default &&
                        input[0] == alias &&
                        (input.Length == 1 || char.IsWhiteSpace(input[1])))
                    {
                        actionToRun = action;
                        actionArgument = input[Math.Max(input.Length - 1, 1)..];

                        break;
                    }
                }

                if (actionToRun != default)
                {
                    actionToRun.Execute(this, actionArgument);
                }
                else
                {
                    Console.WriteLine($"\"{input}\" is no valid command! Use help to show available commands!");
                }
            }
        }

        /// <summary>
        /// Sucht einen Eintrag mit Namen <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Der Name des gesuchten Eintrags.</param>
        /// <param name="printAlternativesIfNotFound">Gibt an, ob Einträge mit einem ähnlichen Namen 
        /// angezeigt werden sollen, falls kein Eintrag mit dem Namen <paramref name="name"/> gefunden wurde.</param>
        /// <returns></returns>
        public Entry? FindEntryByName(string name, bool printAlternativesIfNotFound = false)
        {
            var res = this.KeyLockerCore?.Entries?.SingleOrDefault(i => i.Name.Equals(name));

            if (res == default && printAlternativesIfNotFound)
            {
                var closest = this.FindPossibleAlternativeByName(name);

                Console.Write($"There is no entry named {name}.");

                if (closest == default)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"Did you mean ");
                    ConsoleHelper.WriteAll(closest.Select(i => i.Name));
                    Console.WriteLine("?");
                }
            }

            return res;
        }

        /// <summary>
        /// Gibt den Eintrag zurück, dessen Name die geringste Levenshtein-Distanz zu <paramref name="name"/> hat.
        /// </summary>
        /// <param name="name">Der gesuchte Name.</param>
        /// <returns>Den Eintrag mit dem ähnlichsten Namen.</returns>
        public IEnumerable<Entry> FindPossibleAlternativeByName(string name)
        {
            var entries = this.KeyLockerCore?.Entries;

            if (entries == default || entries.Count() == 0)
            {
                return Array.Empty<Entry>();
            }

            var alternatives = entries.Select(i => KeyValuePair.Create(LevenshteinDistance.Compute(i.Name, name), i))
                                      .OrderBy(i => i.Key);

            if (alternatives == default)
            {
                return Array.Empty<Entry>();
            }

            return alternatives.Where(i => i.Key == alternatives.First().Key)
                               .Select(i => i.Value);
        }

        /// <summary>
        /// Gibt einen Prompttext zurück.
        /// </summary>
        /// <returns></returns>
        public string GenPrompt()
        {

        }
    }
}
