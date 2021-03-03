namespace KeyLocker.Console.Commands
{
    using System;

    using KeyLocker.Console.Validation;
    using KeyLocker.Utility;

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
            var nameNotEmtpyValidator = new NotEmptyValidator("Name");
            var nameUniqeValidator = new UniqueNameValidator(core.KeyLockerCore);
            var nameValidator = new LogicalAndValidator(new IInputValidator[] { nameNotEmtpyValidator, nameUniqeValidator });
            var loginNotEmtpyValidator = new NotEmptyValidator("Login");

            Console.WriteLine("Enter the required data");

            var entry = new Entry()
            {
                Name = ConsoleHelper.ValidatedPrompt(nameValidator, "Enter entry name: "),
                Login = ConsoleHelper.ValidatedPrompt(loginNotEmtpyValidator, "enter login: "),
                Comment = ConsoleHelper.Prompt("Enter comment (optional):"),
            };

            if (ConsoleHelper.PromptBool("Do you want to set special settings for this entry? (y/n): "))
            {
                Console.WriteLine("Enter required values or skip to use default settings:");

                var usageValidator = new EnumValidator<Usage>();
                var settings = new PartialPasswordSettings();

                Console.Write("Enter usages for different categories. Valid values are ");
                ConsoleHelper.WriteAll(Enum.GetNames(typeof(Usage)));
                Console.WriteLine();

                var upperCaseChars = ConsoleHelper.ValidatedPromptOrEmpty(usageValidator, "Upper case characters: ");
                var lowerCaseChars = ConsoleHelper.ValidatedPromptOrEmpty(usageValidator, "Lower case characters: ");
                var digits = ConsoleHelper.ValidatedPromptOrEmpty(usageValidator, "digits: ");
                var specialCharacters = ConsoleHelper.ValidatedPromptOrEmpty(usageValidator, "Special characters: ");

                Console.WriteLine("Enter the following additional values: ");

                var minLength = ConsoleHelper.ValidatedPromptOrEmpty(new IntValidator(0, int.MaxValue), "Min length: ");
                var maxLength = ConsoleHelper.ValidatedPromptOrEmpty(new IntValidator(minLength != null ? int.Parse(minLength) : 1, int.MaxValue), "Max length: ");
                var forbiddenCharacters = ConsoleHelper.Prompt("List of forbidden characters: ");
                var allowedSpecialCharacters = ConsoleHelper.ValidatedPromptOrEmpty(new IsOnlySpecialCharactersValidator(), "Allowed special characters: ");
                var decayTime = ConsoleHelper.ValidatedPromptOrEmpty(new CustomTimeSpanValidator(), "Time until invalidation: ");

                if (upperCaseChars != null)
                {
                    settings.UpperCaseChars = Enum.Parse<Usage>(upperCaseChars);
                }

                if (lowerCaseChars != null)
                {
                    settings.LowerCaseChars = Enum.Parse<Usage>(lowerCaseChars);
                }

                if (specialCharacters != null)
                {
                    settings.SpecialCharacters = Enum.Parse<Usage>(specialCharacters);
                }

                if (digits != null)
                {
                    settings.Digits = Enum.Parse<Usage>(digits);
                }

                if (minLength != null)
                {
                    settings.MinLength = uint.Parse(minLength);
                }

                if (maxLength != null)
                {
                    settings.MaxLength = uint.Parse(maxLength);
                }

                if (forbiddenCharacters != null && forbiddenCharacters != string.Empty)
                {
                    settings.ForbiddenCharacters = forbiddenCharacters.ToCharArray();
                }

                if (allowedSpecialCharacters != null)
                {
                    settings.AllowedSpecialCharacters = allowedSpecialCharacters.ToCharArray();
                }

                if (decayTime != null)
                {
                    settings.DecayTime = CustomTimeSpan.Parse(decayTime);
                }
            }

            if (ConsoleHelper.PromptBool("Do you want to auto-generate a password for this entry? (y/n): "))
            {
                entry.EncryptedPassword = Crypto.GeneratePassword(core.KeyLockerCore.Settings.PasswordSettings.Fill(entry.CustomSettings));
            }
            else
            {
                while (true)
                {
                    var password = ConsoleHelper.HiddenPrompt("Enter password: ");
                    var confirmation = ConsoleHelper.HiddenPrompt("Confirm password: ");

                    if (password.Equals(confirmation))
                    {
                        entry.EncryptedPassword = password;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Passwords don't match, try again.");
                    }
                }
            }

            Console.WriteLine($"Successfully created entry {entry.Name}!");

            entry.LastUpdateDate = DateTime.Now;
            core.KeyLockerCore.Register(entry, entry.EncryptedPassword);
        }
    }
}
