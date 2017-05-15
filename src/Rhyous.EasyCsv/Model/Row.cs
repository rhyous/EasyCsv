using System;
using System.Collections.Generic;

namespace Rhyous.EasyCsv
{
    public class Row<T> : List<T>, IParent<ICsv>
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
            get
            {
                var id = Parent.Headers.IndexOf(header);
                if (id < 0 && (Parent != null && Parent.ThrowExceptionOnMissingHeader))
                    throw new HeaderMissingException("Header not found: " + header);
                return id >= 0 ? this[id] : default(T);
            }
            set { this[Parent.Headers.IndexOf(header)] = value; }
        }
    }
}
