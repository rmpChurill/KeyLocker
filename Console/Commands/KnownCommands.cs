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
        /// Die Instanz des Befehls zum Laden einer Passwortdatei.
        /// </summary>
        public readonly static LoadFileCommand LoadFileCommand = new LoadFileCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Speichern einer Passwortdatei.
        /// </summary>
        public readonly static SaveFileCommand SaveCommand = new SaveFileCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Erstellen einer neuen Passwortdatei.
        /// </summary>
        public readonly static NewFileCommand NewFileCommand = new NewFileCommand();

        /// <summary>
        /// Holt eine Auflistung aller bekannten Befehle.
        /// </summary>
        public static IEnumerable<ICommand> All
        {
            get
            {
                yield return HelpCommand;
                yield return LoadFileCommand;
                yield return NewFileCommand;
                yield return LoadFileCommand;
                yield return SaveCommand;
                yield return NewEntryCommand;
            }
        }
    }
}
