using QuoridorGame.Model.Events;
using System;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.View.Bot
{
    public class BotOutput
    {
        private readonly IBot bot;
        private readonly Game game;

        public BotOutput(IBot bot, Game game)
        {
            this.bot = bot;
            this.game = game;
        }

        public void ListenTo(Game game)
        {
            /* These handlers were used mainly for graphics
            game.GameWon += OnPlayerWon;
            game.GameStarted += OnGameStarted;
            game.WallPlaced += OnWallPlaced;
            */

            game.PlayerMoved += OnPlayerMoved;
            game.NewTurn += OnNewTurn;
        }

        private void OnPlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            // Do not handle if it was other player's turn
            if (e.PlayerNumber != bot.PlayerNumber)
            {
                return;
            }
            // TODO: Log own turns to console
            throw new NotImplementedException();
        }

        private void OnNewTurn(object sender, NextTurnEventArgs e)
        {
            // Do not handle if it is not our turn
            if(e.PlayerNumber != bot.PlayerNumber)
            {
                return;
            }
            // TODO: Process game state and make turn
            throw new NotImplementedException();
        }
    }
}
