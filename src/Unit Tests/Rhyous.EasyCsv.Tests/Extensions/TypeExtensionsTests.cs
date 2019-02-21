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
    }
}
