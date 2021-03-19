namespace KeyLocker.Console.Commands
{
    using System;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die den Befehl zum Beenden der Anwendung darstellt.
    /// </summary>
    public class ExitCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            if (string.IsNullOrEmpty(core.FileName) ||
                core.KeyLockerCore != null && !core.KeyLockerCore.PendingChanges.Any ||
                ConsoleHelper.PromptBool("There are unsaved changes. Do you really want to exit? (y/n): "))
            {
                core.Loop = false;
            }
        }
    }
}
