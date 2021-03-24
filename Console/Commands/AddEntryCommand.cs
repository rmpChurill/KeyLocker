namespace KeyLocker.Console.Commands
{
    using System;
    using System.Linq;

    using KeyLocker.Console.Validation;
    using KeyLocker.CoreLib.Validation;
    using KeyLocker.Utility;

    /// <summary>
    /// Stellt den Befehl zur Erzeugung eines neuen Eintrags dar.
    /// </summary>
    public class AddEntryCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Adds a new entry.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "add";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return '+';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            var keyLockerCore = core.KeyLockerCore;

            if (keyLockerCore == null)
            {
                Console.WriteLine("Can't add an entry, no opened password file!");

                return;
            }

            var nameNotEmtpyValidator = new NotEmptyValidator("Name");
            var nameUniqeValidator = new UniqueNameValidator(keyLockerCore);
            var nameValidator = new LogicalAndValidator(new IInputValidator[] { nameNotEmtpyValidator, nameUniqeValidator });
            var loginNotEmtpyValidator = new NotEmptyValidator("Login");

            Console.WriteLine("Creating a new entry");
            ConsoleHelper.WriteSeperator();

            var entry = new Entry()
            {
                Name = ConsoleHelper.Prompt("Enter entry name: ", new ConsolePromptOptions() { Validator = nameValidator }),
                Login = ConsoleHelper.Prompt("Enter login: ", new ConsolePromptOptions() { Validator = loginNotEmtpyValidator }),
                Comment = ConsoleHelper.Prompt("Enter comment (optional): "),
            };

            if (ConsoleHelper.PromptBool("Do you want to set special settings for this entry? (y/n): "))
            {
                Console.WriteLine("\nEnter required values or skip to use default settings: ");

                var usageValidator = new EnumValidator<Usage>();
                var settings = new PartialPasswordSettings();
                var usageOptions = new ConsolePromptOptions() { AllowSkip = true, Validator = usageValidator };

                Console.Write("  Enter usages for different categories. Valid values are ");
                ConsoleHelper.WriteAll(Enum.GetNames(typeof(Usage)));
                Console.WriteLine();

                var upperCaseChars = ConsoleHelper.Prompt("    Upper case characters: ", usageOptions);
                var lowerCaseChars = ConsoleHelper.Prompt("    Lower case characters: ", usageOptions);
                var digits = ConsoleHelper.Prompt("    Digits: ", usageOptions);
                var specialCharacters = ConsoleHelper.Prompt("    Special characters: ", usageOptions);

                Console.WriteLine("  Enter the following additional values: ");

                var minLength = ConsoleHelper.Prompt("    Min length: ", new ConsolePromptOptions() { AllowSkip = true, Validator = new IntValidator(0, int.MaxValue) });
                var maxLength = ConsoleHelper.Prompt("    Max length: ", new ConsolePromptOptions() { AllowSkip = true, Validator = new IntValidator(minLength != null ? int.Parse(minLength) : 1, int.MaxValue) });
                var forbiddenCharacters = ConsoleHelper.Prompt("    List of forbidden characters: ", new ConsolePromptOptions() { AllowSkip = true });
                var allowedSpecialCharacters = ConsoleHelper.Prompt("    Allowed special characters: ", new ConsolePromptOptions() { AllowSkip = true, Validator = new IsOnlySpecialCharactersValidator() });
                var decayTime = ConsoleHelper.Prompt("    Time until invalidation: ", new ConsolePromptOptions() { AllowSkip = true, Validator = new CustomTimeSpanValidator() });

                if (!string.IsNullOrEmpty(upperCaseChars))
                {
                    settings.UpperCaseChars = Enum.Parse<Usage>(upperCaseChars);
                }

                if (!string.IsNullOrEmpty(lowerCaseChars))
                {
                    settings.LowerCaseChars = Enum.Parse<Usage>(lowerCaseChars);
                }

                if (!string.IsNullOrEmpty(specialCharacters))
                {
                    settings.SpecialCharacters = Enum.Parse<Usage>(specialCharacters);
                }

                if (!string.IsNullOrEmpty(digits))
                {
                    settings.Digits = Enum.Parse<Usage>(digits);
                }

                if (!string.IsNullOrEmpty(minLength))
                {
                    settings.MinLength = uint.Parse(minLength);
                }

                if (!string.IsNullOrEmpty(maxLength))
                {
                    settings.MaxLength = uint.Parse(maxLength);
                }

                if (!string.IsNullOrEmpty(forbiddenCharacters))
                {
                    settings.ForbiddenCharacters = forbiddenCharacters.ToCharArray();
                }

                if (!string.IsNullOrEmpty(allowedSpecialCharacters))
                {
                    settings.AllowedSpecialCharacters = allowedSpecialCharacters.ToCharArray();
                }

                if (!string.IsNullOrEmpty(decayTime))
                {
                    settings.DecayTime = CustomTimeSpan.Parse(decayTime);
                }
            }

            if (ConsoleHelper.PromptBool("Do you want to auto-generate a password for this entry? (y/n): "))
            {
                entry.EncryptedPassword = Crypto.GeneratePassword(keyLockerCore.PasswordSettings.Fill(entry.CustomSettings));

                if (ConsoleHelper.PromptBool("Do you want to show to password right now? (y/n): "))
                {
                    Console.WriteLine(entry.EncryptedPassword);
                }
            }
            else
            {
                while (true)
                {
                    var password = ConsoleHelper.Prompt("Enter password: ", new ConsolePromptOptions() { Hidden = true });
                    var confirmation = ConsoleHelper.Prompt("Confirm password: ", new ConsolePromptOptions() { Hidden = true });

                    if (!password.Equals(confirmation))
                    {
                        Console.WriteLine("Passwords don't match, try again.");

                        continue;
                    }

                    var settings = keyLockerCore.PasswordSettings.Fill(entry.CustomSettings);
                    var validationResults = EntryValidator.ValidatePassword(password, settings);

                    if (validationResults.Any())
                    {
                        Console.WriteLine("The password validates the following rules: ");

                        foreach (var validationResult in validationResults)
                        {
                            Console.WriteLine($"  {validationResult.Description}");
                        }

                        if (ConsoleHelper.PromptBool("Do you want to enter another password? y/n: "))
                        {
                            continue;
                        }
                    }

                    entry.EncryptedPassword = password;
                    break;
                }
            }

            Console.WriteLine($"Successfully created entry {entry.Name}!");

            entry.LastUpdateDate = DateTime.Now;

            keyLockerCore.Add(entry, entry.EncryptedPassword);
        }
    }
}
