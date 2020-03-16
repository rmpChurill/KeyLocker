namespace KeyLocker
{
    using System;
    using System.IO;
    using System.Text;

    public class PasswordSettings
    {
        public void LoadFromString(string value)
        {
            var lines = value.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split('=');

                if (parts.Length != 2)
                {
                    continue;
                }

                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.GetMethod != null && property.SetMethod != null && property.Name.Equals(parts[0]))
                    {
                        Util.Set(property, this, parts[1]);
                    }
                }
            }
        }

        public bool Load()
        {
            var res = true;
            var reader = default(StreamReader);

            try
            {
                Directories.EnsureSettingsFileExists();

                reader = new StreamReader(File.OpenRead(Directories.SettingsFile));
                this.LoadFromString(reader.ReadToEnd());
            }
            catch (IOException e)
            {
                res = false;
                ErrorHandler.HandleError(e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            return res;
        }

        public bool Save()
        {
            var res = true;
            var writer = default(StreamWriter);

            try
            {
                Directories.EnsureDataFileExists();

                writer = new StreamWriter(File.OpenWrite(Directories.SettingsFile));
                writer.Write(this.SaveToString());
            }
            catch (IOException e)
            {
                res = false;
                ErrorHandler.HandleError(e);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }

            return res;
        }

        public string SaveToString()
        {
            var sb = new StringBuilder();

            foreach (var property in this.GetType().GetProperties())
            {
                if (property.GetMethod != null && property.SetMethod != null)
                {
                    var value = property.GetValue(this);

                    if (value != null)
                    {
                        sb.AppendFormat("{0}={1}\n", property.Name, value.ToString());
                    }
                }
            }

            return sb.ToString();
        }

        public PasswordSettings()
        {
            this.Digits = Usage.Require;
            this.UpperCaseChars = Usage.Allow;
            this.SpecialCharacters = Usage.Allow;
            this.MinLength = 16;
            this.MaxLength = 32;
            this.DecayTime = 1;
            this.DecayTimeUnit = TimeUnit.Years;
            this.WarnIfOld = true;
            this.ForbiddenCharacters = string.Empty;
            this.AllowedSpecialCharacters = Definitions.Digits;
        }

        public PasswordSettings(PasswordSettings copy)
        {
            this.CopyFrom(copy);
        }

        public Usage UpperCaseChars
        {
            get;
            set;
        }

        public Usage Digits
        {
            get;
            set;
        }

        public Usage SpecialCharacters
        {
            get;
            set;
        }

        public int MinLength
        {
            get;
            set;
        }

        public int MaxLength
        {
            get;
            set;
        }

        public string ForbiddenCharacters
        {
            get;
            set;
        }

        public string AllowedSpecialCharacters
        {
            get;
            set;
        }

        public int DecayTime
        {
            get;
            set;
        }

        public TimeUnit DecayTimeUnit
        {
            get;
            set;
        }

        public bool WarnIfOld
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.SaveToString();
        }


        internal void CopyFrom<T>(T copy)
        {
            Type type;

            if (this.GetType().IsAssignableFrom(copy.GetType()))
            {
                type = this.GetType();
            }
            else if (copy.GetType().IsAssignableFrom(this.GetType()))
            {
                type = copy.GetType();
            }
            else
            {
                throw new NotSupportedException();
            }

            foreach (var property in type.GetProperties())
            {
                if (property.SetMethod != null && property.GetMethod != null)
                {
                    property.SetValue(this, property.GetValue(copy));
                }
            }
        }
    }
}
