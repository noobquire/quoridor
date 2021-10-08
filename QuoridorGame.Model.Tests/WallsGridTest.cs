using NUnit.Framework;
using QuoridorGame.Model.Entities;
using System.Linq;

namespace QuoridorGame.Model.Tests
{
    public class WallsGridTests
    {
        private WallsGrid grid;

        [SetUp]
        public void Setup()
        {
            grid = new WallsGrid();
        }

        [TestCase(0, 8)]
        [TestCase(8, 0)]
        [TestCase(8, 8)]
        [TestCase(8, 3)]
        [TestCase(3, 8)]
        public void WallIsNotPlaceable_IndexOutOfBounds(int x, int y)
        {
            (bool status, string msg) = grid.WallIsPlaceable(WallType.None, x, y);
            Assert.IsFalse(status);
            Assert.AreEqual(msg, "WallsGrid index is out of bounds.");
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(7, 7)]
        public void WallIsNotPlaceable_PositionAlreadyTaken(int x, int y)
        {
            grid.Grid[x][y] = 1;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.None, x, y);
            Assert.IsFalse(status);
            Assert.AreEqual(msg, "Position already taken by other wall.");
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(7, 7)]
        public void WallIsNotPlaceable_NoneType(int x, int y)
        {
            (bool status, string msg) = grid.WallIsPlaceable(WallType.None, x, y);
            Assert.IsFalse(status);
            Assert.AreEqual(msg, "WallType can not be WallType:None.");
        }

        [TestCase(1, 0)]
        [TestCase(7, 0)]
        [TestCase(7, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_UpperVerticalBlock(int x, int y)
        {
            grid.Grid[x-1][y] = 1;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.Vertical, x, y);
            Assert.IsFalse(status);
            Assert.AreEqual(msg, "Position is blocked by another vertical wall.");
        }

        [TestCase(1, 0)]
        [TestCase(6, 0)]
        [TestCase(6, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_LowerVerticalBlock(int x, int y)
        {
            grid.Grid[x + 1][y] = 1;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.Vertical, x, y);
            Assert.IsFalse(status);
            Assert.AreEqual(msg, "Position is blocked by another vertical wall.");
        }

        [TestCase(0, 1)]
        [TestCase(7, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_LeftHorizontalBlock(int x, int y)
        {
            grid.Grid[x][y-1] = 2;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.Horizontal, x, y);
            Assert.IsFalse(status);
            Assert.AreEqual(msg, "Position is blocked by another horizontal wall.");
        }

        [TestCase(0, 0)]
        [TestCase(6, 6)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_RigtHorizontalBlock(int x, int y)
        {
            grid.Grid[x][y + 1] = 2;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.Horizontal, x, y);
            Assert.IsFalse(status);
            Assert.AreEqual(msg, "Position is blocked by another horizontal wall.");
        }

        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 0, 1, 1)]
        public void WallIsPlaceable_Corner(int x1, int y1, int x2, int y2)
        {
            grid.Grid[x1][y1] = 2;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.Vertical, x2, y2);
            Assert.IsTrue(status);
            Assert.AreEqual(msg, "");
        }

        [TestCase(0, 0, 2, 0)]
        [TestCase(7, 0, 5, 0)]
        [TestCase(0, 0, 7, 7)]
        [TestCase(0, 0, 0, 7)]
        public void WallIsPlaceable_Vertical(int x1, int y1, int x2, int y2)
        {
            grid.Grid[x1][y1] = 1;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.Vertical, x2, y2);
            Assert.IsTrue(status);
            Assert.AreEqual(msg, "");
        }

        [TestCase(0, 0, 0, 2)]
        [TestCase(0, 7, 0, 5)]
        [TestCase(0, 0, 7, 7)]
        [TestCase(0, 0, 7, 0)]
        public void WallIsPlaceable_Horizontal(int x1, int y1, int x2, int y2)
        {
            grid.Grid[x1][y1] = 2;
            (bool status, string msg) = grid.WallIsPlaceable(WallType.Horizontal, x2, y2);
            Assert.IsTrue(status);
            Assert.AreEqual(msg, "");
        }
    }
}