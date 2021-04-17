namespace KeyLocker.Console.Commands
{
    using System;
    using System.Linq;
    using KeyLocker.Utility;
    using KeyLocker.Utility.Console;

    /// <summary>
    /// Stellt den Hilfebefehl dar.
    /// </summary>
    public class HelpCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Shows the help.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "help";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return 'h';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                this.ListAllCommands();
            }
            else
            {
                this.AutocompleteCommand(arg);
            }
        }

        private void AutocompleteCommand(string prefix)
        {
            var matching = KnownCommands.All
                .Where(i => string.Compare(prefix, i.Command, StringComparison.OrdinalIgnoreCase) == 0)
                .SingleOrDefault();

            if (matching != default)
            {
                Console.WriteLine($"Command:     {matching.Command}");
                Console.WriteLine($"Shortcut:    {matching.Alias?.ToString() ?? " - none -"}");
                Console.WriteLine($"Description: {matching.HelpDescritpion}");
            }
            else
            {
                var alternatives = KnownCommands.All
                    .Select(i => new Tuple<ICommand, int>(i, LevenshteinDistance.Compute(i.Command, prefix)))
                    .OrderBy(i => i.Item2)
                    .Where(i => i.Item2 < 3)
                    .Select(i => i.Item1.Command);

                if (alternatives.Any())
                {
                    Console.WriteLine("Similiar commands: ");
                    ConsoleHelper.WriteAll(alternatives);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("No matching commands");
                }
            }
        }

        /// <summary>
        /// Gibt eine Auflistung aller Befehle mit Hilfen aus.
        /// </summary>
        private void ListAllCommands()
        {
            var longestCommand = 0;

            foreach (var command in KnownCommands.All)
            {
                longestCommand = Math.Max(longestCommand, command.Command.Length);
            }

            foreach (var command in KnownCommands.All)
            {
                var alias = command.Alias;

                if (alias != null)
                {
                    Console.Write(alias);
                    Console.Write(", ");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.Write(command.Command);
                Console.Write(string.Concat(Enumerable.Repeat(' ', longestCommand - command.Command.Length)));
                Console.Write(": ");
                Console.WriteLine(command.HelpDescritpion);
            }
        }
    }
}
