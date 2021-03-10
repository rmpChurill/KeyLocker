namespace KeyLocker.Console.Commands
{
    using System;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die den Befehl zum Entfernen eines Eintrags darstellt.
    /// </summary>
    public class DeleteEntryCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Removes an entry.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "delete entry";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return '-';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            var keyLockerCore = core.KeyLockerCore;

            if (keyLockerCore == null)
            {
                Console.WriteLine("Can't remove entry, no password file opened!");

                return;
            }

            var entry = core.FindEntryByName(arg, true);

            if (entry != default)
            {
                keyLockerCore.Remove(entry);
            }
        }
    }
}
