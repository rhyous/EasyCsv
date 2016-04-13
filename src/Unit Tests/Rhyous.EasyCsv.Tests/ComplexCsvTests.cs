using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Rhyous.EasyCsv.Tests
{
    [TestClass]
    public class ComplexCsvTests
    {
        [TestMethod]
        public void ComplexCsvAlsoHandlesNaiveCsvTest()
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
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void ComplexCsvAlsoHandlesNaiveCsvNoHeaderTest()
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
            var csv = new Csv(@"Data\NaiveNoHeader.csv", false);

            // Assert
            Assert.AreEqual(csv.Rows[0].Count, csv.Columns);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void TabComplexCsvAlsoHandlesNaiveCsvTest()
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
            var csv = new Csv(@"Data\TabNaive.csv", true, '\t');

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void TabComplexCsvAlsoHandlesNaiveCsvNoHeaderTest()
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
            var csv = new Csv(@"Data\TabNaiveNoHeader.csv", false, '\t');

            // Assert
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void ComplexCsvTest()
        {
            // Arrange         
            var headers = new List<string> { "H1", "H2", "H3" };
            var rows = new List<List<string>>()
                        {
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", Environment.NewLine),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", Environment.NewLine),
                                }
                        };
            // Act
            var csv = new Csv(@"Data\Complex.csv");

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            Assert.AreEqual(rows.Count, csv.Rows.Count);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void ComplexCsvNoHeaderTest()
        {
            // Arrange         
            var rows = new List<List<string>>()
                        {
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", Environment.NewLine),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", Environment.NewLine),
                                }
                        };
            // Act
            var csv = new Csv(@"Data\ComplexNoHeader.csv", false);

            // Assert
            Assert.AreEqual(0, csv.Headers.Count);
            Assert.AreEqual(rows.Count, csv.Rows.Count);
            var i = 0;
            foreach (var row in rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void ComplexCsvExcelCreatedTyposTest()
        {
            // Arrange         
            var headers = new List<string> { "Typo", "Message" };
            var rows = new List<List<string>>()
                        {
                            new List<string> { "\"Hello, he said.", "Missing end quote" },
                            new List<string> { "Hello,\" he said.", "Missing start quote" },
                            new List<string> { "It was her's.","No apostrophe in hers" },
                            new List<string> { "She went to the\nstore.", "New line character in the middle of the sentence" }
                        };
            // Act
            var csv = new Csv(@"Data\ExcelCreatedTypos.csv");

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            Assert.AreEqual(rows.Count, csv.Rows.Count);
            var i = 0;
            Assert.AreEqual(rows.Count, csv.Rows.Count);
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void ComplexCsvWhiteSpaceInFinalLinesTest()
        {
            // Arrange         
            var headers = new List<string> { "H1", "H2", "H3" };
            var rows = new List<List<string>>()
                        {
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", Environment.NewLine),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", Environment.NewLine),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", Environment.NewLine),
                                }
                        };
            // Act
            var csv = new Csv(@"Data\ComplexWhiteSpaceFinalLines.csv");

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            Assert.AreEqual(rows.Count, csv.Rows.Count);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }

        [TestMethod]
        public void ComplexCsvHeadersOnly()
        {
            // Arrange         
            var headers = new List<string> { "H1", "H2", "H3" };

            // Act
            var csv = new Csv(@"Data\HeaderOnly.csv");

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            Assert.AreEqual(0, csv.Rows.Count);
            var i = 0;
        }
    }
}
