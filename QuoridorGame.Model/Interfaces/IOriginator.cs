namespace QuoridorGame.Model.Interfaces
{
    public interface IOriginator<T>
    {
        IMemento<T> Save();
        void Restore(IMemento<T> memento);
    }
}
