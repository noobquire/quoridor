using QuoridorGame.View.Bot.Interfaces;
using QuoridorGame.Model.Entities;
using Game = QuoridorGame.Model.Entities.QuoridorGame;
using System;
using System.Linq;

namespace QuoridorGame.View.Bot.Logic
{
    class LogSEV : IStaticEvaluationFunction
    {
        public double Eval(Game game)
        {
            int alpha = 9;
            int betta = 7;
            double gamma = (alpha / betta) * 1.1;

            //game.pathFinder
            var playerCell = game.CurrentPlayer.CurrentCell;
            var enemyCell = game.OpponentPlayer.CurrentCell;
            int playerRow = (game.CurrentPlayer == game.FirstPlayer) ? 8 : 0;
            int enemyRow = (game.CurrentPlayer == game.FirstPlayer) ? 0 : 8;

            var playerLengths = new int[9];
            var enemyLengths = new int[9];
            for (int i = 0; i <= 8; i++)
            {
                playerLengths[i] = game.pathFinder.ShortestPathLength(playerCell, game.GameField.Cells[playerRow, i]);
                enemyLengths[i] = game.pathFinder.ShortestPathLength(enemyCell, game.GameField.Cells[enemyRow, i]);
            }
            int playerPath = playerLengths.Min();
            int enemyPath = enemyLengths.Min();

            if (playerPath == 0)
            {
                return double.NegativeInfinity;
            }

            return -alpha * Math.Log2(1 + playerPath) +
                betta * Math.Log2(1 + enemyPath) +
                gamma * game.CurrentPlayer.WallsCount;

        }
    }
}
