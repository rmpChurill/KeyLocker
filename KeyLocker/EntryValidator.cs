using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using KeyLocker.Lib;
using KeyLocker.Properties;

namespace KeyLocker
{
    internal class EntryValidator : IElementValidator
    {
        private readonly Entry entry;
        private bool dirty;
        private readonly List<IValidationItem> validationResults;

        public EntryValidator(Entry entry)
        {
            this.entry = entry;
            this.dirty = true;
            this.validationResults = new List<IValidationItem>();

            this.entry.PropertyChanged += this.HandleEntryPropertyChanged;
        }

        private void HandleEntryPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.dirty = true;
        }

        public IValidationItem[] Validate()
        {
            if (this.dirty)
            {
                this.CreateValidationResults();
                this.dirty = false;
            }

            return this.validationResults.ToArray();
        }

        private void CreateValidationResults()
        {
            this.validationResults.Clear();

            if (this.entry.IsOutdated)
            {
                this.validationResults.Add(new OutDatedValidationResult());
            }

            this.ValidateLength(this.entry.Password, this.validationResults);
            this.ValidateDigits(this.entry.Password, this.validationResults);
            this.ValidateUpperCaseCharacters(this.entry.Password, this.validationResults);
            this.ValidateSpecialCharacters(this.entry.Password, this.validationResults);
            this.ValidateForbiddenDigits(this.entry.Password, this.validationResults);
        }

        private void ValidateLength(string text, List<IValidationItem> messages)
        {
            if (text.Length < this.entry.ApplicableSettings.MinLength)
            {
                ////messages.Add(string.Format("The password must be at least {0} characters long!", Settings.Instance.MinLength));
                messages.Add(new LengthValidationResult());
            }

            if (text.Length >= this.entry.ApplicableSettings.MaxLength)
            {
                ////messages.Add(string.Format("The password must not be longer than {0} characters!", Settings.Instance.MaxLength));
                messages.Add(new LengthValidationResult());
            }
        }

        private void ValidateSpecialCharacters(string text, List<IValidationItem> messages)
        {
            if (this.entry.ApplicableSettings.SpecialCharacters == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.SpecialCharacters.IndexOf(text[i]) != -1)
                    {
                        ////messages.Add(string.Format("Must not contain special character '{0}'!", text[i]));
                        messages.Add(new SpecialCharactersValidationResult());
                    }
                }
            }
            else if (this.entry.ApplicableSettings.SpecialCharacters == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.SpecialCharacters.ToCharArray()) == -1)
                {
                    ////messages.Add(string.Format("Missing special character! ({0})", Definitions.SpecialCharacters));
                    messages.Add(new SpecialCharactersValidationResult());
                }
            }
        }

        private void ValidateUpperCaseCharacters(string text, List<IValidationItem> messages)
        {
            if (this.entry.ApplicableSettings.UpperCaseChars == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.UpperCaseChars.IndexOf(text[i]) != -1)
                    {
                        ////messages.Add(string.Format("Must not contain uppercase character '{0}'!", text[i]));
                        messages.Add(new CaseValidationResult());
                    }
                }
            }
            else if (this.entry.ApplicableSettings.UpperCaseChars == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.UpperCaseChars.ToCharArray()) == -1)
                {
                    ////messages.Add(string.Format("Missing uppercase character! ({0})", Definitions.UpperCaseChars));
                    messages.Add(new CaseValidationResult());
                }
            }
        }

        private void ValidateDigits(string text, List<IValidationItem> messages)
        {
            if (this.entry.ApplicableSettings.Digits == Usage.Forbid)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (Definitions.Digits.IndexOf(text[i]) != -1)
                    {
                        ////messages.Add(string.Format("Must not contain digit '{0}'!", text[i]));
                        messages.Add(new DigitValidationResult());
                    }
                }
            }
            else if (this.entry.ApplicableSettings.Digits == Usage.Require)
            {
                if (text.IndexOfAny(Definitions.Digits.ToCharArray()) == -1)
                {
                    ////messages.Add(string.Format("Missing digit! ({0})", Definitions.Digits));
                    messages.Add(new DigitValidationResult());
                }
            }
        }

        private void ValidateForbiddenDigits(string text, List<IValidationItem> messages)
        {
            if (this.entry.ApplicableSettings.ForbiddenCharacters.Length > 0)
            {
                foreach (var c in this.entry.ApplicableSettings.ForbiddenCharacters)
                {
                    if (text.IndexOf(c) >= 0)
                    {
                        //messages.Add(string.Format("Forbidden digit found ({0})", c));
                        messages.Add(new ForbiddenDigitResult());
                    }
                }
            }
        }

        private class ForbiddenDigitResult : IValidationItem
        {
            public ForbiddenDigitResult()
            {
            }

            public string Description
            {
                get
                {
                    return "forbidden digit [TODO]";
                }
            }

            public int Severity
            {
                get
                {
                    return 8;
                }
            }

            public Image Icon
            {
                get
                {
                    return Resources.UnknownError_16px;
                }
            }
        }

        private class LengthValidationResult : IValidationItem
        {
            private readonly string description;

            public LengthValidationResult()
            {

            }

            public string Description
            {
                get
                {
                    return "length [TODO]";
                }
            }

            public int Severity
            {
                get
                {
                    return 8;
                }
            }

            public Image Icon
            {
                get
                {
                    return Resources.LengthError_16px;
                }
            }
        }

        private class MultipleValidationResult<T> : IValidationItem
            where T : IValidationItem
        {
            private readonly List<T> items;
            private string description;
            private int severity;
            private Image icon;

            public MultipleValidationResult()
            {
                this.items = new List<T>();
                this.description = string.Empty;
                this.icon = Resources.UnknownError_16px;
                this.severity = 0;
            }

            public void Add(T item)
            {
                this.items.Add(item);

                if (string.IsNullOrEmpty(this.description))
                {
                    this.description = item.Description;
                }
                else
                {
                    this.description += "\n" + item.Description;
                }

                this.severity = Math.Max(this.severity, item.Severity);
                this.icon = item.Icon;
            }

            public string Description
            {
                get
                {
                    return this.description;
                }
            }

            public int Severity
            {
                get
                {
                    return this.severity;
                }
            }

            public Image Icon
            {
                get
                {
                    return this.icon;
                }
            }
        }

        private class SpecialCharactersValidationResult : IValidationItem
        {
            public string Description
            {
                get
                {
                    return "special chars [TODO]";
                }
            }

            public int Severity
            {
                get
                {
                    return 8;
                }
            }

            public Image Icon
            {
                get
                {
                    return Resources.SpecialCharError_16px;
                }
            }
        }

        private class CaseValidationResult : IValidationItem
        {
            public string Description
            {
                get
                {
                    return "case [TODO]";
                }
            }

            public int Severity
            {
                get
                {
                    return 8;
                }
            }

            public Image Icon
            {
                get
                {
                    return Resources.CaseError_16px;
                }
            }
        }

        private class DigitValidationResult : IValidationItem
        {
            public string Description
            {
                get
                {
                    return "numbers [TODO]";
                }
            }

            public int Severity
            {
                get
                {
                    return 8;
                }
            }

            public Image Icon
            {
                get
                {
                    return Resources.DigitError_16px;
                }
            }
        }

        private class OutDatedValidationResult : IValidationItem
        {
            public string Description
            {
                get
                {
                    return "date [TODO]";
                }
            }

            public int Severity
            {
                get
                {
                    return 8;
                }
            }

            public Image Icon
            {
                get
                {
                    return Resources.TimeError_16px;
                }
            }
        }
    }
}
