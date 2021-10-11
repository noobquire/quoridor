using NUnit.Framework;
using System.Linq;
using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Exceptions;
using QuoridorGame.Model.Logic;
using Game = QuoridorGame.Model.Entities.QuoridorGame;


namespace QuoridorGame.Model.Tests
{
    public class WallsGridTests
    {
        private WallsGrid grid;
        private WallPlacer wallPlacer;

        [SetUp]
        public void Setup()
        {
            Game game = new Game();
            wallPlacer = new WallPlacer(game, new PathFinder<CellField, Cell>(game.GameField.Cells));
            grid = game.GameField.Walls;
        }

        
        [TestCase(0, 8)]
        [TestCase(8, 0)]
        [TestCase(8, 8)]
        [TestCase(8, 3)]
        [TestCase(3, 8)]
        public void WallIsNotPlaceable_IndexOutOfBounds(int x, int y)
        {
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.None, x, y)
            );
            Assert.That(ex.Message, Is.EqualTo("WallsGrid index is out of bounds."));
            
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(7, 7)]
        public void WallIsNotPlaceable_PositionAlreadyTaken(int x, int y)
        {
            wallPlacer.PlaceWall(WallType.Vertical, x, y);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.None, x, y)
            );
            Assert.That(ex.Message, Is.EqualTo("Position already taken by other wall."));
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(7, 7)]
        public void WallIsNotPlaceable_NoneType(int x, int y)
        {
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.None, x, y)
            );
            Assert.That(ex.Message, Is.EqualTo("WallType can not be WallType:None."));
        }

        [TestCase(1, 0)]
        [TestCase(7, 0)]
        [TestCase(7, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_UpperVerticalBlock(int x, int y)
        {
            wallPlacer.PlaceWall(WallType.Vertical, x - 1, y);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Vertical, x, y)
            );
            Assert.That(ex.Message, Is.EqualTo("Position is blocked by another vertical wall."));
        }

        [TestCase(1, 0)]
        [TestCase(6, 0)]
        [TestCase(6, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_LowerVerticalBlock(int x, int y)
        {
            wallPlacer.PlaceWall(WallType.Vertical, x+1, y);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Vertical, x, y)
            );
            Assert.That(ex.Message, Is.EqualTo("Position is blocked by another vertical wall."));
        }

        [TestCase(0, 1)]
        [TestCase(7, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_LeftHorizontalBlock(int x, int y)
        {
            wallPlacer.PlaceWall(WallType.Horizontal, x, y -1);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Horizontal, x, y)
            );
            Assert.That(ex.Message, Is.EqualTo("Position is blocked by another horizontal wall."));
        }

        [TestCase(0, 0)]
        [TestCase(6, 6)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_RigtHorizontalBlock(int x, int y)
        {
            wallPlacer.PlaceWall(WallType.Horizontal, x, y+1);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Horizontal, x, y)
            );
            Assert.That(ex.Message, Is.EqualTo("Position is blocked by another horizontal wall."));
        }

        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 0, 1, 1)]
        public void WallIsPlaceable_Corner(int x1, int y1, int x2, int y2)
        {
            wallPlacer.PlaceWall(WallType.Horizontal, x1, y1);
            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(WallType.Vertical, x2, y2));
        }

        [TestCase(0, 0, 2, 0)]
        [TestCase(7, 0, 5, 0)]
        [TestCase(0, 0, 7, 7)]
        [TestCase(0, 0, 0, 7)]
        public void WallIsPlaceable_Vertical(int x1, int y1, int x2, int y2)
        {
            wallPlacer.PlaceWall(WallType.Vertical, x1, y1);
            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(WallType.Vertical, x2, y2));
        }

        [TestCase(0, 0, 0, 2)]
        [TestCase(0, 7, 0, 5)]
        [TestCase(0, 0, 7, 7)]
        [TestCase(0, 0, 7, 0)]
        public void WallIsPlaceable_Horizontal(int x1, int y1, int x2, int y2)
        {
            wallPlacer.PlaceWall(WallType.Horizontal, x1, y1);
            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(WallType.Horizontal, x2, y2));
        }
    }
}