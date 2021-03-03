namespace KeyLocker.Console.Commands
{
    using System;
    using System.Linq;
    using KeyLocker.Console.Validation;

    /// <summary>
    /// Stellt den Befehl zur Erzeugung eines neuen Eintrags dar.
    /// </summary>
    public class NewEntryCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Erzeugt einen neuen Eintrag.";
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
                return "new";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return 'n';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            var entry = new Entry();
            var nameNotEmtpyValidator = new NotEmptyValidator("Name");
            var nameUniqeValidator = new UniqueNameValidator(core.KeyLockerCore);
            var nameValidator = new MultiValidator(new IInputValidator[] { nameNotEmtpyValidator, nameUniqeValidator });
            var loginNotEmtpyValidator = new NotEmptyValidator("Login");

            Console.WriteLine("Enter the required data");

            entry.Name = ConsoleHelper.ValidatedPrompt(nameValidator, "Enter entry name: ");
            entry.Login = ConsoleHelper.ValidatedPrompt(loginNotEmtpyValidator, "enter login: ");
            entry.Comment = ConsoleHelper.Prompt("Enter comment (optional):");

            if (ConsoleHelper.PromptBool("Do you want to set special settings for this entry? (y/n): "))
            {

            }

            if (ConsoleHelper.PromptBool("Do you want to auto-generate a password for this entry? (y/n): "))
            {
                entry.EncryptedPassword = "aB4%";
            }
            else
            {
                entry.EncryptedPassword = ConsoleHelper.HiddenPrompt("Enter Password: ");
            }

            Console.WriteLine($"Successfully created entry {entry.Name}!");

            entry.LastUpdateDate = DateTime.Now;
            core.KeyLockerCore.Register(entry, entry.EncryptedPassword);
        }
    }
}
