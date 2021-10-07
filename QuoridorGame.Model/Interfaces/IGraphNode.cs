using System.Collections.Generic;

namespace QuoridorGame.Model.Interfaces
{
    public interface IGraphNode<T> where T : IGraphNode<T>
    {
        IEnumerable<T> AdjacentNodes { get; set; }
    }
}