using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.EasyCsv.Tests
{
    [TestClass]
    public class ToDataTableTests
    {
        [TestMethod]
        public void DataTableNoHeaderTest()
        {
            // Arrange
            var csv = new Csv(@"Data\ComplexNoHeader.csv", false);

            // Act
            var table = csv.ToDataTable();

            // Assert
            Assert.AreEqual(csv.Rows[0].Count, table.Columns.Count);
            Assert.AreEqual(csv.Rows.Count, table.Rows.Count);
            var i = 0;
            foreach (var row in csv.Rows)
            {
                CollectionAssert.Equals(csv.Rows, table.Rows[i]);
            }
        }
    }
}
