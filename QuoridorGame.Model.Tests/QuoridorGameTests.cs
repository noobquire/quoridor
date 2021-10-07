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
            Assert.AreEqual(game.GameField.Nodes[0, 4], game.FirstPlayer.CurrentCell);
            Assert.AreEqual(game.GameField.Nodes[8, 4], game.SecondPlayer.CurrentCell);
        }
    }
}