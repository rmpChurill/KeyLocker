﻿namespace KeyLocker.Console.Commands
{
    using System;
    using System.Linq;
    using KeyLocker.Utility;

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
            var alternatives = KnownCommands.All
                .Select(i => new Tuple<ICommand, int>(i, LevenshteinDistance.Compute(i.Command, prefix)))
                .OrderBy(i => i.Item2)
                .Where(i => i.Item2 < 4);

            if(alternatives.Any())
            {
                Console.WriteLine("");
            }
            else
            {

            }
        }

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
