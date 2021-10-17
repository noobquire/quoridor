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
            game.FieldUpdated += OnFieldUpdated;
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
                    Console.Write($"{c(cellGrid[i, j])}");
                }
                Console.WriteLine();
                if (i < 8)
                {
                    for (int z = 0; z < 8; z++)
                    {
                        Console.Write($"{w(wallGrid[i, z])}");
                    }
                }
                Console.WriteLine();
            }

            string c(Cell cell)
            {
                var firstPlayerPosition = game.FirstPlayer.CurrentCell;
                var secondPlayerPosition = game.SecondPlayer.CurrentCell;
                return (cell.X == firstPlayerPosition.X && cell.Y == firstPlayerPosition.Y) || (cell.X == secondPlayerPosition.X && cell.Y == secondPlayerPosition.Y)
                    ? "P" : ".";
            }

            string w(Wall wall)
            {
                return wall.Type switch
                {
                    WallType.None => " ",
                    WallType.Horizontal => "-",
                    WallType.Vertical => "|"
                };
            }
        }
    }
}
