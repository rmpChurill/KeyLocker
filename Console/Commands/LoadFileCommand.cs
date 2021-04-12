namespace KeyLocker.Console.Commands
{
    using System;
    using System.IO;
    using System.Text.Json;

    using KeyLocker.Console.Validation;
    using KeyLocker.CoreLib;
    using KeyLocker.Utility.Console;
    using KeyLocker.Utility.Console.Validation;

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

            if (string.IsNullOrEmpty(arg))
            {
                arg = ConsoleHelper.Prompt("Enter the file name to be opened: ", new ConsolePromptOptions() { Validator = new NotEmptyValidator("File name") });
            }

            try
            {
                using var inStream = File.OpenRead(arg) ?? throw new FileNotFoundException();
                using var json = JsonDocument.Parse(inStream);

                var keylockerCore = KeyLockerCore.Load(json.RootElement);

                for (int i = 0; i < 3; i++)
                {
                    var password = ConsoleHelper.Prompt("Enter password: ", new ConsolePromptOptions() { Hidden = true });

                    if (keylockerCore.ConfirmPassword(password))
                    {
                        Console.WriteLine("File opened!");

                        break;
                    }

                    Console.WriteLine("Wrong password! Try again!");
                }
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is FileNotFoundException)
                {
                    Console.WriteLine($"File \"{arg}\" could not be opened!");
                }
                else
                {
                    throw new Exception("Unknown exception!", e);
                }
            }
        }
    }
}
