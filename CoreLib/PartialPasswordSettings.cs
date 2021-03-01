namespace KeyLocker
{
    using System;

    /// <summary>
    /// Stellt Einstellungen für ein Passwort dar.
    /// </summary>
    public class PartialPasswordSettings
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        public PartialPasswordSettings()
        {
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Großbuchstaben.
        /// </summary>
        public Usage? UpperCaseChars
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Kleinbuchstaben.
        /// </summary>
        public Usage? LowerCaseChars
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Ziffern.
        /// </summary>
        public Usage? Digits
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Sonderzeichen
        /// </summary>
        public Usage? SpecialCharacters
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Mindestlänge des Passworts. 
        /// </summary>
        public uint? MinLength
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Maximallänge des Passworts.
        /// </summary>
        public uint? MaxLength
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt eine Liste von verbotenen Zeichen.
        /// </summary>
        public char[]? ForbiddenCharacters
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt eine Liste von erlaubten Sonderzeichen.
        /// </summary>
        public char[]? AllowedSpecialCharacters
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Zeitspanne, nach der das Passwort als veraltet gilt.
        /// </summary>
        public TimeSpan? DecayTime
        {
            get;
            set;
        }

        /// <summary>
        /// Erstellt eine Kopie der aktuellen Instanz.
        /// </summary>
        /// <returns>Eine flache Kopie der aktuellen Instanz.</returns>
        public PartialPasswordSettings Clone()
        {
            return new PartialPasswordSettings()
            {
                AllowedSpecialCharacters = this.AllowedSpecialCharacters,
                DecayTime = this.DecayTime,
                Digits = this.Digits,
                ForbiddenCharacters = this.ForbiddenCharacters,
                LowerCaseChars = this.LowerCaseChars,
                MaxLength = this.MaxLength,
                MinLength = this.MinLength,
                SpecialCharacters = this.SpecialCharacters,
                UpperCaseChars = this.UpperCaseChars,
            };
        }
    }
}
