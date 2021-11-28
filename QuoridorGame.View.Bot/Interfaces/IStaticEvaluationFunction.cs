using Game = QuoridorGame.Model.Entities.QuoridorGame;
using QuoridorGame.Model.Entities;

namespace QuoridorGame.View.Bot.Interfaces
{
    interface IStaticEvaluationFunction
    {
        public double Eval(Game game);
    }
}
