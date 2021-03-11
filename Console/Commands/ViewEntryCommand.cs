namespace KeyLocker.Console.Commands
{
    using System;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die den Befehl zur Anzeige eines Eintrags darstellt.
    /// </summary>
    public class ViewEntryCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Displays an entry with the given name.";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "view";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return '?';
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
                var s = entry.CustomSettings;
                var d = keyLockerCore.PasswordSettings;

                static string isDefault(object? x) => x == null ? "        " : "    X   ";

                Console.WriteLine($"Name:    {entry.Name}");
                Console.WriteLine($"Login:   {entry.Name}");
                Console.WriteLine($"Comment: {entry.Name}");
                Console.WriteLine($"Settings:");
                Console.WriteLine($"  Name                          | default? | value");
                Console.WriteLine($"  Allowed special caharacters   | {isDefault(s.AllowedSpecialCharacters)} | {s.AllowedSpecialCharacters ?? d.AllowedSpecialCharacters}");
                Console.WriteLine($"  Decay time                    | {isDefault(s.DecayTime)} | {s.DecayTime ?? d.DecayTime}");
                Console.WriteLine($"  Forbidden characters          | {isDefault(s.ForbiddenCharacters)} | {s.ForbiddenCharacters ?? d.ForbiddenCharacters}");
                Console.WriteLine($"  Min length                    | {isDefault(s.MaxLength)} | {s.MaxLength ?? d.MaxLength}");
                Console.WriteLine($"  Max length                    | {isDefault(s.MinLength)} | {s.MinLength ?? d.MinLength}");
                Console.WriteLine($"  Usage of digits               | {isDefault(s.Digits)} | {s.Digits ?? d.Digits}");
                Console.WriteLine($"  Usage of lowercase characters | {isDefault(s.LowerCaseChars)} | {s.LowerCaseChars ?? d.LowerCaseChars}");
                Console.WriteLine($"  Usage of uppdarcase characters| {isDefault(s.UpperCaseChars)} | {s.UpperCaseChars ?? d.UpperCaseChars}");
                Console.WriteLine($"  Usage of special characters   | {isDefault(s.SpecialCharacters)} | {s.SpecialCharacters ?? d.SpecialCharacters}");
            }
        }
    }
}
