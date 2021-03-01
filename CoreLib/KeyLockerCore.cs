namespace KeyLocker.CoreLib
{
    /// <summary>
    /// Diese Klasse stellt die Schnittstelle zur KeyLocker-Bibliothek dar.
    /// </summary>
    public class KeyLockerCore
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="settings"></param>
        public KeyLockerCore(AppSettings settings)
        {
            this.Settings = settings;
        }

        /// <summary>
        /// Holt die Einstellungen der Bibliothek.
        /// </summary>
        public AppSettings Settings
        {
            get;
        }
    }
}
