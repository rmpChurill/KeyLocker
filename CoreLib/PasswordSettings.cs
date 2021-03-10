namespace KeyLocker
{
    using System;
    using System.Text.Json;

    using KeyLocker.Utility;

    /// <summary>
    /// Stellt Einstellungen für ein Passwort dar.
    /// </summary>
    public class PasswordSettings : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Die Nutzungsrichtlinie für Großbuchstaben.
        /// </summary>
        private Usage upperCaseChars;

        /// <summary>
        /// Die Nutzungsrichtlinie für Kleinbuchstaben.
        /// </summary>
        private Usage lowerCaseChars;

        /// <summary>
        /// Die Nutzungsrichtlinie für Ziffern.
        /// </summary>
        private Usage digits;

        /// <summary>
        /// Die Nutzungsrichtlinie für Sonderzeichen.
        /// </summary>
        private Usage specialCharacters;

        /// <summary>
        /// Die Mindestlänge des Passworts. 
        /// </summary>
        private uint minLength;

        /// <summary>
        /// Die Maximallänge des Passworts. 
        /// </summary>
        private uint maxLength;

        /// <summary>
        /// Eine Liste von verbotenen Zeichen.
        /// </summary>
        private char[] forbiddenCharacters;

        /// <summary>
        /// Eine Liste von erlaubten Sonderzeichen.
        /// </summary>
        private char[] allowedSpecialCharacters;

        /// <summary>
        /// Die Zeitspanne, nach der das Passwort als veraltet gilt.
        /// </summary>
        private CustomTimeSpan decayTime;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        public PasswordSettings()
        {
            this.digits = Usage.Require;
            this.upperCaseChars = Usage.Allow;
            this.lowerCaseChars = Usage.Allow;
            this.specialCharacters = Usage.Allow;
            this.minLength = 8;
            this.maxLength = 32;
            this.decayTime = new CustomTimeSpan(90, CustomTimeSpanKind.Days);
            this.forbiddenCharacters = Array.Empty<char>();
            this.allowedSpecialCharacters = Definitions.Digits;
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
                allowedSpecialCharacters = element.GetProperty(nameof(PartialPasswordSettings.AllowedSpecialCharacters)).GetString()?.ToCharArray() ?? throw new Exception(),
                forbiddenCharacters = element.GetProperty(nameof(PartialPasswordSettings.ForbiddenCharacters)).GetString()?.ToCharArray() ?? throw new Exception(),
                decayTime = CustomTimeSpan.Parse(element.GetProperty(nameof(PartialPasswordSettings.DecayTime)).GetString() ?? throw new Exception()),
                digits = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.Digits)).GetString() ?? throw new Exception()),
                lowerCaseChars = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.LowerCaseChars)).GetString() ?? throw new Exception()),
                specialCharacters = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.SpecialCharacters)).GetString() ?? throw new Exception()),
                upperCaseChars = Enum.Parse<Usage>(element.GetProperty(nameof(PartialPasswordSettings.UpperCaseChars)).GetString() ?? throw new Exception()),
                maxLength = element.GetProperty(nameof(PartialPasswordSettings.MaxLength)).GetUInt32(),
                minLength = element.GetProperty(nameof(PartialPasswordSettings.MinLength)).GetUInt32()
            };
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Großbuchstaben.
        /// </summary>
        public Usage UpperCaseChars
        {
            get
            {
                return this.upperCaseChars;
            }

            set
            {
                if (this.upperCaseChars != value)
                {
                    this.upperCaseChars = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Kleinbuchstaben.
        /// </summary>
        public Usage LowerCaseChars
        {
            get
            {
                return this.lowerCaseChars;
            }

            set
            {
                if (this.lowerCaseChars != value)
                {
                    this.lowerCaseChars = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Ziffern.
        /// </summary>
        public Usage Digits
        {
            get
            {
                return this.digits;
            }

            set
            {
                if (this.digits != value)
                {
                    this.digits = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt die Nutzungsrichtlinie für Sonderzeichen
        /// </summary>
        public Usage SpecialCharacters
        {
            get
            {
                return this.specialCharacters;
            }

            set
            {
                if (this.specialCharacters != value)
                {
                    this.specialCharacters = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt die Mindestlänge des Passworts. 
        /// </summary>
        public uint MinLength
        {
            get
            {
                return this.minLength;
            }

            set
            {
                if (this.minLength != value)
                {
                    this.minLength = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt die Maximallänge des Passworts.
        /// </summary>
        public uint MaxLength
        {
            get
            {
                return this.maxLength;
            }

            set
            {
                if (this.maxLength != value)
                {
                    this.maxLength = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt eine Liste von verbotenen Zeichen.
        /// </summary>
        public char[] ForbiddenCharacters
        {
            get
            {
                return this.forbiddenCharacters;
            }

            set
            {
                if (this.forbiddenCharacters != value)
                {
                    this.forbiddenCharacters = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt eine Liste von erlaubten Sonderzeichen.
        /// </summary>
        public char[] AllowedSpecialCharacters
        {
            get
            {
                return this.allowedSpecialCharacters;
            }

            set
            {
                if (this.allowedSpecialCharacters != value)
                {
                    this.allowedSpecialCharacters = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holt oder setzt die Zeitspanne, nach der das Passwort als veraltet gilt.
        /// </summary>
        public CustomTimeSpan DecayTime
        {
            get
            {
                return this.decayTime;
            }

            set
            {
                if (this.decayTime != value)
                {
                    this.decayTime = value;
                    this.OnPropertyChanged();
                }
            }
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
                allowedSpecialCharacters = this.AllowedSpecialCharacters,
                decayTime = this.DecayTime,
                digits = this.Digits,
                forbiddenCharacters = this.ForbiddenCharacters,
                lowerCaseChars = this.LowerCaseChars,
                maxLength = this.MaxLength,
                minLength = this.MinLength,
                specialCharacters = this.SpecialCharacters,
                upperCaseChars = this.UpperCaseChars,
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
                allowedSpecialCharacters = partial.AllowedSpecialCharacters ?? this.AllowedSpecialCharacters,
                decayTime = partial.DecayTime ?? this.DecayTime,
                digits = partial.Digits ?? this.Digits,
                forbiddenCharacters = partial.ForbiddenCharacters ?? this.ForbiddenCharacters,
                lowerCaseChars = partial.LowerCaseChars ?? this.LowerCaseChars,
                maxLength = partial.MaxLength ?? this.MaxLength,
                minLength = partial.MinLength ?? this.MinLength,
                specialCharacters = partial.SpecialCharacters ?? this.SpecialCharacters,
                upperCaseChars = partial.UpperCaseChars ?? this.UpperCaseChars,
            };
        }

        /// <summary>
        /// Schreibt die Daten der Instanz nach <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Das Ausgabeziel.</param>
        public void Save(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(this.AllowedSpecialCharacters), this.allowedSpecialCharacters);
            writer.WriteString(nameof(this.DecayTime), this.decayTime.ToString());
            writer.WriteString(nameof(this.Digits), Enum.GetName(this.digits));
            writer.WriteString(nameof(this.LowerCaseChars), Enum.GetName(this.lowerCaseChars));
            writer.WriteString(nameof(this.SpecialCharacters), Enum.GetName(this.specialCharacters));
            writer.WriteString(nameof(this.UpperCaseChars), Enum.GetName(this.upperCaseChars));
            writer.WriteString(nameof(this.ForbiddenCharacters), this.forbiddenCharacters);
            writer.WriteNumber(nameof(this.MaxLength), this.maxLength);
            writer.WriteNumber(nameof(this.MinLength), this.minLength);

            writer.WriteEndObject();
        }
    }
}
