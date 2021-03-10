namespace KeyLocker.CoreLib
{
    /// <summary>
    /// Implementierende Klassen stellen Eigenschaften bereit, die anzeigen, wie 
    /// viele Änderungen an einer geladenen Passwortliste vorgenommen wurden.
    /// </summary>
    public interface IPendingChanges
    {
        /// <summary>
        /// Holt die Anzahl der der bearbeiteten Einträge.
        /// </summary>
        int ModifiedEntries
        {
            get;
        }

        /// <summary>
        /// Holt die Anzahl der hinzugefügten Einträge.
        /// </summary>
        int AddedEntries
        {
            get;
        }

        /// <summary>
        /// Holt die Anzahl der gelöschten Einträge.
        /// </summary>
        int DeletedEntries
        {
            get;
        }

        /// <summary>
        /// Holt einen Wert der anzeigt ob die Einstellungen geändert wurden oder nicht.
        /// </summary>
        bool SettingsChanged
        {
            get;
        }

        /// <summary>
        /// Holt einen Wert der angibt ob das Passwort der Datei geändert wurde oder nicht.
        /// </summary>
        bool PasswordChanged
        {
            get;
        }
    }
}
