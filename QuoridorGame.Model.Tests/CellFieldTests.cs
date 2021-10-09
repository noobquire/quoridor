using NUnit.Framework;
using QuoridorGame.Model.Entities;
using System.Linq;

namespace QuoridorGame.Model.Tests
{
    public class CellFieldTests
    {
        private CellField cellField;

        [SetUp]
        public void Setup()
        {
            cellField = new CellField();
        }

        [TestCase(0, 0)]
        [TestCase(0, 8)]
        [TestCase(8, 0)]
        [TestCase(8, 8)]
        public void CellField_CornerCells_ShouldHaveTwoAdjacentCells(int i, int j)
        {
            Assert.AreEqual(2, cellField.Nodes[i, j].AdjacentNodes.Count());
        }

        [TestCase(1, 0)]
        [TestCase(5, 8)]
        [TestCase(8, 3)]
        public void CellField_BorderCells_ShouldHaveThreeAdjacentCells(int i, int j)
        {
            Assert.AreEqual(3, cellField.Nodes[i, j].AdjacentNodes.Count());
        }

        [TestCase(1, 1)]
        [TestCase(3, 5)]
        [TestCase(3, 3)]
        [TestCase(5, 6)]
        public void CellField_BorderCells_ShouldHaveFourAdjacentCells(int i, int j)
        {
            Assert.AreEqual(4, cellField.Nodes[i, j].AdjacentNodes.Count());
        }

        [Test]
        public void Save_ReturnsDeepCopyOfCells()
        {
            var savedState = cellField.Save();
            Assert.That(savedState.Data[0, 0], Is.Not.SameAs(cellField.Nodes[0, 0]));
        }

        [Test]
        public void Restore_RestoresCellFieldState()
        {
            var savedState = cellField.Save();
            cellField.Nodes[0, 0].AdjacentNodes = Enumerable.Empty<Cell>(); // change state after save
            cellField.Restore(savedState);
            Assert.AreEqual(2, cellField.Nodes[0, 0].AdjacentNodes.Count());
        }
    }
}
