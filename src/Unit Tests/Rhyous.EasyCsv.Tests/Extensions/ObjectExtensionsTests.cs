using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.EasyCsv.Extensions;
using Rhyous.EasyCsv.Tests.Model;

namespace Rhyous.EasyCsv.Tests.Extensions
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void ObjectExtensions_ToCsvRow_NullObject_Test()
        {
            // Arrange
            A a = null;
            var expected = ",";
            var headers = typeof(A).GetHeaders();

            // Act
            var actual = a.ToCsvRow(headers);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ObjectExtensions_ToCsvRow_NullValue_Test()
        {
            // Arrange
            A a = new A { Id = 10 };
            var expected = "10,";
            var headers = typeof(A).GetHeaders();

            // Act
            var actual = a.ToCsvRow(headers);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ObjectExtensions_ToCsvRow_Basic_Test()
        {
            // Arrange
            var a = new A { Id = 10, Name = "Awesome Name" };
            var expected = "10,Awesome Name";
            var headers = typeof(A).GetHeaders();

            // Act
            var actual = a.ToCsvRow(headers);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ObjectExtensions_ToCsvRow_HasDelimn_Test()
        {
            // Arrange
            var a = new A { Id = 10, Name = "Awesome Name, Inc" };
            var expected = "10,\"Awesome Name, Inc\"";
            var headers = typeof(A).GetHeaders();

            // Act
            var actual = a.ToCsvRow(headers);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
