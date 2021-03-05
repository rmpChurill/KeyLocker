namespace KeyLocker.Console.Commands
{
    using System;

    using KeyLocker.CoreLib;

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
                return "init";
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
                password = ConsoleHelper.HiddenPrompt("  Enter a password for this file: ");

                if (password == ConsoleHelper.HiddenPrompt("  Confirm password: "))
                {
                    break;
                }

                Console.WriteLine("  Passwords don't match, try again!");
            }

            core.KeyLockerCore = new KeyLockerCore(password);
        }
    }
}
