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

        public void MakeMove(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
