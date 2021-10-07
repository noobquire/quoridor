using NUnit.Framework;
using QuoridorGame.Model.Entities;
using System.Linq;

namespace QuoridorGame.Model.Tests
{
    public class GameFieldTests
    {
        private GameField field;

        [SetUp]
        public void Setup()
        {
            field = new GameField();
        }

        [TestCase(0, 0)]
        [TestCase(0, 8)]
        [TestCase(8, 0)]
        [TestCase(8, 8)]
        public void GameField_CornerCells_ShouldHaveTwoAdjacentCells(int i, int j)
        {
            Assert.AreEqual(2, field.Nodes[i, j].AdjacentNodes.Count());
        }

        [TestCase(1, 0)]
        [TestCase(5, 8)]
        [TestCase(8, 3)]
        public void GameField_BorderCells_ShouldHaveThreeAdjacentCells(int i, int j)
        {
            Assert.AreEqual(3, field.Nodes[i, j].AdjacentNodes.Count());
        }

        [TestCase(1, 1)]
        [TestCase(3, 5)]
        [TestCase(3, 3)]
        [TestCase(5, 6)]
        public void GameField_BorderCells_ShouldHaveFourAdjacentCells(int i, int j)
        {
            Assert.AreEqual(4, field.Nodes[i, j].AdjacentNodes.Count());
        }
    }
}