using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Rhyous.EasyCsv.Tests.Model
{
    [TestClass]
    public class RowTests
    {
        [TestMethod]
        public void ColumnDoesntExistReturnNullTest()
        {
            // Arrange
            var headers = new List<string> { "H1", "H2", "H3" };
            var lines = new List<string>() { "H1,H2,H3", "A,B,C", "D,E,F", "G,H,I", "J,K,L" };
            var rows = new List<List<string>>()
                        {
                            new List<string> { "A", "B", "C" },
                            new List<string> { "D", "E", "F" },
                            new List<string> { "G", "H", "I" },
                            new List<string> { "J", "K", "L" }
                        };
            var csv = new Csv(headers);
            csv.Rows.AddRange(rows);

            // Act
            var value = csv.Rows[0]["H4"];

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        [ExpectedException(typeof(HeaderMissingException))]
        public void ColumnDoesntExistThrowsTest()
        {
            // Arrange
            var headers = new List<string> { "H1", "H2", "H3" };
            var lines = new List<string>() { "H1,H2,H3", "A,B,C", "D,E,F", "G,H,I", "J,K,L" };
            var rows = new List<List<string>>()
                        {
                            new List<string> { "A", "B", "C" },
                            new List<string> { "D", "E", "F" },
                            new List<string> { "G", "H", "I" },
                            new List<string> { "J", "K", "L" }
                        };
            var csv = new Csv(headers);
            csv.ThrowExceptionOnMissingHeader = true;
            csv.Rows.AddRange(rows);

            // Act
            var value = csv.Rows[0]["H4"];

            // Assert in test method attribute
        }
    }
}
