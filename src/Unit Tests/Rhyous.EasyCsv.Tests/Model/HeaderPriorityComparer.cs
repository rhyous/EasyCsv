using System.Collections.Generic;

namespace Rhyous.EasyCsv.Tests.Model
{
    public class HeaderPriorityComparer : IComparer<string>
    {
        Dictionary<string, int> PriorityProperties = new Dictionary<string, int>
        {
            { "Id", 1 },
            { "Name", 2 },
            { "Description", 3 }
        };

        public int Compare(string x, string y)
        {
            PriorityProperties.TryGetValue(x, out int priorityX);
            PriorityProperties.TryGetValue(y, out int priorityY);
            if (priorityX != 0 && priorityY != 0)
                return priorityX.CompareTo(priorityY);
            if (priorityX != 0)
                return -1;
            if (priorityY != 0)
                return 1;
            return x.CompareTo(y);
        }
    }
}
