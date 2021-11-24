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
            game.WallPlaced += OnWallPlaced;
            game.PlayerMoved += OnPlayerMoved;
            game.NewTurn += OnNewTurn;
        }

        private void OnWallPlaced(object sender, WallPlacedEventArgs e)
        {
            // Do not handle if it was other player's turn
            if (e.PlayerNumber == bot.PlayerNumber)
            {
                return;
            }

            var x = (e.X + 1).ToString();
            var y = (char)(e.Y + 'S');
            var type = e.WallType == Model.Entities.WallType.Vertical ? 'v' : 'h';

            var message = $"wall {y}{x}{type}";

            Console.WriteLine(message);
        }

        private void OnPlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            // Do not handle if it was other player's turn
            if (e.PlayerNumber != bot.PlayerNumber)
            {
                return;
            }

            var x = (e.X + 1).ToString();
            var y = (char)(e.Y + 'A');

            // TODO: decide move or jump
            var moveOrJump = e.Jump ? "jump" : "move";
            var message = $"{moveOrJump} {y}{x}";

            Console.WriteLine(message);
        }

        private void OnNewTurn(object sender, NextTurnEventArgs e)
        {
            // Do not handle if it is not our turn
            if(e.PlayerNumber != bot.PlayerNumber)
            {
                return;
            }

            bot.MakeTurn(game);
        }
    }
}
