namespace KeyLocker.Console.Commands
{
    using System.Collections.Generic;

    /// <summary>
    /// Stellt eine Sammlung aller bekannten Zustände bereit.
    /// </summary>
    public static class KnownCommands
    {
        /// <summary>
        /// Die Instanz des Hilfsbefehls.
        /// </summary>
        public readonly static HelpCommand HelpCommand = new HelpCommand();

        /// <summary>
        /// Die Instanz des Befehls zur Erzeugung eines neuen Eintrags.
        /// </summary>
        public readonly static NewEntryCommand NewEntryCommand = new NewEntryCommand();

        /// <summary>
        /// Holt eine Auflistung aller bekannten Befehle.
        /// </summary>
        public static IEnumerable<ICommand> All
        {
            get
            {
                yield return HelpCommand;
                yield return NewEntryCommand;
            }
        }
    }
}
