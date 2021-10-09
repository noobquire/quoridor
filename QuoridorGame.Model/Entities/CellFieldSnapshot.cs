using QuoridorGame.Model.Interfaces;

namespace QuoridorGame.Model.Entities
{
    public class CellFieldSnapshot : IMemento<Cell[,]>
    {
        private readonly Cell[,] data;

        public Cell[,] Data => data;

        public CellFieldSnapshot(Cell[,] data)
        {
            this.data = data;
        }
    }
}
