using System;

namespace QuoridorGame.Model.Entities
{
    /// <summary>
    /// Quoridor game field consisting of walkable cells and walls.
    /// </summary>
    public class GameField
    {
        public CellField Cells { get; internal set; }
        public WallsGrid Walls { get; internal set; }

        public GameField(CellField cellField, WallsGrid wallsGrid)
        {
            Cells = cellField;
            Walls = wallsGrid;
        }
    }
}
