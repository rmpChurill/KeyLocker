using System.ComponentModel;
using System.Xml;

namespace KeyLocker
{
    public class Entry
    {
        public Entry()
        {
            this.Name = string.Empty;
            this.Comment = string.Empty;
            this.Password = string.Empty;
            this.Login = string.Empty;
        }

        public Entry(Entry copy)
        {
            this.Name = copy.Name;
            this.Comment = copy.Comment;
            this.Password = copy.Password;
            this.Login = copy.Login;
        }

        public Entry(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.SetMethod != null && property.Name.ToLower().Equals(childNode.Name))
                    {
                        property.SetValue(this, Util.Decode(childNode.InnerText));
                    }
                }
            }
        }

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
            get;
            set;
        }

        [DisplayName("Login")]
        public string Login
        {
            get;
            set;
        }

        [Browsable(false)]
        public string Password
        {
            get;
            set;
        }

        [DisplayName("Password")]
        public string MaskedPassword
        {
            get
            {
                if (string.IsNullOrEmpty(this.Password))
                {
                    return string.Empty;
                }
                else
                {
                    return new string('*', this.Password.Length);
                }
            }
        }

        [DisplayName("Comment")]
        public string Comment
        {
            get;
            set;
        }
    }
}
