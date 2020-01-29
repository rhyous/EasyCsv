using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Rhyous.EasyCsv.Tests.Extensions;

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
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", NewLine.Get()),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", NewLine.Get()),
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
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", NewLine.Get()),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", NewLine.Get()),
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
                            new List<string> { $"She went to the{Environment.NewLine}store.", "New line character in the middle of the sentence" }
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
                CollectionAssert.AreEqual(rows[i], row, $"Expected values:{Environment.NewLine}" 
                                                      + $"{string.Join(",",rows[i])}{Environment.NewLine}"
                                                      + $"Actual Values:{Environment.NewLine}"
                                                      + $"{string.Join(",", row)}");
                i++;
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
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", NewLine.Get()),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", NewLine.Get()),
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
        }

        [TestMethod]
        public void ComplexCsvFromStreamTest()
        {
            // Arrange         
            var headers = new List<string> { "H1", "H2", "H3" };
            var rows = new List<List<string>>()
                        {
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", NewLine.Windows()),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", NewLine.Windows()),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", NewLine.Windows()),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", NewLine.Windows()),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", NewLine.Windows()),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", NewLine.Windows()),
                                }
                        };
            // Act

            var csvAsString = "H1,H2,H3\r\n\"A\r\nB\r\nC\",\"D\r\nE\r\nF\",\"G\r\nH\r\nI\"\r\n\"J\r\nK\r\nL\",\"M\r\nN\r\nO\",\"P\r\nQ\r\nR\"\r\n";
            var csv = new Csv(csvAsString.AsStream());

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
        public void ComplexCsvGetRowDataByHeaderStringIndexerTest()
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

        [TestMethod]
        public void ComplexCsvForceSpaceInHeaderTest()
        {
            // Arrange         
            var headers = new List<string> { "H1", "H2", " H3 " };
            var rows = new List<List<string>>()
                        {
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "A", "B", "C", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "D", "E", "F", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "G", "H", "I", NewLine.Get()),
                                },
                            new List<string>
                                {
                                    string.Format("{0}{3}{1}{3}{2}", "J", "K", "L", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "M", "N", "O", NewLine.Get()),
                                    string.Format("{0}{3}{1}{3}{2}", "P", "Q", "R", NewLine.Get()),
                                }
                        };
            // Act
            var csv = new Csv(@"Data\ComplexForceSpaceInHeader.csv");

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
        public void SO5865747CsvTest()
        {
            // Arrange         
            var headers = new List<string>();
            var rows = new List<List<string>>()
                        {
                            new List<string>
                            {
                                "1"
                                ,"1/2/2010"
                                ,"The sample (\"adasdad\") asdada"
                                ,"I was pooping in the door \"Stinky\", so I'll be damn"
                                ,"AK"
                            }
                        };
            // Act
            var csv = new Csv(@"Data\SO5865747.csv", new CsvParserUnescapedQuotes(), false);

            // Assert
            CollectionAssert.AreEqual(headers, csv.Headers);
            Assert.AreEqual(rows.Count, csv.Rows.Count);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.AreEqual(rows[i++], row);
            }
        }
    }
}
