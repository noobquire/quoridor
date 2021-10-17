using QuoridorGame.Model.Entities;
using System;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.View
{
    public class ConsoleOutput
    {
        private Game game;

        public void ListenTo(Game game)
        {
            this.game = game;
            game.PlayerWon += OnPlayerWon;
            game.GameStarted += OnFieldUpdated;
        }

        private void OnPlayerWon(Player player)
        {
            Console.WriteLine($"Game is over! Player {player} has won!");
        }

        public void OnFieldUpdated(GameField gameField)
        {
            var cellGrid = gameField.Cells;
            var wallGrid = gameField.Walls;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"{cellGrid[i, j]}");
                }
                if (i < 6)
                {
                    for (int z = 0; z < 7; z++)
                    {
                        Console.Write($"{wallGrid[i, z]}");
                    }
                }
            }
        }
    }
}
