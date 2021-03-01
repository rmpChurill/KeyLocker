namespace KeyLocker
{
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
        }

        /// <summary>
        /// Holt den Passwort-Hash mit Salt.
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
    }
}
