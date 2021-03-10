namespace KeyLocker.Console.Commands
{
    using System;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die den Befehl zum Anzeigen eines Passworts anzeigt.
    /// </summary>
    public class ViewPasswordCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Zeigt ein Passwort an.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "pass";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return 'p';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            var keyLockerCore = core.KeyLockerCore;

            if (keyLockerCore == null)
            {
                Console.WriteLine("No file opened!");

                return;
            }

            var entry = core.FindEntryByName(arg, true);

            if (entry != default)
            {
                for (int i = 0; i < 3; i++)
                {
                    var password = ConsoleHelper.Prompt("Enter password: ", new ConsolePromptOptions() { Hidden = true });

                    if (keyLockerCore.ConfirmPassword(password))
                    {
                        var decrypted = Crypto.Decrypt(entry.EncryptedPassword, password);

                        Console.WriteLine(decrypted);

                        break;
                    }

                    Console.WriteLine("Wrong password, try again.");
                }
            }
        }
    }
}
