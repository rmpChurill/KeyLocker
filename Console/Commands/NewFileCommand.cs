namespace KeyLocker.Console.Commands
{
    using System;

    using KeyLocker.CoreLib;
    using KeyLocker.Utility.Console;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die die Erstellung einer neuen Passwortliste darstellt.
    /// </summary>
    public class NewFileCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Creates new password file.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "new";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return 'i';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            Console.WriteLine("Creating new password file.");

            string? password;

            while (true)
            {
                password = ConsoleHelper.Prompt("  Enter a password for this file: ", new ConsolePromptOptions() { Hidden = true });

                if (password == ConsoleHelper.Prompt("  Confirm password: ", new ConsolePromptOptions() { Hidden = true }))
                {
                    break;
                }

                Console.WriteLine("  Passwords don't match, try again!");
            }

            core.KeyLockerCore = new KeyLockerCore(password);
        }
    }
}
