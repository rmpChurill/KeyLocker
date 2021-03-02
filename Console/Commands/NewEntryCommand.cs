namespace KeyLocker.Console.Commands
{
    using System.Collections.Generic;

    using KeyLocker.Console.States;

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
        public IEnumerable<string>? Aliases
        {
            get
            {
                yield return "n";
                yield return "+";
            }
        }

        /// <inheritdoc/>
        public void Execute(ConsoleCore core, string arg)
        {
            core.PushState(KnownStates.);
        }
    }
}
