using QuoridorGame.Controller.Interfaces;
using QuoridorGame.Model.Entities;

namespace QuoridorGame.Controller
{
    public class GameController : IGameController
    {
        public GameController()
        {

        }

        public void Move(Cell destination)
        {
            throw new System.NotImplementedException();
        }

        public void SetWall(WallType wallType, int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}
