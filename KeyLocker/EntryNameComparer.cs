namespace KeyLocker
{
    using System.Collections.Generic;

    public class EntryNameComparer : IComparer<Entry>
    {
        public int Compare(Entry x, Entry y)
        {
            return string.Compare(x.Name, y.Name);
        }
    }
}
