namespace KeyLocker.CoreLib.Validation
{
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
        /// <param name="password">Das zu validierende Passwort.</param>
        /// <param name="settings">Die Standardeinstellungen.</param>
        /// <returns>Eine Liste von Validierungsergebnissen.</returns>
        public static IEnumerable<IValidationResult> ValidatePassword(string password, PasswordSettings settings)
        {
            var validationResults = new List<IValidationResult>();

            switch (settings.LowerCaseChars)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(password, Definitions.LowerCaseChars, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(password, Definitions.LowerCaseChars, validationResults);
                    break;
            }

            switch (settings.UpperCaseChars)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(password, Definitions.UpperCaseChars, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(password, Definitions.UpperCaseChars, validationResults);
                    break;
            }

            switch (settings.Digits)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(password, Definitions.Digits, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(password, Definitions.Digits, validationResults);
                    break;
            }

            switch (settings.SpecialCharacters)
            {
                case Usage.Allow:
                    break;
                case Usage.Forbid:
                    CheckForbiddenChars(password, settings.AllowedSpecialCharacters, validationResults);
                    break;
                case Usage.Require:
                    CheckRequiredChars(password, settings.AllowedSpecialCharacters, validationResults);
                    break;
            }

            CheckForbiddenChars(password, settings.ForbiddenCharacters, validationResults);

            if (password.Length < settings.MinLength)
            {
                validationResults.Add(new TooShortResult(password.Length, (int)settings.MinLength));
            }

            if (password.Length > settings.MaxLength)
            {
                validationResults.Add(new TooLongResult(password.Length, (int)settings.MaxLength));
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
