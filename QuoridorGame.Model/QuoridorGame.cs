using System;

namespace QuoridorGame.Model
{
    public class QuoridorGame
    {
        public GameField GameField { get; }
        public Player FirstPlayer { get; }
        public Player SecondPlayer { get; }

        public QuoridorGame()
        {
            GameField = new GameField();
            FirstPlayer = new Player(GameField.Nodes[0, 4]);
            SecondPlayer = new Player(GameField.Nodes[8, 4]);
        }
    }
}
