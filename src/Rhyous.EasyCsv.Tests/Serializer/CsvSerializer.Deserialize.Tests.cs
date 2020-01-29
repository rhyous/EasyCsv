using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.EasyCsv.Tests
{
    [TestClass]
    public partial class CsvSerializer_Deserialize_Tests
    {

        [TestMethod]
        public void CsvSerializer_Deserialize_SimpleObject_Test()
        {
            // Arrange
            var csv = "Id,Name" + Environment.NewLine
                    + "27,Simple Object 27";
            var expected = new SimpleObject { Id = 27, Name = "Simple Object 27" };
            var csvSerializer = new CsvSerializer();

            // Act
            var actual = csvSerializer.Deserialize<SimpleObject>(csv);

            // Assert
            Assert.AreEqual(expected.Id, actual.Id, nameof(expected.Id) + "property should match");
            Assert.AreEqual(expected.Name, actual.Name, nameof(expected.Name) + "property should match");
        }

        [TestMethod]
        public void CsvSerializer_Deserialize_Enumerable_Test()
        {
            // Arrange
            var expectedList = new List<SimpleObject>
            {
                new SimpleObject { Id = 27, Name = "Simple Object 27" },
                new SimpleObject { Id = 28, Name = "Simple Object 28" }
            };
            var csvSerializer = new CsvSerializer();
            var csv = "Id,Name" + Environment.NewLine
                    + "27,Simple Object 27" + Environment.NewLine
                    + "28,Simple Object 28" + Environment.NewLine;

            // Act
            var actualList = csvSerializer.Deserialize<List<SimpleObject>>(csv);

            // Assert
            for (int i = 0; i < actualList.Count; i++)
            {
                var actual = actualList[i];
                var expected = expectedList[i];
                Assert.AreEqual(expected.Id, actual.Id, nameof(expected.Id) + "property should match");
                Assert.AreEqual(expected.Name, actual.Name, nameof(expected.Name) + "property should match");
            }
        }
    }
}
