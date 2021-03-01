namespace KeyLocker.Console.Commands
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Stellt den Hilfebefehl dar.
    /// </summary>
    public class HelpCommand : ICommand
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        public HelpCommand()
        {
        }

        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Shows the help.";
            }
        }

        /// <inheritdoc/>
        public bool IsCaseSensitive
        {
            get
            {
                return false;
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
        public IEnumerable<string>? Aliases
        {
            get
            {
                yield return "h";
                yield return "?";
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            foreach (var command in core.AllCommands)
            {
                Console.WriteLine(command.Command);

                Console.Write("\taliases: ");
                ConsoleHelper.WriteLine(command.Aliases ?? Array.Empty<string>());

                Console.Write("\tdescription: ");
                Console.WriteLine(command.HelpDescritpion);
                Console.WriteLine();
            }
        }
    }
}
