namespace KeyLocker.Console.Commands
{
    using System;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die den Befehl zum Bearbeiten eines Eintrags darstellt.
    /// </summary>
    public class EditEntryCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Edits an entry with the given name.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "edit";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return '!';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            var keyLockerCore = core.KeyLockerCore;

            if (keyLockerCore == null)
            {
                Console.WriteLine("Can't view entry, no file opened!");

                return;
            }

            var entry = core.FindEntryByName(arg, true);

            if (entry != default)
            {
                //// TODO
            }
        }
    }
}
