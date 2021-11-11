using Game = QuoridorGame.Model.Entities.QuoridorGame;
using QuoridorGame.Model.Entities;

namespace QuoridorGame.View.Bot.Interfaces
{
    interface IStaticEvaluationFunction
    {
        public float Eval(Game game, Player evalplayer);
    }
}
