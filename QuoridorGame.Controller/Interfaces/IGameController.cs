using QuoridorGame.Model.Entities;

namespace QuoridorGame.Controller.Interfaces
{
    public interface IGameController
    {
        void Move(Cell destination);
        void SetWall(WallType wallType, int x, int y);
    }
}
