﻿namespace KeyLocker
{
    using System;
    using System.Text.Json;

    using KeyLocker.Utility;

    /// <summary>
    /// Stellt die App-Einstellungen dar.
    /// </summary>
    public class AppSettings : NotifyPropertyChangedBase
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
        public static AppSettings Load(JsonElement element)
        {
            return new AppSettings
            {
                Salt = element.GetProperty(nameof(AppSettings.Salt)).GetString() ?? throw new Exception(),
                SaltedPasswordHash = element.GetProperty(nameof(AppSettings.SaltedPasswordHash)).GetString() ?? throw new Exception(),
                PasswordSettings = PasswordSettings.Read(element.GetProperty(nameof(AppSettings.PasswordSettings))),
            };
        }

        /// <summary>
        /// Holt oder setzt den Hash des Passworts mit Salt.
        /// </summary>
        internal string SaltedPasswordHash
        {
            get;
            set;
        }

        /// <summary>
        /// Holt das Salt.5
        /// </summary>
        internal string Salt
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die allgemeingültigen Passworteinstellungen.
        /// </summary>
        internal PasswordSettings PasswordSettings
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
            writer.WriteStartObject();
            writer.WriteString(nameof(this.Salt), this.Salt);
            writer.WriteString(nameof(this.SaltedPasswordHash), this.SaltedPasswordHash);
            writer.WritePropertyName(nameof(this.PasswordSettings));

            this.PasswordSettings.Save(writer);

            writer.WriteEndObject();
        }
    }
}
