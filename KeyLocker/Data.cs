namespace KeyLocker
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public static class Data
    {
        private static List<Entry> entries = new List<Entry>();

        public static IList<Entry> Entries
        {
            get
            {
                return entries;
            }
        }

        public static void Save()
        {
            var doc = default(XmlDocument);
            var writer = default(StreamWriter);

            try
            {
                var file = Settings.DataFile;

                if (!File.Exists(file))
                {
                    Util.CreateFile(file);
                }

                doc = new XmlDocument();
                doc.AppendChild(doc.CreateElement("entries"));

                foreach (var entry in entries)
                {
                    doc.DocumentElement.AppendChild(entry.ToXml(doc));
                }

                writer = new StreamWriter(file, false);
                doc.Save(writer);
            }
            catch (Exception e)
            {
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
        }

        internal static void Check()
        {
            if(!Settings.Instance.WarnForOldPasswords)
            {
                return;
            }

            foreach(var entry in entries)
            {
                if(entry.IsOutdated)
                {
                    MessageBox.Show("There are outdated Entries!");

                    break;
                }
            }
        }

        public delegate void HandleDataChanged();
        public static event HandleDataChanged DataChanged;

        public static void Load()
        {
            entries = new List<Entry>();
            var doc = default(XmlDocument);

            try
            {
                var file = Settings.DataFile;

                if (File.Exists(file))
                {
                    doc = new XmlDocument();
                    doc.Load(file);

                    entries.Clear();

                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    {
                        entries.Add(new Entry(node));
                    }
                }
            }
            catch (XmlException)
            { }
            catch (Exception e)
            {
                ErrorHandler.HandleError(e);
            }

            DataChanged?.Invoke();
        }
    }
}
