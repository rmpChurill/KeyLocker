namespace KeyLocker
{
    using System;
    using System.IO;

    public class Settings
    {
        private static Settings instance;

        public static string DataDir
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Keylocker");
            }
        }

        public static string SettingsFile
        {
            get
            {
                return Path.Combine(DataDir, "settings.txt");
            }
        }

        public static string DataFile
        {
            get
            {
                return Path.Combine(DataDir, "data.xml");
            }
        }

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();

                    if (!instance.Load())
                    {
                        instance = DefaultSettings;
                    }
                }

                return instance;
            }
        }

        public bool Load()
        {
            var res = true;
            var reader = default(StreamReader);

            try
            {
                this.EnsureDataFileExists();

                reader = new StreamReader(File.OpenRead(SettingsFile));

                while (!reader.EndOfStream)
                {
                    var parts = reader.ReadLine().Split('=');

                    if (parts.Length != 2)
                    {
                        continue;
                    }

                    foreach (var property in this.GetType().GetProperties())
                    {
                        if (property.GetMethod != null && property.SetMethod != null && property.Name.Equals(parts[0]))
                        {
                            if (property.PropertyType.Equals(typeof(string)))
                            {
                                property.SetValue(this, parts[1]);
                            }
                            else if (property.PropertyType.Equals(typeof(Usage)))
                            {
                                property.SetValue(this, Enum.Parse(typeof(Usage), parts[1]));
                            }
                            else if (property.PropertyType.Equals(typeof(int)))
                            {
                                property.SetValue(this, int.Parse(parts[1]));
                            }
                            else
                            {
                                throw new NotSupportedException();
                            }
                        }
                    }
                }
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

        private void EnsureDataFileExists()
        {
            if (!File.Exists(DataFile))
            {
                File.Create(DataFile);
            }
        }

        public bool Save()
        {
            var res = true;
            var writer = default(StreamWriter);
            var currentInstance = new Settings(Instance);

            try
            {
                this.EnsureDataFileExists();

                writer = new StreamWriter(File.OpenWrite(SettingsFile));

                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.GetMethod != null && property.SetMethod != null)
                    {
                        writer.WriteLine(string.Format("{0}={1}", property.Name, property.GetValue(currentInstance).ToString()));
                    }
                }
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

        public static Settings DefaultSettings
        {
            get
            {
                return new Settings()
                {
                    Digits = Usage.Require,
                    UpperCaseChars = Usage.Allow,
                    SpecialCharacters = Usage.Allow,
                    MinLength = 16,
                    MaxLength = 32,
                    SaltedPasswordHash = string.Empty,
                    Salt = string.Empty,
                };
            }
        }

        private Settings()
        {
        }

        public Settings(Settings copy)
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

        public string SaltedPasswordHash
        {
            get;
            set;
        }

        public string Salt
        {
            get;
            set;
        }

        internal void CopyFrom(Settings copy)
        {
            foreach (var property in copy.GetType().GetProperties())
            {
                if (property.SetMethod != null && property.GetMethod != null)
                {
                    property.SetValue(this, property.GetValue(copy));
                }
            }
        }
    }
}
