using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.EasyCsv.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToCsv<T>(this IEnumerable<T> ts, string delimn = ",", string newLine = "\r\n", bool includeHeaders = true, IComparer<string> comparer = null)
            where T : class
        {
            var csv = string.Empty;
            var headers = typeof(T).GetHeaders(comparer);
            if (includeHeaders)
                csv += string.Join(delimn, headers) + newLine;
            if (ts == null || !ts.Any())
                return csv;
            csv += string.Join(newLine, ts.Select(t => t.ToCsvRow(headers, delimn)));
            return csv;
        }
    }
}
