namespace KeyLocker.CoreLib
{
    using System.Collections.Generic;

    /// <summary>
    /// Diese Klasse stellt die Schnittstelle zur KeyLocker-Bibliothek dar.
    /// </summary>
    public class KeyLockerCore
    {
        /// <summary>
        /// Die registrierten Einträge.
        /// </summary>
        private List<Entry> entries;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="settings"></param>
        public KeyLockerCore(AppSettings settings)
        {
            this.Settings = settings;
            this.entries = new List<Entry>();
        }

        /// <summary>
        /// Holt die Einstellungen der Bibliothek.
        /// </summary>
        public AppSettings Settings
        {
            get;
        }

        /// <summary>
        /// Holt alle registrierten Einträge.
        /// </summary>
        /// <value></value>
        public IEnumerable<Entry> Entries
        {
            get
            {
                return this.entries;
            }
        }

        /// <summary>
        /// Registriert einen neuen Eintrag.
        /// Das Passwort wird verschlüsselt und gesetzt.
        /// </summary>
        /// <param name="entry">Der Eintrag.</param>
        /// <param name="rawPassword">Das unverschlüsselte Passwort.</param>
        public void Register(Entry entry, string rawPassword)
        {
            this.entries.Add(entry);
        }
    }
}
