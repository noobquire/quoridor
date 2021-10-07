namespace QuoridorGame.Model
{
    public class GameField
    {
        private const int FieldSize = 9;
        public Cell[,] Cells { get; set; }

        public GameField()
        {
            Cells = new Cell[FieldSize, FieldSize];
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    Cells[i, j] = new Cell();
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
                Cells[0, i].AdjacentCells = new[]
                {
                    Cells[0, i - 1],
                    Cells[0, i + 1],
                    Cells[1, i]
                };

                // bottom row
                Cells[FieldSize - 1, i].AdjacentCells = new[]
                {
                    Cells[FieldSize - 1, i - 1],
                    Cells[FieldSize - 1, i + 1],
                    Cells[FieldSize - 2, i]
                };

                // first column
                Cells[i, 0].AdjacentCells = new[]
                {
                    Cells[i - 1, 0],
                    Cells[i + 1, 0],
                    Cells[i, 1]
                };

                // last column
                Cells[i, FieldSize - 1].AdjacentCells = new[]
                {
                    Cells[i - 1, FieldSize - 1],
                    Cells[i + 1, FieldSize - 1],
                    Cells[i, FieldSize - 2]
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
                    Cells[i, j].AdjacentCells = new[]
                    {
                        Cells[i, j - 1],
                        Cells[i, j + 1],
                        Cells[i - 1, j],
                        Cells[i + 1, j]
                    };
                }
            }
        }

        /// <summary>
        /// Initializes corner cells with two adjacent cells.
        /// </summary>
        private void InitCornerCells()
        {
            Cells[0, 0].AdjacentCells = new[] // top left
            {
                Cells[0, 1],
                Cells[1, 0]
            };
            Cells[0, FieldSize - 1].AdjacentCells = new[] // top right
            {
                Cells[0, FieldSize - 2],
                Cells[1, FieldSize - 1]
            };
            Cells[FieldSize - 1, 0].AdjacentCells = new[] // bottom left
            {
                Cells[FieldSize - 2, 0],
                Cells[FieldSize - 1, 1]
            };
            Cells[FieldSize - 1, FieldSize - 1].AdjacentCells = new[] // bottom right
            {
                Cells[FieldSize - 2, FieldSize - 1],
                Cells[FieldSize - 1, FieldSize - 2]
            };
        }
    }
}