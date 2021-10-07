namespace QuoridorGame.Model.Interfaces
{
    public interface IGraph<T> where T : IGraphNode<T>
    {
        T[,] Nodes { get; set; }
    }
}