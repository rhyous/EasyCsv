using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.EasyCsv.Tests.Model
{
    [TestClass]
    public class RowCollectionTests
    {
        [TestMethod]
        public void RowCollection_Add_Test()
        {
            // Arrange
            var rowCollection = new RowCollection<string>();

            // Act

            rowCollection.Add(
                new List<string> {
                "Row1Cell1",
                "Row1Cell2"
                });

            // Assert
            Assert.AreEqual(1, rowCollection.Count);
        }
    }
}
