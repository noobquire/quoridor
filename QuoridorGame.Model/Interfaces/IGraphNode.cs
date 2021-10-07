using System.Collections.Generic;

namespace QuoridorGame.Model.Interfaces
{
    /// <summary>
    /// Represents unoriented unweighted graph node.
    /// </summary>
    /// <typeparam name="T">Data stored in graph node.</typeparam>
    public interface IGraphNode<T> where T : IGraphNode<T>
    {
        /// <summary>
        /// Nodes to which we can traverse from current node.
        /// </summary>
        IEnumerable<T> AdjacentNodes { get; set; }

        /// <summary>
        /// Parent node used for path finding.
        /// </summary>
        T ParentNode { get; set; }
    }
}