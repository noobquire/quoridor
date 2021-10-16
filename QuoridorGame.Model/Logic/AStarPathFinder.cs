using QuoridorGame.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoridorGame.Model.Logic
{
    public class AStarPathFinder<TGraph, TNode> : PathFinder<TGraph, TNode>
        where TGraph : IGraph<TNode>
        where TNode : IGraphNode<TNode>
    {
        public AStarPathFinder(TGraph graph) : base(graph)
        {
        }

        public override IEnumerable<TNode> FindShortestPath(TNode start, TNode finish)
        {
            var allNodes = graph.Nodes.Flatten();

            if (!allNodes.Contains(start) || !allNodes.Contains(finish))
            {
                return Enumerable.Empty<TNode>();
            }

            var path = new List<TNode>();
            var queue = new PriorityQueue<TNode, int>();
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
    }
}
