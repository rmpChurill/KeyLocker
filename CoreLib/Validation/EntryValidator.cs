namespace KeyLocker.CoreLib.Validation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Validiert eine <see cref="Entry"/>-Instanz.
    /// </summary>
    public static class EntryValidator
    {
        /// <summary>
        /// Validiert eine <see cref="Entry"/>-Instanz entsprechend den Einstellungen in 
        /// <see cref="Entry.CustomSettings"/> und <paramref name="settings"/>.
        /// </summary>
        /// <param name="entry">Der zu validierende Eintrag.</param>
        /// <param name="settings">Die Standardeinstellungen.</param>
        /// <param name="now">Der Zeitpunkt an dem die Validierung ausgerichtet werden soll.</param>
        /// <returns>Eine Liste von Validierungsergebnissen.</returns>
        public static List<IValidationResult> Validate(Entry entry, PasswordSettings settings, DateTime now)
        {
            var validationResults = new List<IValidationResult>();
            var filledSettings = settings.Fill(entry.CustomSettings);

            // TODO
            //if (filledSettings.DecayTime < CustomDateTime.Difference(now, entry.LastUpdateDate))
            //{
            //    validationResults.Add(new OutdatedResult(timeSinceLastUpdate));
            //}

            switch (filledSettings.LowerCaseChars)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(entry.EncryptedPassword, Definitions.LowerCaseChars, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(entry.EncryptedPassword, Definitions.LowerCaseChars, validationResults);
                    break;
            }

            switch (filledSettings.UpperCaseChars)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(entry.EncryptedPassword, Definitions.UpperCaseChars, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(entry.EncryptedPassword, Definitions.UpperCaseChars, validationResults);
                    break;
            }

            switch (filledSettings.Digits)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(entry.EncryptedPassword, Definitions.Digits, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(entry.EncryptedPassword, Definitions.Digits, validationResults);
                    break;
            }

            switch (filledSettings.SpecialCharacters)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(entry.EncryptedPassword, filledSettings.AllowedSpecialCharacters, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(entry.EncryptedPassword, filledSettings.AllowedSpecialCharacters, validationResults);
                    break;
            }

            CheckForbiddenChars(entry.EncryptedPassword, filledSettings.ForbiddenCharacters, validationResults);

            if (entry.EncryptedPassword.Length < filledSettings.MinLength)
            {
                validationResults.Add(new TooShortResult(entry.EncryptedPassword.Length, (int)filledSettings.MinLength));
            }

            if (entry.EncryptedPassword.Length > filledSettings.MaxLength)
            {
                validationResults.Add(new TooLongResult(entry.EncryptedPassword.Length, (int)filledSettings.MaxLength));
            }

            return validationResults;
        }

        /// <summary>
        /// Prüft, ob <paramref name="password"/> keines der Zeichen in <paramref name="chars"/> enthält und fügt 
        /// <see cref="InvalidCharacterResult"/> zu <paramref name="results"/> hinzu, falls dies nicht der Fall ist.
        /// </summary>
        /// <param name="password">Das zu prüfende Passwort.</param>
        /// <param name="chars">Eine Sammlung von Zeichen, die nicht enthalten sein dürfen.</param>
        /// <param name="results">Die Liste von Validierungsergebnissen.</param>
        private static void CheckForbiddenChars(string password, char[] chars, List<IValidationResult> results)
        {
            foreach (var c in chars)
            {
                if (password.Contains(c))
                {
                    results.Add(new InvalidCharacterResult(c));
                }
            }
        }


        /// <summary>
        /// Prüft, ob <paramref name="password"/> mindestens eines der Zeichen in <paramref name="chars"/> enthält und fügt 
        /// <see cref="MissingRequiredCharacterResult"/> zu <paramref name="results"/> hinzu, falls dies nicht der Fall ist.
        /// </summary>
        /// <param name="password">Das zu prüfende Passwort.</param>
        /// <param name="chars">Eine Sammlung von Zeichen, die enthalten sein müssen.</param>
        /// <param name="results">Die Liste von Validierungsergebnissen.</param>
        private static void CheckRequiredChars(string password, char[] chars, List<IValidationResult> results)
        {
            var anyCharContained = false;

            foreach (var c in chars)
            {
                if (password.Contains(c))
                {
                    anyCharContained = true;
                    break;
                }
            }

            if (!anyCharContained)
            {
                results.Add(new MissingRequiredCharacterResult(chars));
            }
        }
    }
}
