using System;
using System.Collections.Generic;
using QuoridorGame.Model.Interfaces;

namespace QuoridorGame.Model.Entities
{
    /// <summary>
    /// Game field node on which the player can move.
    /// </summary>
    public class Cell : IGraphNode<Cell>
    {
        public List<Cell> AdjacentNodes { get; set; }
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

        public static void RemoveEdge(Cell from, Cell to) 
        {
            from.AdjacentNodes.Remove(to);
            to.AdjacentNodes.Remove(from);
        }

        public static void AddEdge(Cell from, Cell to)
        {
            from.AdjacentNodes.Add(to);
            to.AdjacentNodes.Add(from);
        }
    }
}