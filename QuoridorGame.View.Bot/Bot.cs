using Game = QuoridorGame.Model.Entities.QuoridorGame;
using System;

namespace QuoridorGame.View.Bot
{
    public class Bot : IBot
    {
        public int PlayerNumber { get; private set; }

        public void ChoosePlayer(int playerNumber)
        {
            this.PlayerNumber = playerNumber;
        }

        public void MakeTurn(Game game)
        {
            // TODO: Process game state and make turn
            throw new NotImplementedException();
        }
    }
}
