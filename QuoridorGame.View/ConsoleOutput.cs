using QuoridorGame.Model.Events;
using System;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.View
{
    public class ConsoleOutput
    {
        public void ListenTo(Game game)
        {
            game.GameWon += OnPlayerWon;
            game.GameStarted += OnGameStarted;
            game.FieldUpdated += OnFieldUpdated;
            game.NewTurn += OnNewTurn;
        }

        private void OnNewTurn(object? sender, NextTurnEventArgs e)
        {
            var playerNumberWord = e.PlayerNumber == 1 ? "First" : "Second";
            Console.WriteLine($"{playerNumberWord} player's turn.");
            Console.WriteLine("Type 'move X Y' to move");
            Console.WriteLine("Type 'wall X Y' to place wall");
        }

        private void OnGameStarted(object? sender, GameStartedEventArgs e)
        {
            Console.WriteLine("Game has started!");
        }

        private void OnPlayerWon(object? sender, GameWonEventArgs e)
        {
            Console.WriteLine($"Player {e.PlayerNumber} has won!");
            Console.WriteLine("Type 'start' to start a new game.");
            Console.WriteLine("Type 'exit' to exit.");
        }

        public void OnFieldUpdated(object? sender, FieldUpdatedEventArgs e)
        {
            var action = e.Type == UpdateType.Move ? "moved to" : "placed wall at";
            Console.WriteLine($"Player {e.PlayerNumber} has {action} ({e.X}, {e.Y})");
            var game = sender as Game;
            var view = new GameViewModel(game);
            view.Print();
        }
    }
}
