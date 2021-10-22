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
            game.PlayerMoved += OnPlayerMoved;
            game.WallPlaced += OnWallPlaced;
            game.NewTurn += OnNewTurn;
        }

        private void OnWallPlaced(object? sender, WallPlacedEventArgs e)
        {
            Console.WriteLine($"Player {e.PlayerNumber} has placed {e.WallType.ToString().ToLowerInvariant()} wall ({e.X}, {e.Y})");
            Console.WriteLine($"Player {e.PlayerNumber} has {e.WallsLeft} walls remaining.");
            var game = sender as Game;
            UpdateView(game);
        }

        private void OnNewTurn(object? sender, NextTurnEventArgs e)
        {
            var playerNumberWord = e.PlayerNumber == 1 ? "First" : "Second";
            Console.WriteLine($"{playerNumberWord} player's turn.");
            Console.WriteLine("Type 'move X Y' to move");
            Console.WriteLine("Type 'wall X Y wallType' to place wall (walltype 1 for vertical, 2 for horizontal)");
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

        private void UpdateView(Game game)
        {
            var view = new GameViewModel(game);
            view.Print();
        }

        private void OnPlayerMoved(object? sender, PlayerMovedEventArgs e)
        {
            Console.WriteLine($"Player {e.PlayerNumber} has moved to ({e.X}, {e.Y})");
            var game = sender as Game;
            UpdateView(game);
        }
    }
}
