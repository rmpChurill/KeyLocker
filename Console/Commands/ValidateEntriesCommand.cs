namespace KeyLocker.Console.Commands
{
    using System;
    using System.Linq;

    using KeyLocker.CoreLib.Validation;
    using KeyLocker.Utility.Console;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die den Befehl zur Prüfung aller Einträge darstellt.
    /// </summary>
    public class ValidateEntriesCommand : ICommand
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

            for (int i = 0; i < 3; i++)
            {
                var password = ConsoleHelper.Prompt("Enter password to decode entries: ", new ConsolePromptOptions() { Hidden = true });
                var now = DateTime.Now;

                if (keyLockerCore.ConfirmPassword(password))
                {
                    foreach (var entry in keyLockerCore.Entries)
                    {
                        var settings = keyLockerCore.PasswordSettings.Fill(entry.CustomSettings);
                        var validationResults = EntryValidator.ValidatePassword(Crypto.Decrypt(entry.EncryptedPassword, password), settings);
                        var outdated = settings.DecayTime.CompareToDifference(entry.LastUpdateDate, now) > 0;
                        var hasValidationErrors = validationResults.Any();

                        if (outdated || hasValidationErrors)
                        {
                            Console.WriteLine($"{entry.Name}");

                            if (outdated)
                            {
                                Console.WriteLine($"  Outdated since {settings.DecayTime.AddTo(now)}");
                            }

                            foreach (var validationResult in validationResults)
                            {
                                Console.WriteLine($"  {validationResult.Description}");
                            }
                        }
                    }
                }

                Console.WriteLine("Wrong password, try again!");
            }
        }
    }
}
