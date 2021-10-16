using System;
using System.Collections.Generic;
using System.Linq;
using QuoridorGame.Model.Interfaces;

namespace QuoridorGame.Model.Entities
{
    /// <summary>
    /// Game field node on which the player can move.
    /// </summary>
    [Serializable]
    public class Cell : IGraphNode<Cell>
    {
        public IEnumerable<Cell> AdjacentNodes { get; set; }
        public int X { get; }
        public int Y { get; }
        public Cell ParentNode { get; set; }

        public Cell(int x, int y)
        {
            X = x; 
            Y = y;
        }

        public bool IsAdjacentTo(Cell cell)
        {
            return AdjacentNodes.Contains(cell);
        }

        public void RemoveEdge(Cell adjacentNode) 
        {
            AdjacentNodes = AdjacentNodes.Where(cell => cell != adjacentNode).ToList();
            adjacentNode.AdjacentNodes = adjacentNode.AdjacentNodes.Where(cell => cell != this).ToList();
        }
    }
}