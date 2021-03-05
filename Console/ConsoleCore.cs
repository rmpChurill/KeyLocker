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
                var input = ConsoleHelper.Prompt();

                var i = 0;

                while (i < input.Length && !char.IsWhiteSpace(input[i])) { i++; }

                var command = input[0..i];
                var actionToRun = default(ICommand);

                foreach (var action in KnownCommands.All)
                {
                    var comparison = StringComparison.OrdinalIgnoreCase;

                    if (action.Command.Equals(command, comparison))
                    {
                        actionToRun = action;
                        break;
                    }

                    var alias = action.Alias?.ToString();

                    if (alias != default && alias.Equals(command, comparison))
                    {
                        actionToRun = action;
                    }
                }

                if (actionToRun != default)
                {
                    var arg = input[i..].Trim();

                    actionToRun.Execute(this, arg);
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
        public Entry? FindEntryByName(string name)
        {
            return this.KeyLockerCore?.Entries?.SingleOrDefault(i => i.Name.Equals(name));
        }

        /// <summary>
        /// Gibt den Eintrag zurück, dessen Name die geringste Levenshtein-Distanz zu <paramref name="name"/> hat.
        /// </summary>
        /// <param name="name">Der gesuchte Name.</param>
        /// <returns>Den Eintrag mit dem ähnlichsten Namen.</returns>
        public Entry? FindPossibleAlternativeByName(string name)
        {
            return this.KeyLockerCore?.Entries?.Select(i => KeyValuePair.Create(LevenshteinDistance.Compute(i.Name, name), i))
                    .OrderBy(i => i.Key)
                    .Select(i => i.Value)
                    .FirstOrDefault();
        }
    }
}
