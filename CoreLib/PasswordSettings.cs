namespace KeyLocker
{
    using System;
    using System.Text.Json;

    using KeyLocker.Utility;

    /// <summary>
    /// Stellt Einstellungen für ein Passwort dar.
    /// </summary>
    public class PasswordSettings
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        public PasswordSettings()
        {
            this.Digits = Usage.Require;
            this.UpperCaseChars = Usage.Allow;
            this.LowerCaseChars = Usage.Allow;
            this.SpecialCharacters = Usage.Allow;
            this.MinLength = 8;
            this.MaxLength = 32;
            this.DecayTime = new CustomTimeSpan(90, CustomTimeSpanKind.Days);
            this.ForbiddenCharacters = Array.Empty<char>();
            this.AllowedSpecialCharacters = Definitions.Digits;
        }

        /// <summary>
        /// Liest ein <see cref="PasswordSettings"/> aus einem <see cref="JsonElement"/>.
        /// </summary>
        /// <param name="element">Die Datenquelle.</param>
        /// <returns>Die gelesene Instanz.</returns>
        public static PasswordSettings Read(JsonElement element)
        {
            return new PasswordSettings
            {
                AllowedSpecialCharacters = element.GetProperty(nameof(PartialPasswordSettings.AllowedSpecialCharacters)).GetString()?.ToCharArray() ?? throw new Exception(),
                ForbiddenCharacters = element.GetProperty(nameof(PartialPasswordSettings.ForbiddenCharacters)).GetString()?.ToCharArray() ?? throw new Exception(),
                DecayTime = CustomTimeSpan.Parse(element.GetProperty(nameof(PartialPasswordSettings.DecayTime)).GetString() ?? throw new Exception()),
                Digits = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.Digits)).GetString() ?? throw new Exception()),
                LowerCaseChars = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.LowerCaseChars)).GetString() ?? throw new Exception()),
                SpecialCharacters = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.SpecialCharacters)).GetString() ?? throw new Exception()),
                UpperCaseChars = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.UpperCaseChars)).GetString() ?? throw new Exception()),
                MaxLength = element.GetProperty(nameof(PartialPasswordSettings.MaxLength)).GetUInt32(),
                MinLength = element.GetProperty(nameof(PartialPasswordSettings.MinLength)).GetUInt32()
            };
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Großbuchstaben.
        /// </summary>
        public Usage UpperCaseChars
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Kleinbuchstaben.
        /// </summary>
        public Usage LowerCaseChars
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Ziffern.
        /// </summary>
        public Usage Digits
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Sonderzeichen
        /// </summary>
        public Usage SpecialCharacters
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Mindestlänge des Passworts. 
        /// </summary>
        public uint MinLength
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Maximallänge des Passworts.
        /// </summary>
        public uint MaxLength
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt eine Liste von verbotenen Zeichen.
        /// </summary>
        public char[] ForbiddenCharacters
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt eine Liste von erlaubten Sonderzeichen.
        /// </summary>
        public char[] AllowedSpecialCharacters
        {
            get;
            set;
        }

        /// <summary>
        /// Holt oder setzt die Zeitspanne, nach der das Passwort als veraltet gilt.
        /// </summary>
        public CustomTimeSpan DecayTime
        {
            get;
            set;
        }

        /// <summary>
        /// Holt eine Array mit allen Zeichen, die für ein Passwort mit den Einstellungen der aktuellen Instanz erlaubt sind.
        /// </summary>
        /// <returns>Ein Array aller erlaubten Zeichen.</returns>
        public char[] GetAllowedCharacters()
        {
            var allowedChars = string.Empty;

            if (this.Digits != Usage.Forbid)
            {
                allowedChars += Definitions.Digits;
            }

            if (this.LowerCaseChars != Usage.Forbid)
            {
                allowedChars += Definitions.LowerCaseChars;
            }

            if (this.UpperCaseChars != Usage.Forbid)
            {
                allowedChars += Definitions.UpperCaseChars;
            }

            if (this.SpecialCharacters != Usage.Forbid)
            {
                allowedChars += this.SpecialCharacters;
            }

            foreach (var c in this.ForbiddenCharacters)
            {
                var index = allowedChars.IndexOf(c);

                if (index >= 0)
                {
                    allowedChars = allowedChars.Remove(index, 1);
                }
            }

            return allowedChars.ToCharArray();
        }

        /// <summary>
        /// Erstellt eine Kopie der aktuellen Instanz.
        /// </summary>
        /// <returns>Eine flache Kopie der aktuellen Instanz.</returns>
        public PasswordSettings Clone()
        {
            return new PasswordSettings()
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

        /// <summary>
        /// Erzeugt eine neue <see cref="PartialPasswordSettings"/>-Instanz mit den Werten der aktuellen Instanz.
        /// </summary>
        /// <returns>Eine <see cref="PartialPasswordSettings"/>-Instanz mit den Werten der aktuellen Instanz.</returns>
        public PartialPasswordSettings CopyAsPartial()
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

        /// <summary>
        /// Erzeugt eine neue <see cref="PasswordSettings"/>-Instanz die die Daten von <paramref name="partial"/> enthält und alle null-Werte
        /// mit den entsprechenden Werten der aktuellen Instanz auffüllt.
        /// </summary>
        /// <returns>Eine neue <see cref="PasswordSettings"/>-Instanz.</returns>
        public PasswordSettings Fill(PartialPasswordSettings partial)
        {
            return new PasswordSettings()
            {
                AllowedSpecialCharacters = partial.AllowedSpecialCharacters ?? this.AllowedSpecialCharacters,
                DecayTime = partial.DecayTime ?? this.DecayTime,
                Digits = partial.Digits ?? this.Digits,
                ForbiddenCharacters = partial.ForbiddenCharacters ?? this.ForbiddenCharacters,
                LowerCaseChars = partial.LowerCaseChars ?? this.LowerCaseChars,
                MaxLength = partial.MaxLength ?? this.MaxLength,
                MinLength = partial.MinLength ?? this.MinLength,
                SpecialCharacters = partial.SpecialCharacters ?? this.SpecialCharacters,
                UpperCaseChars = partial.UpperCaseChars ?? this.UpperCaseChars,
            };
        }

        /// <summary>
        /// Schreibt die Daten der Instanz nach <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Das Ausgabeziel.</param>
        public void Save(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(this.AllowedSpecialCharacters), this.AllowedSpecialCharacters);
            writer.WriteString(nameof(this.DecayTime), this.DecayTime.ToString());
            writer.WriteString(nameof(this.Digits), Enum.GetName(this.Digits));
            writer.WriteString(nameof(this.LowerCaseChars), Enum.GetName(this.LowerCaseChars));
            writer.WriteString(nameof(this.SpecialCharacters), Enum.GetName(this.SpecialCharacters));
            writer.WriteString(nameof(this.UpperCaseChars), Enum.GetName(this.UpperCaseChars));
            writer.WriteString(nameof(this.ForbiddenCharacters), this.ForbiddenCharacters);
            writer.WriteNumber(nameof(this.MaxLength), this.MaxLength);
            writer.WriteNumber(nameof(this.MinLength), this.MinLength);

            writer.WriteEndObject();
        }
    }
}
