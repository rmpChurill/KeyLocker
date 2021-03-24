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
        public static readonly HelpCommand HelpCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Laden einer Passwortdatei.
        /// </summary>
        public static readonly LoadFileCommand LoadFileCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Speichern einer Passwortdatei.
        /// </summary>
        public static readonly SaveFileCommand SaveFileCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Erstellen einer neuen Passwortdatei.
        /// </summary>
        public static readonly NewFileCommand NewFileCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Auflisten aller Einträge.
        /// </summary>
        public static readonly ListEntriesCommand ListEntriesCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Validieren aller Einträge.
        /// </summary>
        public static readonly ValidateEntriesCommand ValidateEntriesCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zur Erzeugung eines neuen Eintrags.
        /// </summary>
        public static readonly AddEntryCommand AddEntryCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Anzeigen eines Eintrags.
        /// </summary>
        public static readonly ViewEntryCommand ViewEntryCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Anzeigen eines Passworts.
        /// </summary>
        public static readonly ViewPasswordCommand ViewPasswordCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zur Bearbeitung eines neuen Eintrags.
        /// </summary>
        public static readonly EditEntryCommand EditEntryCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zur Entfernung eines neuen Eintrags.
        /// </summary>
        public static readonly DeleteEntryCommand DeleteEntryCommand = new();

        /// <summary>
        /// Die Instanz des Befehls zum Beenden der Anwendung.
        /// </summary>
        public static readonly ExitCommand ExitCommand = new();

        /// <summary>
        /// Holt eine Auflistung aller bekannten Befehle.
        /// </summary>
        public static IEnumerable<ICommand> All
        {
            get
            {
                yield return HelpCommand;
                yield return NewFileCommand;
                yield return LoadFileCommand;
                yield return SaveFileCommand;
                yield return AddEntryCommand;
                yield return ListEntriesCommand;
                yield return ValidateEntriesCommand;
                yield return ViewEntryCommand;
                yield return ViewPasswordCommand;
                yield return DeleteEntryCommand;
                yield return ExitCommand;
            }
        }
    }
}
