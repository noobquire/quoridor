using Game = QuoridorGame.Model.Entities.QuoridorGame;
using QuoridorGame.View.Bot.Logic;
using QuoridorGame.View.Bot;
using System;
using QuoridorGame.Model;
using System.Diagnostics;

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
            var sw = new Stopwatch();
            sw.Start();
            //var gameCopy = game.DeepClone();
            var SEV = new LogSEV();
            game.IsBotTurn = true;
            var tree = new StateNode(game, SEV);
            tree.Rollout(1);
            game.IsBotTurn = false;
            tree.MakeBestMove(false);
            sw.Stop();
            Debug.WriteLine($"bot turn took {sw.ElapsedMilliseconds/1000}s");
        }
    }
}
