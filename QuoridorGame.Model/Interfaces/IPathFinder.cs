using System.Collections.Generic;

namespace QuoridorGame.Model.Interfaces
{
    /// <summary>
    /// Provides utility to find shortest path between two nodes in a graph.
    /// </summary>
    /// <typeparam name="TGraph">Type of graph to traverse.</typeparam>
    /// <typeparam name="TNode">Type of graph nodes.</typeparam>
    public interface IPathFinder<TGraph, TNode>
        where TGraph : IGraph<TNode>
        where TNode : IGraphNode<TNode>
    {
        /// <summary>
        /// Finds shortest path between two nodes.
        /// </summary>
        /// <param name="start">Start node.</param>
        /// <param name="finish">Finish node.</param>
        /// <returns>Shortest path if it exists, empty enumeration otherwise.</returns>
        IEnumerable<TNode> FindShortestPath(TNode start, TNode finish);

        /// <summary>
        /// Determines if path between two nodes in graph exists.
        /// </summary>
        /// <param name="start">Start node.</param>
        /// <param name="finish">Finish node.</param>
        /// <returns>Boolean indicating if path between two nodes exists.</returns>
        bool PathExists(TNode start, TNode finish);
    }
}
