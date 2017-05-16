using System.Collections.Generic;
using System.Linq;

namespace Rhyous.EasyCsv
{
    public class RowCollection<T> : List<Row<T>>, IParent<ICsv>
    {
        public RowCollection() { }
        public RowCollection(ICsv parent) { Parent = parent; }
        public ICsv Parent { get; set; }

        public void Add(List<T> row)
        {
            var newRow = ConvertRow(row);
            base.Add(newRow);
        }

        public void AddRange(List<List<T>> rows)
        {
            var list = new List<Row<T>>();
            foreach (var row in rows)
            {
                list.Add(ConvertRow(row));
            }
            AddRange(list);
        }

        private Row<T> ConvertRow(List<T> row)
        {
            var newRow = new Row<T>(Parent);
            newRow.AddRange(row);
            if (Parent.HasHeaderRow)
            {
                if (newRow.Count < Parent.Headers.Count)
                    newRow.AddRange(Enumerable.Repeat(default(T), Parent.Headers.Count - newRow.Count));
            }
            return newRow;
        }
    }
}
