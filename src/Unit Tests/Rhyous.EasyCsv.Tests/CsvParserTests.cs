using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Rhyous.EasyCsv.Tests.Extensions;

namespace Rhyous.EasyCsv.Tests
{
    [TestClass]
    public class CsvParserTests
    {
        [TestMethod]
        public void ParseSimpleCsv()
        {
            // Arrange
            var csvAsString = "Col1, Col2, Col3\r\n1, A, B\r\n2,C,D";
            var csvParser = new CsvParser();
            // Act
            var rows = csvParser.GetRowsFromStream(new StreamReader(csvAsString.AsStream()));

            // Assert
            Assert.AreEqual(3, rows.Count);
        }

        [TestMethod]
        public void ParseSimple_1Row_Csv()
        {
            // Arrange
            var csvAsString = "H1\r\n1";
            var csvParser = new CsvParser();

            // Act
            var rows = csvParser.GetRowsFromStream(new StreamReader(csvAsString.AsStream()));

            // Assert
            Assert.AreEqual(2, rows.Count);
        }

        [TestMethod]
        public void ParseSimpleCsvNoDataInLastColumn()
        {
            // Arrange
            var csvAsString = "Col1, Col2, Col3\r\n1, A,\r\n2,C,D\r\n3,E,\r\n4,G,\r\n5,I,J";
            var csvParser = new CsvParser();
            // Act
            var rows = csvParser.GetRowsFromStream(new StreamReader(csvAsString.AsStream()));

            // Assert
            Assert.AreEqual(6, rows.Count);
        }
    }
}
