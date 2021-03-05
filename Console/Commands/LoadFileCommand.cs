namespace KeyLocker.Console.Commands
{
    using System;
    using System.IO;
    using System.Text.Json;

    using KeyLocker.CoreLib;

    /// <summary>
    /// Eine Implementierung von <see cref="ICommand"/>, die das Laden 
    /// </summary>
    public class LoadFileCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Opens a password file";
            }
        }

        /// <inheritdoc/>
        public string Command
        {
            get
            {
                return "load";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return 'l';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            arg = arg.Trim().Trim('"');

            try
            {
                using var inStream = File.OpenRead(arg) ?? throw new FileNotFoundException();
                using var json = JsonDocument.Parse(inStream);

                var keylockerCore = KeyLockerCore.Load(json.RootElement);

                int tries = 0;
                const int maxTries = 3;

                while (tries < maxTries)
                {
                    var password = ConsoleHelper.Prompt("Enter password: ", new ConsolePromptOptions() { Hidden = true });

                    if (keylockerCore.ConfirmPassword(password))
                    {
                        break;
                    }

                    Console.WriteLine("Wrong password! Try again!");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {arg} could not be opened!");
                return;
            }
        }
    }
}
