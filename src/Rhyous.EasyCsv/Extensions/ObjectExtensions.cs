using Rhyous.StringLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhyous.EasyCsv.Extensions
{
    public static class ObjectExtensions
    {
        private const string Quote = "\"";

        public static string ToCsvRow(this object o, IEnumerable<string> headers, string delimn = ",")
        {
            if (o == null)
            {
                if (headers == null || ! headers.Any())
                    return string.Empty;
                return string.Join(delimn, headers.Select(h => ""));
            }
            var row = string.Empty;
            var headerArray = headers?.ToArray();
            for (int i = 0; i < headerArray.Length; i++)
            {
                var header = headerArray[i];
                if (i > 0)
                    row += delimn;
                var col = o.GetPropertyValue(header)?.ToString();
                if (col == null)
                    col = "";
                else if (col.Contains(delimn) && !col.IsWrapped(Quote))
                    col = col.Wrap(Quote);
                row += col;
            }
            return row;
        }
    }
}
