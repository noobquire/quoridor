using QuoridorGame.Model.Interfaces;

namespace QuoridorGame.Model.Entities
{
    /// <summary>
    /// Cells on game field on which players can walk.
    /// </summary>
    public class CellField : IGraph<Cell>
    {
        public const int FieldSize = 9;
        public Cell[,] Nodes { get; set; }

        public CellField()
        {
            Nodes = new Cell[FieldSize, FieldSize];
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    Nodes[i, j] = new Cell(i, j);
                }
            }

            InitMiddleCells();
            InitBorderCells();
            InitCornerCells();
        }

        /// <summary>
        /// Initializes border cells (except corners) with three adjacent cells.
        /// </summary>
        private void InitBorderCells()
        {
            for (int i = 1; i < FieldSize - 1; i++)
            {
                // top row
                Nodes[0, i].AdjacentNodes = new[]
                {
                    Nodes[0, i - 1],
                    Nodes[0, i + 1],
                    Nodes[1, i]
                };

                // bottom row
                Nodes[FieldSize - 1, i].AdjacentNodes = new[]
                {
                    Nodes[FieldSize - 1, i - 1],
                    Nodes[FieldSize - 1, i + 1],
                    Nodes[FieldSize - 2, i]
                };

                // first column
                Nodes[i, 0].AdjacentNodes = new[]
                {
                    Nodes[i - 1, 0],
                    Nodes[i + 1, 0],
                    Nodes[i, 1]
                };

                // last column
                Nodes[i, FieldSize - 1].AdjacentNodes = new[]
                {
                    Nodes[i - 1, FieldSize - 1],
                    Nodes[i + 1, FieldSize - 1],
                    Nodes[i, FieldSize - 2]
                };
            }
        }

        /// <summary>
        /// Initializes middle cells with four adjacent cells.
        /// </summary>
        private void InitMiddleCells()
        {
            for (int i = 1; i < FieldSize - 1; i++)
            {
                for (int j = 1; j < FieldSize - 1; j++)
                {
                    Nodes[i, j].AdjacentNodes = new[]
                    {
                        Nodes[i, j - 1],
                        Nodes[i, j + 1],
                        Nodes[i - 1, j],
                        Nodes[i + 1, j]
                    };
                }
            }
        }

        /// <summary>
        /// Initializes corner cells with two adjacent cells.
        /// </summary>
        private void InitCornerCells()
        {
            Nodes[0, 0].AdjacentNodes = new[] // top left
            {
                Nodes[0, 1],
                Nodes[1, 0]
            };

            Nodes[0, FieldSize - 1].AdjacentNodes = new[] // top right
            {
                Nodes[0, FieldSize - 2],
                Nodes[1, FieldSize - 1]
            };

            Nodes[FieldSize - 1, 0].AdjacentNodes = new[] // bottom left
            {
                Nodes[FieldSize - 2, 0],
                Nodes[FieldSize - 1, 1]
            };

            Nodes[FieldSize - 1, FieldSize - 1].AdjacentNodes = new[] // bottom right
            {
                Nodes[FieldSize - 2, FieldSize - 1],
                Nodes[FieldSize - 1, FieldSize - 2]
            };
        }

        /// <summary>
        /// Indexer which allows to access cell field as an array.
        /// </summary>
        /// <param name="x">Row of array.</param>
        /// <param name="y">Column of array.</param>
        /// <returns>Cell at specified index.</returns>
        public Cell this[int x, int y] 
        { 
            get { return Nodes[x, y]; }
            set {  Nodes[x, y] = value; }
        }

    }
}
