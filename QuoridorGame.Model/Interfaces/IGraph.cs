namespace QuoridorGame.Model.Interfaces
{
    /// <summary>
    /// Represents twodimensional unoriented unweighted graph.
    /// </summary>
    /// <typeparam name="T">Data stored in graph nodes.</typeparam>
    public interface IGraph<T> where T : IGraphNode<T>
    {
        /// <summary>
        /// Nodes of the graph.
        /// </summary>
        T[,] Nodes { get; set; }
    }
}