using System.Collections.Generic;

namespace LazyDev.Core.Utility
{
    public class AsciiSortDictionary<TValue>:SortedDictionary<string, TValue>
    {
        public AsciiSortDictionary()
        :base(new AsciiComparer() )
        {
            
        }
    }

    public class AsciiComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.CompareOrdinal(x, y);
        }
    }
}
