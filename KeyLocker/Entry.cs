using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using KeyLocker.Lib;
using KeyLocker.Properties;

namespace KeyLocker
{
    public class Entry : NotifyPropertyChangedBase
    {
        private string name;
        private string comment;
        private string password;
        private string login;
        private DateTime date;
        private ElementValidator validator;

        public Entry()
        {
            this.name = string.Empty;
            this.comment = string.Empty;
            this.password = string.Empty;
            this.login = string.Empty;
            this.date = DateTime.MinValue;
            this.validator = new ElementValidator(this);
        }

        public Entry(Entry copy)
        {
            this.name = copy.Name;
            this.comment = copy.Comment;
            this.password = copy.Password;
            this.login = copy.Login;
            this.date = DateTime.Now;
            this.validator = new ElementValidator(this);
        }

        public Entry(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.SetMethod != null && property.Name.ToLower().Equals(childNode.Name))
                    {
                        Util.Set(property, this, Util.Decode(childNode.InnerText));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public XmlNode ToXml(XmlDocument parent)
        {
            var node = parent.CreateElement(nameof(Entry).ToLower());

            foreach (var property in this.GetType().GetProperties())
            {
                if (property.GetMethod != null && property.SetMethod != null)
                {
                    var newNode = parent.CreateElement(property.Name.ToLower());
                    var value = property.GetValue(this);

                    newNode.InnerText = value != null ? Util.Encode(value.ToString()) : string.Empty;
                    node.AppendChild(newNode);
                }
            }

            return node;
        }

        [DisplayName("Name")]
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

        [DisplayName("Login")]
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

        [Browsable(false)]
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (!string.Equals(this.password, value))
                {
                    this.password = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayName("Password")]
        public string MaskedPassword
        {
            get
            {
                if (string.IsNullOrEmpty(this.password))
                {
                    return string.Empty;
                }
                else
                {
                    return new string('*', this.password.Length);
                }
            }
        }

        [DisplayName("Comment")]
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

        [DisplayName("Date")]
        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                if (!DateTime.Equals(this.date, value))
                {
                    this.date = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayName("Validation")]
        public IElementValidator Validator
        {
            get
            {
                return this.validator;
            }
        }

        [Browsable(false)]
        public bool IsOutdated
        {
            get
            {
                return Util.IsOutdated(this.Date, DateTime.Now, Settings.Instance.DecayTime, Settings.Instance.DecayTimeUnit);
            }
        }

        private class ElementValidator : IElementValidator
        {
            private readonly Entry entry;
            private bool dirty;
            private List<IValidationItem> validationResults;

            public ElementValidator(Entry entry)
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
                    this.validationResults.Add(new OutDatetValidationResult());
                }

                ////TODO: Debugging only
                this.validationResults.Add(new OutDatetValidationResult());
            }

            private class OutDatetValidationResult : IValidationItem
            {
                public string Description
                {
                    get
                    {
                        return "The passwrod is older than [TODO]";
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
                        return Resources.Time_16x;
                    }
                }
            }
        }
    }
}
