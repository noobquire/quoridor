using  QuoridorGame.Model.Entities;
namespace QuoridorGame.Model.Interfaces
{
    public interface IWallPlacer 
    {
    void PlaceWall(Wall wall, WallType type);
    }
}