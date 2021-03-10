namespace KeyLocker.CoreLib
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using KeyLocker.Utility;

    /// <summary>
    /// Diese Klasse stellt die Schnittstelle zur KeyLocker-Bibliothek dar.
    /// </summary>
    public class KeyLockerCore
    {
        /// <summary>
        /// Die Übersicht über ungespeicherte Änderungen.
        /// </summary>
        private readonly PendingChanges pendingChanges;

        /// <summary>
        /// Die registrierten Einträge.
        /// </summary>
        private readonly List<Entry> entries;

        /// <summary>
        /// Die Einstellungen.
        /// </summary>
        private AppSettings settings;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        private KeyLockerCore()
        {
            this.pendingChanges = new PendingChanges();
            this.settings = new AppSettings();
            this.entries = new List<Entry>();

            this.settings.PasswordSettings.PropertyChanged += this.HandlePropertyChanged;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="password">Das Passwort für die neu erstellte Passwortdatei.</param>
        public KeyLockerCore(string password) : this()
        {
            this.settings.Salt = Crypto.ComputeRandomSalt();
            this.settings.SaltedPasswordHash = Crypto.ComputeSaltedHash(password, this.settings.Salt);
        }

        /// <summary>
        /// Erzeugt eine <see cref="KeyLocker"/>-Instanz aus den Daten von <paramref name="element"/>.
        /// </summary>
        /// <param name="element">Die Datenquelle.</param>
        /// <returns>Die erezugte Instanz.</returns>
        public static KeyLockerCore Load(JsonElement element)
        {
            var res = new KeyLockerCore()
            {
                settings = AppSettings.Load(element.GetProperty(nameof(KeyLockerCore.settings))),
            };

            foreach (var entryElement in element.GetProperty(nameof(KeyLockerCore.Entries)).EnumerateArray())
            {
                res.entries.Add(Entry.Load(entryElement));
            }

            return res;
        }

        /// <summary>
        /// Holt eine Übersicht über ungespeicherte Änderungen.
        /// </summary>
        public IPendingChanges PendingChanges
        {
            get
            {
                return this.pendingChanges;
            }
        }

        /// <summary>
        /// Holt die Einstellungen der Bibliothek.
        /// </summary>
        public PasswordSettings PasswordSettings
        {
            get
            {
                return this.settings.PasswordSettings;
            }
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
        /// Fügt einen neuen Eintrag hinzu, wenn dieser noch nicht vorhanden ist.
        /// Das Passwort wird verschlüsselt und beim Eintrag gesetzt.
        /// </summary>
        /// <param name="entry">Der Eintrag.</param>
        /// <param name="rawPassword">Das unverschlüsselte Passwort.</param>
        public bool Add(Entry entry, string rawPassword)
        {
            if (this.entries.Contains(entry))
            {
                return false;
            }

            entry.EncryptedPassword = Crypto.Encrypt(rawPassword, this.settings.SaltedPasswordHash);

            this.entries.Add(entry);
            this.pendingChanges.NotifyAdd(entry);

            entry.PropertyChanged += this.HandlePropertyChanged;

            return true;
        }

        /// <summary>
        /// Entfernt einen Eintrag.
        /// </summary>
        /// <param name="entry">Der zu entfernende Eintrag.</param>
        public void Remove(Entry entry)
        {
            this.entries.Remove(entry);

            entry.PropertyChanged -= this.HandlePropertyChanged;

            this.pendingChanges.NotifyDelete(entry);
        }

        /// <summary>
        /// Prüft, ob <paramref name="password"/> das Passwort der aktuellen Instanz ist.
        /// </summary>
        /// <param name="password">Das zu prüfende Passwort.</param>
        /// <returns>True, wenn das Passwort stimmt, sonst false.</returns>
        public bool ConfirmPassword(string password)
        {
            return Crypto.ComputeSaltedHash(password, this.settings.Salt) == this.settings.SaltedPasswordHash;
        }

        /// <summary>
        /// Schreibt die Daten in 
        /// </summary>
        /// <param name="writer"></param>
        public void Save(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(nameof(this.settings));

            this.settings.Save(writer);

            writer.WritePropertyName(nameof(this.Entries));
            writer.WriteStartArray();

            foreach (var entry in this.entries)
            {
                entry.Save(writer);
            }

            writer.WriteEndArray();
            writer.WriteEndObject();

            this.pendingChanges.NotifySave();
        }

        /// <summary>
        /// Behandelt die Änderung eines Eintrags.
        /// </summary>
        /// <param name="sender">Der geänderte Eintrag.</param>
        /// <param name="e">Die Parameter.</param>
        private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                this.pendingChanges.NotifyMofication(entry);
            }
            else if (sender is PasswordSettings)
            {
                this.pendingChanges.NotifySettingsChanged();
            }
        }
    }
}
