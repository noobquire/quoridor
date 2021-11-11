using QuoridorGame.View.Bot.Interfaces;
using QuoridorGame.Model.Entities;
using Game = QuoridorGame.Model.Entities.QuoridorGame;
using System; 

namespace QuoridorGame.View.Bot.Logic
{
    class RandomSEV : IStaticEvaluationFunction
    {
        public float Eval(Game game) {
            Random rand = new Random();
            return (float)rand.NextDouble();
        }
    }
}
