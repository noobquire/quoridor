using QuoridorGame.Model.Entities;
using System.Collections.Generic;

namespace QuoridorGame.Model.Interfaces
{
    public interface IWallPlacer
    {
        void PlaceWall(Wall wall, WallType wallType);
        IEnumerable<Wall> GetAvailableWalls();
    }
}