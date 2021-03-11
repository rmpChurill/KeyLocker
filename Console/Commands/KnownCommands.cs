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
        public static readonly HelpCommand HelpCommand = new HelpCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Laden einer Passwortdatei.
        /// </summary>
        public static readonly LoadFileCommand LoadFileCommand = new LoadFileCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Speichern einer Passwortdatei.
        /// </summary>
        public static readonly SaveFileCommand SaveFileCommand = new SaveFileCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Erstellen einer neuen Passwortdatei.
        /// </summary>
        public static readonly NewFileCommand NewFileCommand = new NewFileCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Auflisten aller Einträge.
        /// </summary>
        public static readonly ListEntriesCommand ListEntriesCommand = new ListEntriesCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Validieren aller Einträge.
        /// </summary>
        public static readonly ValidateEntriesCommand ValidateEntriesCommand = new ValidateEntriesCommand();

        /// <summary>
        /// Die Instanz des Befehls zur Erzeugung eines neuen Eintrags.
        /// </summary>
        public static readonly AddEntryCommand AddEntryCommand = new AddEntryCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Anzeigen eines Eintrags.
        /// </summary>
        public static readonly ViewEntryCommand ViewEntryCommand = new ViewEntryCommand();

        /// <summary>
        /// Die Instanz des Befehls zum Anzeigen eines Passworts.
        /// </summary>
        public static readonly ViewPasswordCommand ViewPasswordCommand = new ViewPasswordCommand();

        /// <summary>
        /// Die Instanz des Befehls zur Bearbeitung eines neuen Eintrags.
        /// </summary>
        public static readonly EditEntryCommand EditEntryCommand = new EditEntryCommand();

        /// <summary>
        /// Die Instanz des Befehls zur Entfernung eines neuen Eintrags.
        /// </summary>
        public static readonly DeleteEntryCommand DeleteEntryCommand = new DeleteEntryCommand();

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
            }
        }
    }
}
