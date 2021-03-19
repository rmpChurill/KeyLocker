namespace KeyLocker.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using KeyLocker.Console.Commands;
    using KeyLocker.CoreLib;
    using KeyLocker.Utility;

    /// <summary>
    /// Stellt den Kern der Anwendung dar.
    /// </summary>
    public class ConsoleCore
    {
        private readonly ConsoleWriteOptions noFileTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.Gray, BackgroundColor = ConsoleColor.Black };
        private readonly ConsoleWriteOptions unnamedFileTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.Yellow, BackgroundColor = ConsoleColor.Black };
        private readonly ConsoleWriteOptions fileNameTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.Cyan, BackgroundColor = ConsoleColor.Black };
        private readonly ConsoleWriteOptions entriesAddedTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.Green, BackgroundColor = ConsoleColor.Black };
        private readonly ConsoleWriteOptions entriesModifedTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.Yellow, BackgroundColor = ConsoleColor.Black };
        private readonly ConsoleWriteOptions entriesDeletedTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.Red, BackgroundColor = ConsoleColor.Black };
        private readonly ConsoleWriteOptions settingsChangedTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.DarkYellow, BackgroundColor = ConsoleColor.Black };
        private readonly ConsoleWriteOptions passwordChangedTextOptions = new ConsoleWriteOptions() { TextColor = ConsoleColor.DarkRed, BackgroundColor = ConsoleColor.Black };

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

            var promptOptions = new ConsolePromptOptions() { TextColor = ConsoleColor.White, BackgroundColor = ConsoleColor.Black };

            this.Loop = true;

            while (this.Loop)
            {
                this.WritePrompt();

                var input = ConsoleHelper.Prompt(options: promptOptions).Trim();
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

            if (entries == default || !entries.Any())
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
        public void WritePrompt()
        {
            if (this.KeyLockerCore != null)
            {
                var fileName = Path.GetFileName(this.FileName);
                var changes = this.KeyLockerCore.PendingChanges;

                if (fileName != null)
                {
                    ConsoleHelper.Write(fileName, this.fileNameTextOptions);
                }
                else
                {
                    ConsoleHelper.Write("[unnamed file]", this.unnamedFileTextOptions);
                }

                if (changes.SettingsChanged)
                {
                    ConsoleHelper.Write("s!", this.settingsChangedTextOptions);
                }

                if (changes.PasswordChanged)
                {
                    ConsoleHelper.Write("pw!", this.passwordChangedTextOptions);
                }

                ConsoleHelper.Write("[");
                ConsoleHelper.Write($"+{changes.AddedEntries}", changes.AddedEntries > 0 ? this.entriesAddedTextOptions : default);
                ConsoleHelper.Write($" ~{changes.ModifiedEntries}", changes.ModifiedEntries > 0 ? this.entriesModifedTextOptions : default);
                ConsoleHelper.Write($" -{changes.DeletedEntries}", changes.DeletedEntries > 0 ? this.entriesDeletedTextOptions : default);
                ConsoleHelper.Write("]");
            }
            else
            {
                ConsoleHelper.Write("[no file]", this.noFileTextOptions);
            }

            ConsoleHelper.Write(" ");
        }
    }
}
