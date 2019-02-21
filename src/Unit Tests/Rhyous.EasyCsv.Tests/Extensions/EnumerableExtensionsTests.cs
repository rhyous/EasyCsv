using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.EasyCsv.Extensions;
using Rhyous.EasyCsv.Tests.Model;

namespace Rhyous.EasyCsv.Tests.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void EnumerableExtensions_Basic_Tests()
        {
            // Arrange
            var items = new[] {
                new A { Id = 10, Name = "Awesome Name" },
                new A { Id = 11, Name = "Awesome Name, Inc" },
            };
            var expected = "Id,Name\r\n10,Awesome Name\r\n11,\"Awesome Name, Inc\"";

            // Act
            var actual = items.ToCsv();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnumerableExtensions_SlashNForNewLine_Tests()
        {
            // Arrange
            var items = new[] {
                new A { Id = 10, Name = "Awesome Name" },
                new A { Id = 11, Name = "Awesome Name, Inc" },
            };
            var expected = "Id,Name\n10,Awesome Name\n11,\"Awesome Name, Inc\"";

            // Act
            var actual = items.ToCsv(newLine: "\n");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnumerableExtensions_Basic_DelimnPipe_Tests()
        {
            // Arrange
            var items = new[] {
                new A { Id = 10, Name = "Awesome Name" },
                new A { Id = 11, Name = "Awesome Name, Inc" },
            };
            var expected = "Id|Name\r\n10|Awesome Name\r\n11|Awesome Name, Inc";

            // Act
            var actual = items.ToCsv("|");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnumerableExtensions_Comparer_Tests()
        {
            // Arrange
            var items = new[] {
                new UnorderedPropertyObject { Id = 10, Name = "Awesome Name", A = 10, B = 5, C = 7 },
                new UnorderedPropertyObject { Id = 11, Name = "Awesome Name, Inc", A = 11, B = 7, C = 3 },
            };
            var expected = "Id,Name,A,B,C\r\n10,Awesome Name,10,5,7\r\n11,\"Awesome Name, Inc\",11,7,3";
            var comparer = new HeaderPriorityComparer();

            // Act
            var actual = items.ToCsv(comparer: comparer);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
