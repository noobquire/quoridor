using QuoridorGame.View.Bot.Interfaces;
using QuoridorGame.Model.Entities;
using Game = QuoridorGame.Model.Entities.QuoridorGame;
using System; 

namespace QuoridorGame.View.Bot.Logic
{
    class RandomSEV : IStaticEvaluationFunction
    {
        public double Eval(Game game) {
            Random rand = new Random();
            return rand.NextDouble();
        }
    }
}
