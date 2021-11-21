using NUnit.Framework;
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
            game.Start();
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(7, 7)]
        public void WallIsNotPlaceable_PositionAlreadyTaken(int x, int y)
        {
            var wall = game.GameField.Walls[x, y];
            wallPlacer.PlaceWall(wall, WallType.Vertical);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(wall, WallType.None)
            );
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(7, 7)]
        public void WallIsNotPlaceable_NoneType(int x, int y)
        {
            var wall = game.GameField.Walls[x, y];
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(wall, WallType.None)
            );
        }

        [TestCase(1, 0)]
        [TestCase(7, 0)]
        [TestCase(7, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_UpperVerticalBlock(int x, int y)
        {
            var wall = game.GameField.Walls[x, y];
            var wallAbove = game.GameField.Walls[x - 1, y];
            wallPlacer.PlaceWall(wall, WallType.Vertical);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(wall, WallType.Vertical)
            );
        }

        [TestCase(1, 0)]
        [TestCase(6, 0)]
        [TestCase(6, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_LowerVerticalBlock(int x, int y)
        {
            var wall = game.GameField.Walls[x, y];
            var wallBelow = game.GameField.Walls[x + 1, y];
            wallPlacer.PlaceWall(wallBelow, WallType.Vertical);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(wall, WallType.Vertical)
            );
        }

        [TestCase(0, 1)]
        [TestCase(7, 7)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_LeftHorizontalBlock(int x, int y)
        {
            var wall = game.GameField.Walls[x, y];
            var wallLeft = game.GameField.Walls[x, y - 1];
            wallPlacer.PlaceWall(wallLeft, WallType.Horizontal);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(wall, WallType.Horizontal)
            );
        }

        [TestCase(0, 0)]
        [TestCase(6, 6)]
        [TestCase(3, 3)]
        public void WallIsNotPlaceable_RigtHorizontalBlock(int x, int y)
        {
            var wall = game.GameField.Walls[x, y];
            var wallRight = game.GameField.Walls[x, y + 1];
            wallPlacer.PlaceWall(wallRight, WallType.Horizontal);
            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(wall, WallType.Horizontal)
            );
        }

        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 0, 1, 1)]
        public void WallIsPlaceable_Corner(int x1, int y1, int x2, int y2)
        {
            var wall1 = game.GameField.Walls[x1, y1];
            var wall2 = game.GameField.Walls[x2, y2];
            wallPlacer.PlaceWall(wall1, WallType.Horizontal);
            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(wall2, WallType.Vertical));
        }

        [TestCase(0, 0, 2, 0)]
        [TestCase(7, 0, 5, 0)]
        [TestCase(0, 0, 7, 7)]
        [TestCase(0, 0, 0, 7)]
        public void WallIsPlaceable_Vertical(int x1, int y1, int x2, int y2)
        {
            var wall1 = game.GameField.Walls[x1, y1];
            var wall2 = game.GameField.Walls[x2, y2];
            wallPlacer.PlaceWall(wall1, WallType.Vertical);
            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(wall2, WallType.Vertical));
        }

        [TestCase(0, 0, 0, 2)]
        [TestCase(0, 7, 0, 5)]
        [TestCase(0, 0, 7, 7)]
        [TestCase(0, 0, 7, 0)]
        public void WallIsPlaceable_Horizontal(int x1, int y1, int x2, int y2)
        {
            var wall1 = game.GameField.Walls[x1, y1];
            var wall2 = game.GameField.Walls[x2, y2];
            wallPlacer.PlaceWall(wall1, WallType.Horizontal);
            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(wall2, WallType.Horizontal));
        }

        [TestCase]
        public void WallIsPlaceable_PathExists1()
        {
            wallPlacer.PlaceWall(game.GameField.Walls[0, 0], WallType.Horizontal);
            wallPlacer.PlaceWall(game.GameField.Walls[1, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[2, 0], WallType.Horizontal);

            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(game.GameField.Walls[1, 1], WallType.Vertical));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void WallIsNotPlaceable_PathDoesntExists1_Player1(int x, int y)
        {
            game.FirstPlayer.CurrentCell = game.GameField.Cells[x, y];

            wallPlacer.PlaceWall(game.GameField.Walls[0, 0], WallType.Horizontal);
            wallPlacer.PlaceWall(game.GameField.Walls[1, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[2, 0], WallType.Horizontal);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(game.GameField.Walls[1, 1], WallType.Vertical)
            );
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void WallIsNotPlaceable_PathDoesntExists1_Player2(int x, int y)
        {
            game.SecondPlayer.CurrentCell = game.GameField.Cells[x, y];

            wallPlacer.PlaceWall(game.GameField.Walls[0, 0], WallType.Horizontal);
            wallPlacer.PlaceWall(game.GameField.Walls[1, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[2, 0], WallType.Horizontal);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(game.GameField.Walls[1, 1], WallType.Vertical)
            );
        }

        [TestCase]
        public void WallIsPlaceable_PathExists2()
        {
            wallPlacer.PlaceWall(game.GameField.Walls[0, 0], WallType.Horizontal);
            wallPlacer.PlaceWall(game.GameField.Walls[1, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[1, 1], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[3, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[3, 1], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[5, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[5, 1], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[7, 0], WallType.Vertical);

            Assert.DoesNotThrow(() => wallPlacer.PlaceWall(game.GameField.Walls[7, 1], WallType.Vertical));
        }

        [TestCase(7, 1)]
        [TestCase(6, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 1)]
        public void WallIsNotPlaceable_PathDoesntExists2_Player1(int x, int y)
        {
            game.FirstPlayer.CurrentCell = game.GameField.Cells[x, y];
            wallPlacer.PlaceWall(game.GameField.Walls[0, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[0, 1], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[2, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[2, 1], WallType.Vertical);

            wallPlacer.PlaceWall(game.GameField.Walls[4, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[4, 1], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[6, 0], WallType.Vertical);
            wallPlacer.PlaceWall(game.GameField.Walls[6, 1], WallType.Vertical);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(game.GameField.Walls[7, 0], WallType.Horizontal)
            );
        }

        [TestCase(0, 0)]
        public void WallIsNotPlaceable_PathDoesntExists2_Player2(int x, int y)
        {
            game.SecondPlayer.CurrentCell = game.GameField.Cells[x, y];

            wallPlacer.PlaceWall(game.GameField.Walls[0, 0], WallType.Horizontal);
            wallPlacer.PlaceWall(game.GameField.Walls[0, 2], WallType.Horizontal);
            wallPlacer.PlaceWall(game.GameField.Walls[0, 4], WallType.Horizontal);
            wallPlacer.PlaceWall(game.GameField.Walls[0, 6], WallType.Horizontal);

            QuoridorGameException ex = Assert.Throws<QuoridorGameException>(
                () => wallPlacer.PlaceWall(game.GameField.Walls[0, 7], WallType.Vertical)
            );
        }
    }
}