using System.Data;
using System.Linq;

namespace Rhyous.EasyCsv
{
    public static class CsvExtensions
    {
        public static DataTable ToDataTable(this ICsv csv)
        {
            if (csv.Headers.Count == 0 && csv.Rows.Count == 0)
                return null;
            var dt = new DataTable();
            var headers = (csv.Headers.Count > 0) ? csv.Headers.Select(h => new DataColumn(h)) : csv.Rows[0].Select(c => new DataColumn());
            dt.Columns.AddRange(headers.ToArray());
            foreach (var csvRow in csv.Rows)
            {
                var row = dt.NewRow();
                row.ItemArray = csvRow.ToArray();
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
