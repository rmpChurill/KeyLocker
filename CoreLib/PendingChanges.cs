namespace KeyLocker.CoreLib
{
    using System.Collections.Generic;

    /// <summary>
    /// Die Implementierung von <see cref="IPendingChanges"/>.
    /// </summary>
    internal class PendingChanges : IPendingChanges
    {
        /// <summary>
        /// Alle geänderten Einträge.
        /// </summary>
        private readonly HashSet<Entry> modifiedEntries;

        /// <summary>
        /// Alle hinzugefügtenEinträge.
        /// </summary>
        private readonly HashSet<Entry> addedEntries;

        /// <summary>
        /// Alle gelöschten Einträge.
        /// </summary>
        private readonly HashSet<Entry> deletedEntries;

        /// <summary>
        /// Initialisiert eine neue Instanze der Klasse.
        /// </summary>
        internal PendingChanges()
        {
            this.addedEntries = new HashSet<Entry>();
            this.modifiedEntries = new HashSet<Entry>();
            this.deletedEntries = new HashSet<Entry>();
        }

        /// <inheritdoc/>
        public int AddedEntries
        {
            get
            {
                return this.addedEntries.Count;
            }
        }

        /// <inheritdoc/>
        public int ModifiedEntries
        {
            get
            {
                return this.modifiedEntries.Count;
            }
        }

        /// <inheritdoc/>
        public int DeletedEntries
        {
            get
            {
                return this.deletedEntries.Count;
            }
        }

        /// <inheritdoc/>
        public bool SettingsChanged
        {
            get;
            private set;
        }

        /// <inheritdoc/>
        public bool PasswordChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// Registriert die Bearbeitung eines Eintrags.
        /// </summary>
        /// <param name="entry">Der bearbeitete Eintrag.</param>
        internal void NotifyAdd(Entry entry)
        {
            this.deletedEntries.Remove(entry);
            this.modifiedEntries.Remove(entry);

            this.addedEntries.Add(entry);
        }

        /// <summary>
        /// Registriert die Bearbeitung eines Eintrags.
        /// </summary>
        /// <param name="entry">Der bearbeitete Eintrag.</param>
        internal void NotifyMofication(Entry entry)
        {
            if (!this.addedEntries.Contains(entry) &&
                !this.deletedEntries.Contains(entry))
            {
                this.modifiedEntries.Add(entry);
            }
        }

        /// <summary>
        /// Registriert das Entfernen eines Eintrags.
        /// </summary>
        /// <param name="entry">Der bearbeitete Eintrag.</param>
        internal void NotifyDelete(Entry entry)
        {
            this.modifiedEntries.Remove(entry);

            if (!this.addedEntries.Remove(entry))
            {
                this.deletedEntries.Add(entry);
            }
        }

        /// <summary>
        /// Registriert die Änderung der Einstellungen.
        /// </summary>
        internal void NotifySettingsChanged()
        {
            this.SettingsChanged = true;
        }

        /// <summary>
        /// Registriert die Änderung des Dateipassworts.
        /// </summary>
        internal void NotifyPasswordChanged()
        {
            this.PasswordChanged = true;
        }

        /// <summary>
        /// Registriert das Speichern derausstehenden Änderungen, wodurch alle Werte der Instanz zurückgesetzt werden.
        /// </summary>
        internal void NotifySave()
        {
            this.PasswordChanged = false;
            this.SettingsChanged = false;
            this.addedEntries.Clear();
            this.deletedEntries.Clear();
            this.modifiedEntries.Clear();
        }
    }
}
