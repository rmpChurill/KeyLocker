using System.IO;
using System.Text.Json;

using KeyLocker.Console.Validation;

namespace KeyLocker.Console.Commands
{
    /// <summary>
    /// Speichert eine geöffnete Datei.
    /// </summary>
    public class SaveFileCommand : ICommand
    {
        /// <inheritdoc/>
        public string HelpDescritpion
        {
            get
            {
                return "Speichert ausstehende Änderungen in eine Datei.";
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
                return "save";
            }
        }

        /// <inheritdoc/>
        public char? Alias
        {
            get
            {
                return 's';
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            var keyLockerCore = core.KeyLockerCore;

            if (keyLockerCore == null)
            {
                System.Console.WriteLine("No opened file, nothing to save!");

                return;
            }

            if (string.IsNullOrEmpty(core.FileName))
            {
                core.FileName = ConsoleHelper.ValidatedPrompt(new NotEmptyValidator("file name"), "Enter filename to save to: ");
            }
            else
            {
                var fileName = ConsoleHelper.Prompt("Enter filename to save to (or skip to save to \"{core.FileName}\"): ");

                fileName = fileName.Trim().Trim('"');

                if (!string.IsNullOrEmpty(fileName))
                {
                    core.FileName = fileName;
                }
            }

            using var stream = File.OpenWrite(core.FileName);
            using var writer = new Utf8JsonWriter(stream);

            keyLockerCore.Save(writer);
        }
    }
}
