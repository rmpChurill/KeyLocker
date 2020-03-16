namespace KeyLocker
{
    public class AppSettings : PasswordSettings
    {
        private static AppSettings instance;

        public static AppSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppSettings();
                    if (!instance.Load())
                    {
                        // reset on fail
                        instance = new AppSettings();
                    }
                }

                return instance;
            }
        }

        public AppSettings() : base()
        { 
            this.SaltedPasswordHash = string.Empty;
            this.Salt = string.Empty;
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

        public override string ToString()
        {
            return this.SaveToString();
        }
    }
}
