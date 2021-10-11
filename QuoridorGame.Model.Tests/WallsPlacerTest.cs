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
        private Game game;

        [SetUp]
        public void Setup()
        {
            game = new Game();
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

        [TestCase]
        public void WallIsPlaceable_PathExists1()
        {
            wallPlacer.PlaceWall(WallType.Horizontal, 0, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 1, 0);
            wallPlacer.PlaceWall(WallType.Horizontal, 2, 0);

            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(WallType.Vertical, 1, 1));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void WallIsNotPlaceable_PathDoesntExists1_Player1(int x, int y)
        {
            game.FirstPlayer.CurrentCell = game.GameField.Cells[x, y];

            wallPlacer.PlaceWall(WallType.Horizontal, 0, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 1, 0);
            wallPlacer.PlaceWall(WallType.Horizontal, 2, 0);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Vertical, 1, 1)
            );
            Assert.That(ex.Message, Is.EqualTo("Can't block player with a wall."));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void WallIsNotPlaceable_PathDoesntExists1_Player2(int x, int y)
        {
            game.SecondPlayer.CurrentCell = game.GameField.Cells[x, y];

            wallPlacer.PlaceWall(WallType.Horizontal, 0, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 1, 0);
            wallPlacer.PlaceWall(WallType.Horizontal, 2, 0);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Vertical, 1, 1)
            );
            Assert.That(ex.Message, Is.EqualTo("Can't block player with a wall."));
        }

        [TestCase]
        public void WallIsPlaceable_PathExists2()
        {
            wallPlacer.PlaceWall(WallType.Horizontal, 0, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 1, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 1, 1);
            wallPlacer.PlaceWall(WallType.Vertical, 3, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 3, 1);
            wallPlacer.PlaceWall(WallType.Vertical, 5, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 5, 1);
            wallPlacer.PlaceWall(WallType.Vertical, 7, 0);
            
            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(WallType.Vertical, 7, 1));
        }

        [TestCase(7, 1)]
        [TestCase(6, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 1)]
        public void WallIsNotPlaceable_PathDoesntExists2_Player1(int x, int y)
        {
            game.FirstPlayer.CurrentCell = game.GameField.Cells[x, y];
            wallPlacer.PlaceWall(WallType.Horizontal, 0, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 1, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 1, 1);
            wallPlacer.PlaceWall(WallType.Vertical, 3, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 3, 1);
            wallPlacer.PlaceWall(WallType.Vertical, 5, 0);
            wallPlacer.PlaceWall(WallType.Vertical, 5, 1);
            wallPlacer.PlaceWall(WallType.Vertical, 7, 0);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Vertical, 7, 1)
            );
            Assert.That(ex.Message, Is.EqualTo("Can't block player with a wall."));
        }

        [TestCase(0, 0)]
        public void WallIsNotPlaceable_PathDoesntExists2_Player2(int x, int y)
        {
            game.SecondPlayer.CurrentCell = game.GameField.Cells[x, y];

            wallPlacer.PlaceWall(WallType.Horizontal, 0, 0);
            wallPlacer.PlaceWall(WallType.Horizontal, 0, 2);
            wallPlacer.PlaceWall(WallType.Horizontal, 0, 4);
            wallPlacer.PlaceWall(WallType.Horizontal, 0, 6);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(WallType.Vertical, 0, 7)
            );
            Assert.That(ex.Message, Is.EqualTo("Can't block player with a wall."));
        }
    }
}