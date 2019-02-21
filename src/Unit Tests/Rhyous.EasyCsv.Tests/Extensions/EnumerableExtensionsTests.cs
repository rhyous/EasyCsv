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
    }
}
