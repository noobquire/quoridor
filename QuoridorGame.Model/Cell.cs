using System.Collections.Generic;

namespace QuoridorGame.Model
{
    public class Cell
    {
        public IEnumerable<Cell> AdjacentCells { get; set; }
    }
}