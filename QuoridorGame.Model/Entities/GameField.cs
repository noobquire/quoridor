using QuoridorGame.Model.Interfaces;

namespace QuoridorGame.Model.Entities
{
    /// <summary>
    /// Quoridor game field consisting of walkable cells and walls.
    /// </summary>
    public class GameField
    {
        public CellField Cells { get; }
        public WallsGrid Walls {  get; }

        public GameField(CellField cellField, WallsGrid wallsGrid)
        {
            Cells = cellField;
            Walls = wallsGrid;
        }
    }
}
