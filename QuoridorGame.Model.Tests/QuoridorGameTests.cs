using NUnit.Framework;
using QuoridorGame.Model.Entities;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Model.Tests
{

    [TestFixture]
    public class QuoridorGameTests
    {
        private Game game;

        [SetUp]
        public void Setup()
        {
            game = new Game();
        }

        [Test]
        public void QuoridorGame_Players_ShouldStartAtMiddleOfTopAndBottomRows()
        {
            Assert.AreEqual(game.GameField.Cells[0, 4], game.FirstPlayer.CurrentCell);
            Assert.AreEqual(game.GameField.Cells[8, 4], game.SecondPlayer.CurrentCell);
        }

        [Test]
        public void Start_ChangesGameState_ToSecondPlayerTurn()
        {
            Assert.AreEqual(GameState.Pregame, game.State);
            game.Start();
            Assert.AreEqual(GameState.SecondPlayerTurn, game.State);
        }

        [Test]
        public void NextTurn_PassesTurnToAnotherPlayer()
        {
            game.Start();
            Assert.AreEqual(GameState.SecondPlayerTurn, game.State);
            game.NextTurn();
            Assert.AreEqual(GameState.FirstPlayerTurn, game.State);
        }

        [Test]
        public void Win_ChangesStateToAppropriatePlayerWin()
        {
            game.Start();
            game.Win(game.SecondPlayer);
            Assert.AreEqual(GameState.SecondPlayerWin, game.State);
        }

        [Test]
        public void CurrentPlayer_ReturnsNull_IfNotPlayersTurn()
        {
            var currentPlayer = game.CurrentPlayer;
            Assert.IsNull(currentPlayer);
        }

        [Test]
        public void CurrentPlayer_ShouldBeAppropriatePlayer_IfPlayersTurn()
        {
            game.Start();
            var currentPlayer = game.CurrentPlayer;
            var expectedPlayer = game.SecondPlayer;
            Assert.AreEqual(expectedPlayer, currentPlayer);
        }

        [Test]
        public void OpponentPlayer_ReturnsNull_IfNotPlayersTurn()
        {
            var opponentPlayer = game.OpponentPlayer;
            Assert.IsNull(opponentPlayer);
        }

        [Test]
        public void OpponentPlayer_ShouldBeAppropriatePlayer_IfPlayersTurn()
        {
            game.Start();
            var opponentPlayer = game.OpponentPlayer;
            var expectedPlayer = game.FirstPlayer;
            Assert.AreEqual(expectedPlayer, opponentPlayer);
        }
    }
}
