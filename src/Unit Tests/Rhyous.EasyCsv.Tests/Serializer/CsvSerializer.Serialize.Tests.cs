using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.EasyCsv.Tests
{
    [TestClass]
    public class CsvSerializer_Serializer_Tests
    {
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

        [TestMethod]
        public void CsvSerializer_Serialize_Enumerable_Test()
        {
            // Arrange
            var simpleObjects = new List<SimpleObject>
            {
                new SimpleObject { Id = 27, Name = "Simple Object 27" },
                new SimpleObject { Id = 28, Name = "Simple Object 28" }
            };
            var csvSerializer = new CsvSerializer();
            var expected = "27,Simple Object 27" + Environment.NewLine
                         + "28,Simple Object 28" + Environment.NewLine;

            // Act
            var actual = csvSerializer.Serialize(simpleObjects);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
