using NUnit.Framework;
using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Logic;
using System;
using System.Linq;

namespace QuoridorGame.Model.Tests
{
    [TestFixture(typeof(PathFinder<CellField, Cell>), typeof(CellField), typeof(Cell))]
    [TestFixture(typeof(AStarPathFinder<CellField, Cell>), typeof(CellField), typeof(Cell))]
    public class PathFinderTests<T, TGraph, Node> 
        where TGraph : IGraph<Node>
        where Node : IGraphNode<Node>
        where T : IPathFinder<TGraph, Node>
    {
        private IGraph<Node> graph;
        private IPathFinder<TGraph, Node> pathFinder;

        [SetUp]
        public void Setup()
        {
            graph = (IGraph<Node>)new CellField();
            pathFinder = (T)Activator.CreateInstance(typeof(T), new object[] { graph });
        }

        [TestCase(0, 4, 8, 4)]
        [TestCase(0, 0, 8, 8)]
        [TestCase(5, 5, 5, 5)]
        public void PathExists_WhenThereIsPath_ShouldReturnTrue(int startX, int startY, int finishX, int finishY)
        {
            var start = graph.Nodes[startX, startY];
            var finish = graph.Nodes[finishX, finishY];

            var result = pathFinder.PathExists(start, finish);

            Assert.IsTrue(result);
        }

        private void SplitGraph()
        {
            for (int i = 0; i < 9; i++)
            {
                var bottomNeigbour = graph.Nodes[5, i];
                var newNegibours = graph.Nodes[4, i].AdjacentNodes.ToList();
                newNegibours.Remove(bottomNeigbour);
                graph.Nodes[4, i].AdjacentNodes = newNegibours;
            }
        }

        [TestCase(0, 4, 8, 4)]
        [TestCase(0, 0, 8, 8)]
        public void PathExists_WhenNoPath_ShouldReturnFalse(int startX, int startY, int finishX, int finishY)
        {
            SplitGraph();

            var start = graph.Nodes[startX, startY];
            var finish = graph.Nodes[finishX, finishY];

            var result = pathFinder.PathExists(start, finish);

            Assert.IsFalse(result);
        }

        [TestCase(0, 4, 8, 4)]
        [TestCase(0, 0, 8, 8)]
        public void FindShortestPath_WhenNoPath_ReturnsEmptyEnumerable(int startX, int startY, int finishX, int finishY)
        {
            SplitGraph();

            var start = graph.Nodes[startX, startY];
            var finish = graph.Nodes[finishX, finishY];

            var result = pathFinder.FindShortestPath(start, finish);

            Assert.IsEmpty(result);
        }

        [Test]
        public void FindShortestPath_WhenSameNode_ReturnsEpmtyEnumerable()
        {
            SplitGraph();

            var start = graph.Nodes[5, 5];
            var finish = graph.Nodes[5, 5];

            var result = pathFinder.FindShortestPath(start, finish);

            Assert.IsEmpty(result);
        }

        [TestCase(0, 4, 8, 4, 8)]
        [TestCase(0, 0, 8, 8, 16)]
        [TestCase(5, 5, 5, 5, 0)]
        [TestCase(3, 4, 4, 3, 2)]
        public void FindShortestPath_WhenPathExists_ReturnsExpectedPathLength(int startX, int startY, int finishX, int finishY, int expectedLength)
        {
            var start = graph.Nodes[startX, startY];
            var finish = graph.Nodes[finishX, finishY];

            var result = pathFinder.FindShortestPath(start, finish);

            Assert.AreEqual(expectedLength, result.Count());
        }
    }
}
