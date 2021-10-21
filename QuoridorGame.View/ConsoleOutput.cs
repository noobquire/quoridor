using QuoridorGame.Model.Entities;
using System;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.View
{
    public class ConsoleOutput
    {
        public void ListenTo(Game game)
        {
            game.PlayerWon += OnPlayerWon;
            game.GameStarted += OnFieldUpdated;
            game.FieldUpdated += OnFieldUpdated;
        }

        private void OnPlayerWon(Player player)
        {
            Console.WriteLine($"Game is over! Player {player} has won!");
        }

        public void OnFieldUpdated(Game game)
        {
            var view = new GameViewModel(game);
            view.Print();
        }
    }
}
