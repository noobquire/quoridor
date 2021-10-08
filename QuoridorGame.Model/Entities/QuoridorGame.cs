namespace QuoridorGame.Model.Entities
{
    public class QuoridorGame
    {
        public GameField GameField { get; }
        public Player FirstPlayer { get; }
        public Player SecondPlayer { get; }

        public QuoridorGame()
        {
            var cellField = new CellField();
            var wallsGrid = new WallsGrid();
            GameField = new GameField(cellField, wallsGrid);
            FirstPlayer = new Player(GameField.Cells[0, 4]);
            SecondPlayer = new Player(GameField.Cells[8, 4]);
        }
    }
}
