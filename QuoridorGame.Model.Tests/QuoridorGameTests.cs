using NUnit.Framework;

namespace QuoridorGame.Model.Tests
{

    [TestFixture]
    public class QuoridorGameTests
    {
        private QuoridorGame game;

        [SetUp]
        public void Setup()
        {
            game = new QuoridorGame();
        }

        [Test]
        public void QuoridorGame_Players_ShouldStartAtMiddleOfTopAndBottomRows()
        {
            Assert.AreEqual(game.GameField.Cells[0, 4], game.FirstPlayer.CurrentCell);
            Assert.AreEqual(game.GameField.Cells[8, 4], game.SecondPlayer.CurrentCell);
        }
    }
}