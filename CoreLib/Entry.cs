namespace KeyLocker
{
    using KeyLocker.Utility;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Stellt einen Eintrag der Passwortliste dar.
    /// </summary>
    [DebuggerDisplay("Name={name}")]
    public partial class Entry : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Der Name.
        /// </summary>
        private string name;

        /// <summary>
        /// Der Kommentar.
        /// </summary>
        private string comment;

        /// <summary>
        /// Das verschlüsselte Passwort.
        /// </summary>
        private string encryptedPassword;

        /// <summary>
        /// Der Login-Name.
        /// </summary>
        private string login;

        /// <summary>
        /// Das Datum der letzten Änderung.
        /// </summary>
        private DateTime lastUpdateDate;

        /// <summary>
        /// Besondere Einstellungen für dieses Passwort.
        /// </summary>
        private PartialPasswordSettings customSettings;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        public Entry()
        {
            this.name = string.Empty;
            this.comment = string.Empty;
            this.encryptedPassword = string.Empty;
            this.login = string.Empty;
            this.lastUpdateDate = DateTime.MinValue;
            this.customSettings = new PartialPasswordSettings();
        }

        /// <summary>
        /// Holt oder setzt den Namen des Eintrags.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!string.Equals(this.name, value))
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt den Login-Namen.
        /// </summary>
        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                if (!string.Equals(this.login, value))
                {
                    this.login = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt das verschlüsselte Passwort.
        /// </summary>
        public string EncryptedPassword
        {
            get
            {
                return this.encryptedPassword;
            }

            set
            {
                if (!string.Equals(this.encryptedPassword, value))
                {
                    this.encryptedPassword = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt den Kommentar.
        /// </summary>
        public string Comment
        {
            get
            {
                return this.comment;
            }

            set
            {
                if (!string.Equals(this.comment, value))
                {
                    this.comment = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt das Datum an dem das Passwort des Eintrags zum letzten Mal geändert wurde.
        /// </summary>
        public DateTime LastUpdateDate
        {
            get
            {
                return this.lastUpdateDate;
            }

            set
            {
                if (!DateTime.Equals(this.lastUpdateDate, value))
                {
                    this.lastUpdateDate = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt die Einstellungen dieses Passworts.
        /// </summary>
        public PartialPasswordSettings CustomSettings
        {
            get
            {
                return this.customSettings;
            }

            set
            {
                if (this.customSettings != value)
                {
                    this.customSettings = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
