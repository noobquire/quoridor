using NUnit.Framework;
using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Exceptions;
using QuoridorGame.Model.Logic;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Model.Tests
{
    [TestFixture]
    public class MovementLogicTests
    {
        private Game game;
        private MovementLogic movementLogic;

        [SetUp]
        public void Setup()
        {
            var game = new Game();
            game.Start();
            this.game = game;
            movementLogic = new MovementLogic(game);
        }

        [TestCase(0, 0)]
        [TestCase(0, 4)]
        [TestCase(4, 4)]
        public void GetAvailableMoves_FromUnblockedCell_ShouldReturnAdjacentCells(int cellX, int cellY)
        {
            var cell = game.GameField.Cells[cellX, cellY];

            var actualMoves = movementLogic.GetAvailableMoves(cell);

            CollectionAssert.AreEquivalent(cell.AdjacentNodes, actualMoves);
        }

        [Test]
        public void GetAvailableMoves_FromCellAboveEnemy_ShouldReturnExpectedCells()
        {
            var cell = game.GameField.Cells[7, 4];
            var expectedMoves = new[]
            {
                game.GameField.Cells[6, 4],
                game.GameField.Cells[7, 3],
                game.GameField.Cells[7, 5],
                game.GameField.Cells[8, 3],
                game.GameField.Cells[8, 5]
            };

            var actualMoves = movementLogic.GetAvailableMoves(cell);

            CollectionAssert.AreEquivalent(expectedMoves, actualMoves);
        }

        [Test]
        public void GetAvailableMoves_FromCellNearEnemy_ShouldReturnExpectedCells()
        {
            var cell = game.GameField.Cells[8, 3];
            var expectedMoves = new[]
            {
                game.GameField.Cells[8, 2],
                game.GameField.Cells[8, 5],
                game.GameField.Cells[7, 3]
            };

            var actualMoves = movementLogic.GetAvailableMoves(cell);

            CollectionAssert.AreEquivalent(expectedMoves, actualMoves);
        }

        [Test]
        public void MovePlayer_IllegalMove_ShouldThrowException()
        {
            var cell = game.GameField.Cells[8, 4];

            Assert.Throws<QuoridorGameException>(() => movementLogic.MovePlayer(game.FirstPlayer, cell));
        }

        [Test]
        public void MovePlayer_MoveToEnemyCell_ShouldThrowException()
        {
            var cell = game.GameField.Cells[7, 4];
            game.FirstPlayer.CurrentCell = cell;
            cell = game.GameField.Cells[8, 4];

            Assert.Throws<QuoridorGameException>(() => movementLogic.MovePlayer(game.FirstPlayer, cell));
        }

        [Test]
        public void MovePlayer_MoveToCell_ShouldChangeCurrentCell()
        {
            var cell = game.GameField.Cells[1, 4];

            movementLogic.MovePlayer(game.FirstPlayer, cell);

            Assert.AreEqual(cell, game.FirstPlayer.CurrentCell);
        }

        [Test]
        public void MovePlayer_MoveToCell_ShouldEndTurn()
        {
            var cell = game.GameField.Cells[1, 4];

            movementLogic.MovePlayer(game.FirstPlayer, cell);

            Assert.AreEqual(GameState.SecondPlayerTurn, game.State);
        }

        [Test]
        public void MovePlayer_GameNotStarted_ShouldThrowException()
        {
            game = new Game();
            movementLogic = new MovementLogic(game);
            var cell = game.GameField.Cells[1, 4];

            Assert.Throws<QuoridorGameException>(() => movementLogic.MovePlayer(game.FirstPlayer, cell));
        }

        [Test]
        public void MovePlayer_MoveToEnemySideCell_ShouldWinGame()
        {
            var cell = game.GameField.Cells[7, 4];
            game.FirstPlayer.CurrentCell = cell;
            cell = game.GameField.Cells[8, 5];

            movementLogic.MovePlayer(game.FirstPlayer, cell);

            Assert.AreEqual(GameState.FirstPlayerWin, game.State);
        }
    }
}
