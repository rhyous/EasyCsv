using System;

namespace Rhyous.EasyCsv.Tests.Model
{
    public class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class B
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int? NullableInt { get; set;}
        public Guid Guid { get; set; }
    }

    public class UnorderedPropertyObject
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
