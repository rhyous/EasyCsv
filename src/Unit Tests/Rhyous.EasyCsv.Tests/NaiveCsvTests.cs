using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Rhyous.EasyCsv.Tests
{
    [TestClass]
    public class NaiveCsvTests
    {
        [TestMethod]
        public void NaiveCsvTest()
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
            // Act
            var csv = new NaiveCsv(@"Data\Naive.csv");

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            CollectionAssert.AreEqual(lines, csv.Lines);
            Assert.AreEqual(headers.Count, csv.Columns);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void NaiveCsvNoHeaderTest()
        {
            // Arrange
            var lines = new List<string>() { "A,B,C", "D,E,F", "G,H,I", "J,K,L" };
            var rows = new List<List<string>>()
                        {
                            new List<string> { "A", "B", "C" },
                            new List<string> { "D", "E", "F" },
                            new List<string> { "G", "H", "I" },
                            new List<string> { "J", "K", "L" }
                        };
            // Act
            var csv = new NaiveCsv(@"Data\NaiveNoHeader.csv", false);

            // Assert
            Assert.AreEqual(csv.Rows[0].Count, csv.Columns);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void TabNaiveCsvTest()
        {
            // Arrange
            var headers = new List<string> { "H1", "H2", "H3" };
            var lines = new List<string>() { "H1\tH2\tH3", "A\tB\tC", "D\tE\tF", "G\tH\tI", "J\tK\tL" };
            var rows = new List<List<string>>()
                        {
                            new List<string> { "A", "B", "C" },
                            new List<string> { "D", "E", "F" },
                            new List<string> { "G", "H", "I" },
                            new List<string> { "J", "K", "L" }
                        };
            // Act
            var csv = new NaiveCsv(@"Data\TabNaive.csv", true, '\t');

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void TabNaiveCsvNoHeaderTest()
        {
            // Arrange
            var lines = new List<string>() { "A\tB\tC", "D\tE\tF", "G\tH\tI", "J\tK\tL" };
            var rows = new List<List<string>>()
                        {
                            new List<string> { "A", "B", "C" },
                            new List<string> { "D", "E", "F" },
                            new List<string> { "G", "H", "I" },
                            new List<string> { "J", "K", "L" }
                        };
            // Act
            var csv = new NaiveCsv(@"Data\TabNaiveNoHeader.csv", false, '\t');

            // Assert
            CollectionAssert.AreEqual(lines, csv.Lines);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void NaiveCsvGetRowDataByHeaderStringIndexerTest()
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
            // Act
            var csv = new Csv(@"Data\Naive.csv");

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            Assert.AreEqual(headers.Count, csv.Columns);
            var i = 0;
            foreach (var row in rows)
            {
                int j = 0;
                foreach (var header in csv.Headers)
                {
                    Assert.AreEqual(rows[i][j++], csv.Rows[i][header]);
                }
                i++;

            }
        }
    }
}