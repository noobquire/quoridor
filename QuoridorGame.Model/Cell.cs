using System.Collections.Generic;
using QuoridorGame.Model.Interfaces;

namespace QuoridorGame.Model
{
    /// <summary>
    /// Game field node on which the player can move.
    /// </summary>
    public class Cell : IGraphNode<Cell>
    {
        public IEnumerable<Cell> AdjacentNodes { get; set; }
    }
}