using System.Collections.Generic;

namespace Rhyous.EasyCsv
{
    public class Row<T> : List<T>
    {
        public Row()
        { }

        public Row(int capacity) : base(capacity) { }

        public Row(IEnumerable<T> collection) : base(collection) { }

        public Row(ICsv parent)
        {
            Parent = parent;
        }

        public Row(int capacity, ICsv parent) : base(capacity)
        {
            Parent = parent;
        }

        public Row(IEnumerable<T> collection, ICsv parent) : base(collection)
        {
            Parent = parent;
        }

        public ICsv Parent { get; set; }

        public T this[string header]
        {
            get { return this[Parent.Headers.IndexOf(header)]; }
            set { this[Parent.Headers.IndexOf(header)] = value; }
        }
    }
}
