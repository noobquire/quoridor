using QuoridorGame.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace QuoridorGame.Model.Entities
{
    /// <summary>
    /// Provides utility to find shortest path between two nodes in a graph.
    /// Uses BFS algorithm.
    /// </summary>
    /// <typeparam name="TGraph">Type of graph to traverse.</typeparam>
    /// <typeparam name="TNode">Type of graph nodes.</typeparam>
    public class PathFinder<TGraph, TNode> : IPathFinder<TGraph, TNode>
        where TGraph : IGraph<TNode>
        where TNode : IGraphNode<TNode>
    {
        private readonly IGraph<TNode> graph;

        /// <summary>
        /// Initializes a new instance of <see cref="PathFinder{TGraph, TNode}"/> class.
        /// </summary>
        /// <param name="graph">Graph to search shortest paths in.</param>
        public PathFinder(TGraph graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Finds shortest path between two nodes using BFS.
        /// </summary>
        /// <param name="start">Start node.</param>
        /// <param name="finish">Finish node.</param>
        /// <returns>Shortest path if it exists, empty enumeration otherwise.</returns>
        public IEnumerable<TNode> FindShortestPath(TNode start, TNode finish)
        {
            var allNodes = graph.Nodes.Flatten();

            if (!allNodes.Contains(start) || !allNodes.Contains(finish))
            {
                return Enumerable.Empty<TNode>();
            }

            //var visitedNodes = new HashSet<TNode>();
            var path = new List<TNode>();
            var queue = new Queue<TNode>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Equals(finish))
                {
                    return GetPathFromParents(start, finish); // no, queue is current layer, not path
                }

                foreach (var adjacentNode in node.AdjacentNodes)
                {
                    if (path.Contains(adjacentNode))
                    {
                        continue;
                    }

                    adjacentNode.ParentNode = node;
                    path.Add(adjacentNode);
                    queue.Enqueue(adjacentNode);
                }
            }

            return Enumerable.Empty<TNode>();
        }

        private IEnumerable<TNode> GetPathFromParents(TNode start, TNode finish)
        {
            var path = new List<TNode>();
            var currentNode = finish;
            while (!currentNode.Equals(start))
            {
                path.Add(currentNode);
                currentNode = currentNode.ParentNode;
            }
            path.Reverse();
            return path;
        }

        /// <inheritdoc/>
        public bool PathExists(TNode start, TNode finish)
        {
            if (start.Equals(finish))
            {
                return true;
            }

            var path = FindShortestPath(start, finish);
            return path.Any();
        }
    }
}
