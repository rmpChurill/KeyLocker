namespace KeyLocker
{
    using System;
    using KeyLocker.Lib;

    public class EntryNameFilter : IFilter<Entry>
    {
        private readonly string searchString;

        public EntryNameFilter(string filter)
        {
            this.searchString = filter.Trim().ToLowerInvariant();
        }

        public bool IsValid(Entry item)
        {
            return item.Name.IndexOf(this.searchString, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
