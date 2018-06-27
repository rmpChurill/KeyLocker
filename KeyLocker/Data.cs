namespace KeyLocker
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using KeyLocker.Lib;

    public class Data
    {
        private static Data instance;

        private List<Entry> entries;
        private List<Entry> filteredEntries;
        private IFilter<Entry> filter;
        private IComparer<Entry> comparer;

        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }

                return instance;
            }
        }

        public Data()
        {
            this.entries = new List<Entry>();
        }


        public IList<Entry> Entries
        {
            get
            {
                return this.entries;
            }
        }

        public IList<Entry> FilteredEntries
        {
            get
            {
                if (this.filteredEntries == null)
                {
                    this.CreateFilteredData();
                }

                return this.filteredEntries;
            }
        }

        public void ApplyFilter(IFilter<Entry> filter)
        {
            this.filteredEntries = null;
            this.filter = filter;
        }

        public void RemoveFilter()
        {
            this.filteredEntries = null;
            this.filter = null;
        }

        public void Sort(IComparer<Entry> comparer)
        {
            this.filteredEntries = null;
            this.comparer = comparer;
        }

        public void RemoveSorting()
        {
            this.comparer = null;
            this.filteredEntries = null;
        }

        public void Save()
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

                foreach (var entry in this.entries)
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

        internal void Check()
        {
            if (!Settings.Instance.WarnForOldPasswords)
            {
                return;
            }

            foreach (var entry in this.entries)
            {
                if (entry.IsOutdated)
                {
                    MessageBox.Show("There are outdated Entries!");

                    break;
                }
            }
        }

        public delegate void HandleDataChanged();
        public event HandleDataChanged DataChanged;

        public void Load()
        {
            this.entries = new List<Entry>();
            var doc = default(XmlDocument);

            try
            {
                var file = Settings.DataFile;

                if (File.Exists(file))
                {
                    doc = new XmlDocument();
                    doc.Load(file);

                    this.entries.Clear();

                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    {
                        this.entries.Add(new Entry(node));
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

        private void CreateFilteredData()
        {
            if (this.filter == null && this.comparer == null)
            {
                this.filteredEntries = this.entries;
            }
            else
            {
                this.filteredEntries = new List<Entry>();

                if (this.filter != null)
                {
                    foreach(var entry in this.entries)
                    {
                        if(this.filter.IsValid(entry))
                        {
                            this.filteredEntries.Add(entry);
                        }
                    }
                }

                if(this.comparer != null)
                {
                    this.filteredEntries.Sort(this.comparer);
                }
            }
        }
    }
}
