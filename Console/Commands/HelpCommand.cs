namespace KeyLocker.Console.Commands
{
    using System;
    using System.Linq;

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
