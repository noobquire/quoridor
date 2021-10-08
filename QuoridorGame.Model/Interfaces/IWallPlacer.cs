using  QuoridorGame.Model.Entities;
namespace QuoridorGame.Model.Interfaces
{
    public interface IWallPlacer 
    {
    void PlaceWall(WallType walltype, int x, int y);
    }
}