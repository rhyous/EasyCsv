using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.EasyCsv.Tests.Serializer
{
    [TestClass]
    public class CsvSerializerTests
    {
        public class SimpleObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        [TestMethod]
        public void CsvSerializer_Serialize_SimpleObject_Test()
        {
            // Arrange
            var simpleObject = new SimpleObject { Id = 27, Name = "Simple Object 27" };
            var csvSerializer = new CsvSerializer();
            var expected = "27,Simple Object 27";

            // Act
            var actual = csvSerializer.Serialize(simpleObject);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
