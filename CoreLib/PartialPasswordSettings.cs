namespace KeyLocker
{
    using System;
    using System.Text.Json;

    using KeyLocker.Utility;

    /// <summary>
    /// Stellt Einstellungen für ein Passwort dar.
    /// </summary>
    public class PartialPasswordSettings
    {
        /// <summary>
        /// Liest ein <see cref="PartialPasswordSettings"/> aus einem <see cref="JsonElement"/>.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static PartialPasswordSettings Read(JsonElement element)
        {
            var res = new PartialPasswordSettings();

            if (element.TryGetProperty(nameof(PartialPasswordSettings.AllowedSpecialCharacters), out var property))
            {
                res.AllowedSpecialCharacters = property.GetString()?.ToCharArray();
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.DecayTime), out property))
            {
                var value = property.GetString();

                if (value != null)
                {
                    res.DecayTime = CustomTimeSpan.Parse(value);
                }
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.Digits), out property))
            {
                var value = property.GetString();

                if (value != null)
                {
                    res.Digits = Enum.Parse<Usage>(value);
                }
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.ForbiddenCharacters), out property))
            {
                res.ForbiddenCharacters = property.GetString()?.ToCharArray();
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.LowerCaseChars), out property))
            {
                var value = property.GetString();

                if (value != null)
                {
                    res.LowerCaseChars = Enum.Parse<Usage>(value);
                }
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.MaxLength), out property))
            {
                res.MaxLength = property.GetUInt32();
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.MinLength), out property))
            {
                res.MinLength = property.GetUInt32();
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.SpecialCharacters), out property))
            {
                var value = property.GetString();

                if (value != null)
                {
                    res.SpecialCharacters = Enum.Parse<Usage>(value);
                }
            }

            if (element.TryGetProperty(nameof(PartialPasswordSettings.UpperCaseChars), out property))
            {
                var value = property.GetString();

                if (value != null)
                {
                    res.UpperCaseChars = Enum.Parse<Usage>(value);
                }
            }

            return res;
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
        public CustomTimeSpan? DecayTime
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

        /// <summary>
        /// Schreibt die Daten der Instanz in <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Das Ausgabeziel.</param>
        public void Save(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();

            var allowedSpecialCharacters = this.AllowedSpecialCharacters;

            if (allowedSpecialCharacters != null)
            {
                writer.WriteString(nameof(this.AllowedSpecialCharacters), allowedSpecialCharacters);
            }

            var decayTime = this.DecayTime;

            if (decayTime != null)
            {
                writer.WriteString(nameof(this.DecayTime), decayTime.ToString());
            }

            var digits = this.Digits;

            if (digits != null)
            {
                writer.WriteString(nameof(this.Digits), Enum.GetName((Usage)digits));
            }

            var lowerCaseChars = this.LowerCaseChars;

            if (lowerCaseChars != null)
            {
                writer.WriteString(nameof(this.LowerCaseChars), Enum.GetName((Usage)lowerCaseChars));
            }

            var specialCharacters = this.SpecialCharacters;

            if (specialCharacters != null)
            {
                writer.WriteString(nameof(this.SpecialCharacters), Enum.GetName((Usage)specialCharacters));
            }

            var upperCaseChars = this.UpperCaseChars;

            if (upperCaseChars != null)
            {
                writer.WriteString(nameof(this.UpperCaseChars), Enum.GetName((Usage)upperCaseChars));
            }

            var forbiddenCharacters = this.ForbiddenCharacters;

            if (forbiddenCharacters != null)
            {
                writer.WriteString(nameof(this.ForbiddenCharacters), forbiddenCharacters);
            }

            var maxLength = this.MaxLength;

            if (maxLength != null)
            {
                writer.WriteNumber(nameof(this.MaxLength), (uint)maxLength);
            }

            var minLength = this.MinLength;

            if (minLength != null)
            {
                writer.WriteNumber(nameof(this.MinLength), (uint)minLength);
            }

            writer.WriteEndObject();
        }
    }
}
