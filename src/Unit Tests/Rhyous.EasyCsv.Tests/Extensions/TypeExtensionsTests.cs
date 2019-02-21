using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.EasyCsv.Extensions;
using Rhyous.EasyCsv.Tests.Model;

namespace Rhyous.EasyCsv.Tests.Extensions
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void TypeExtensions_GetHeaders_Test()
        {
            // Arrange
            var expected = new[] { "Id", "Name" };

            // Act
            var actual = typeof(A).GetHeaders();

            // Assert
            CollectionAssert.AreEqual(expected, actual.ToArray());
        }

        [TestMethod]
        public void TypeExtensions_GetHeaders_Comparer_Test()
        {
            // Arrange
            var expected = new[] { "Id", "Name", "A", "B", "C" };
            var comparer = new HeaderPriorityComparer();

            // Act
            var actual = typeof(UnorderedPropertyObject).GetHeaders(comparer);

            // Assert
            CollectionAssert.AreEqual(expected, actual.ToArray());
        }
    }
}
