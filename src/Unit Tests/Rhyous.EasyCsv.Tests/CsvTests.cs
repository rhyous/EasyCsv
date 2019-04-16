using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.EasyCsv.Tests
{
    [TestClass]
    public class CsvTests
    {
        [TestMethod]
        public void Csv_1column1row_Test()
        {
            // Arrange
            var csv = new Csv(@"Data\1column1row.csv");

            // Act

            // Assert
            Assert.AreEqual(1, csv.Rows.Count);
        }
    }
}
