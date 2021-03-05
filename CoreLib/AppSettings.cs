namespace KeyLocker
{
    using System.Text.Json;

    /// <summary>
    /// Stellt die App-Einstellungen dar.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Erzeugt eine neue Instanz 
        /// </summary>
        public AppSettings()
        {
            this.SaltedPasswordHash = string.Empty;
            this.Salt = string.Empty;
            this.PasswordSettings = new PasswordSettings();
        }

        /// <summary>
        /// Erzeugt eine <see cref="AppSettings"/>-Instanz aus den Daten in <paramref name="element"/>.
        /// </summary>
        /// <param name="element">Die Datenquelle.</param>
        /// <returns>Die gelesene Instanz.</returns>
        public static AppSettings Parse(JsonElement element)
        {
            var res = new AppSettings();

            return res;
        }

        /// <summary>
        /// Holt den Hash des Passworts mit Salt.
        /// </summary>
        public string SaltedPasswordHash
        {
            get;
            set;
        }

        /// <summary>
        /// Holt das Salt.
        /// </summary>
        public string Salt
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die allgemeingültigen Passworteinstellungen.
        /// </summary>
        public PasswordSettings PasswordSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Schreibt die Daten der aktuellen Instanz nach <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Das Ausgabziel.</param>
        public void Save(Utf8JsonWriter writer)
        {
            writer.WriteString(nameof(this.Salt), this.Salt);
            writer.WriteString(nameof(this.SaltedPasswordHash), this.SaltedPasswordHash);

            writer.WritePropertyName(nameof(this.PasswordSettings));

            this.PasswordSettings.Save(writer);
        }
    }
}
