namespace KeyLocker.CoreLib
{
    using System.Collections.Generic;
    using System.Text.Json;

    /// <summary>
    /// Diese Klasse stellt die Schnittstelle zur KeyLocker-Bibliothek dar.
    /// </summary>
    public class KeyLockerCore
    {
        /// <summary>
        /// Die registrierten Einträge.
        /// </summary>
        private readonly List<Entry> entries;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        private KeyLockerCore()
        {
            this.Settings = new AppSettings();
            this.entries = new List<Entry>();
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="password">Das Passwort für die neu erstellte Passwortdatei.</param>
        public KeyLockerCore(string password)
        {
            this.Settings = new AppSettings();
            this.entries = new List<Entry>();

            this.Settings.Salt = Crypto.ComputeRandomSalt();
            this.Settings.SaltedPasswordHash = Crypto.ComputeSaltedHash(password, this.Settings.Salt);
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
                Settings = AppSettings.Load(element.GetProperty(nameof(KeyLockerCore.Settings))),
            };

            foreach (var entryElement in element.GetProperty(nameof(KeyLockerCore.Entries)).EnumerateArray())
            {
                res.entries.Add(Entry.Load(entryElement));
            }

            return res;
        }

        /// <summary>
        /// Holt die Einstellungen der Bibliothek.
        /// </summary>
        public AppSettings Settings
        {
            get;
            private init;
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
            entry.EncryptedPassword = Crypto.Encrypt(rawPassword, this.Settings.SaltedPasswordHash);

            this.entries.Add(entry);
        }

        /// <summary>
        /// Prüft, ob <paramref name="password"/> das Passwort der aktuellen Instanz ist.
        /// </summary>
        /// <param name="password">Das zu prüfende Passwort.</param>
        /// <returns>True, wenn das Passwort stimmt, sonst false.</returns>
        public bool ConfirmPassword(string password)
        {
            return Crypto.ComputeSaltedHash(password, this.Settings.Salt) == this.Settings.SaltedPasswordHash;
        }

        /// <summary>
        /// Schreibt die Daten in 
        /// </summary>
        /// <param name="writer"></param>
        public void Save(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(nameof(this.Settings));
            this.Settings.Save(writer);

            writer.WritePropertyName(nameof(this.Entries));
            writer.WriteStartArray();

            foreach (var entry in this.entries)
            {
                entry.Save(writer);
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
