using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using KeyLocker.Lib;

namespace KeyLocker
{
    [DebuggerDisplay("Name={name}")]
    public partial class Entry : NotifyPropertyChangedBase
    {
        private string name;
        private string comment;
        private string password;
        private string login;
        private DateTime date;
        private readonly EntryValidator validator;

        public Entry()
        {
            this.name = string.Empty;
            this.comment = string.Empty;
            this.password = string.Empty;
            this.login = string.Empty;
            this.date = DateTime.MinValue;
            this.validator = new EntryValidator(this);
        }

        public Entry(Entry copy)
        {
            this.name = copy.Name;
            this.comment = copy.Comment;
            this.password = copy.Password;
            this.login = copy.Login;
            this.date = DateTime.Now;
            this.validator = new EntryValidator(this);
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

            this.validator = new EntryValidator(this);
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
    }
}
