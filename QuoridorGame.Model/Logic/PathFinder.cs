using QuoridorGame.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace QuoridorGame.Model.Logic
{
    /// <summary>
    /// Provides utility to find shortest path between two nodes in a graph.
    /// Uses BFS algorithm.
    /// </summary>
    /// <typeparam name="TGraph">Type of graph to traverse.</typeparam>
    /// <typeparam name="TNode">Type of graph nodes.</typeparam>
    public class PathFinder<TGraph, TNode> : IPathFinder<TGraph, TNode>
        where TGraph : IGraph<TNode>
        where TNode : FastPriorityQueueNode, IGraphNode<TNode>
    {
        protected readonly IGraph<TNode> graph;

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
        public virtual IEnumerable<TNode> FindShortestPath(TNode start, TNode finish)
        {
            var allNodes = graph.Nodes.Flatten();

            if (!allNodes.Contains(start) || !allNodes.Contains(finish))
            {
                return Enumerable.Empty<TNode>();
            }

            var path = new List<TNode>();
            var queue = new FastPriorityQueue<TNode>(100);
            var distance = GetDistance(start, finish);
            queue.Enqueue(start, distance);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Equals(finish))
                {
                    return GetPathFromParents(start, finish);
                }

                foreach (var adjacentNode in node.AdjacentNodes)
                {
                    if (path.Contains(adjacentNode))
                    {
                        continue;
                    }

                    adjacentNode.ParentNode = node;
                    path.Add(adjacentNode);
                    distance = GetDistance(adjacentNode, finish);
                    queue.Enqueue(adjacentNode, distance);
                }
            }

            return Enumerable.Empty<TNode>();
        }

        private static int GetDistance(TNode start, TNode target)
        {
            return Math.Abs(target.X - start.X) + Math.Abs(target.Y - start.Y);
        }

        protected IEnumerable<TNode> GetPathFromParents(TNode start, TNode finish)
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

        public int ShortestPathLength(TNode start, TNode finish)
        {
            if (start.Equals(finish))
            {
                return 0;
            }

            var path = FindShortestPath(start, finish);

            if(path.Count() == 0)
            {
                return int.MaxValue;
            }

            return path.Count();
        }

    }
}
