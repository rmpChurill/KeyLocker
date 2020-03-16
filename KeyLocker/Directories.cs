using System;
using System.IO;

namespace KeyLocker
{
    public static class Directories
    {
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

        public static void EnsureSettingsFileExists()
        {
            if (!File.Exists(SettingsFile))
            {
                File.Create(SettingsFile);
            }

            while (!File.Exists(SettingsFile))
            { }
        }

        public static void EnsureDataFileExists()
        {
            if (!File.Exists(DataFile))
            {
                File.Create(DataFile);
            }

            while (!File.Exists(DataFile))
            { }
        }
    }
}
