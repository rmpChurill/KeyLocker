namespace KeyLocker.Console.Commands
{
    using System;
    using System.Linq;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die den Befehl zur Auflistung aller Einträge darstellt.
    /// </summary>
    public class ListEntriesCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Lists all entries.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "list";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            var keyLockerCore = core.KeyLockerCore;

            if (keyLockerCore == null)
            {
                Console.WriteLine("Can't view entries, no file opened!");

                return;
            }

            var longestName = keyLockerCore.Entries.Max(i => i.Name.Length);

            foreach (var entry in keyLockerCore.Entries)
            {
                Console.WriteLine($"{entry.Name}{new string(' ', entry.Name.Length - longestName)} {entry.Comment}");
            }
        }
    }
}
