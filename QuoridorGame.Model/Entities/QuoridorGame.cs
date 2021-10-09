namespace QuoridorGame.Model.Entities
{
    public class QuoridorGame
    {
        public GameState State { get; set; }
        public GameField GameField { get; }
        public Player FirstPlayer { get; }
        public Player SecondPlayer { get; }

        public Player CurrentPlayer => State switch
        {
            GameState.FirstPlayerTurn => FirstPlayer,
            GameState.SecondPlayerTurn => SecondPlayer,
            _ => null
        };

        public Player OpponentPlayer => State switch
        {
            GameState.FirstPlayerTurn => SecondPlayer,
            GameState.SecondPlayerTurn => FirstPlayer,
            _ => null
        };

        public QuoridorGame()
        {
            var cellField = new CellField();
            var wallsGrid = new WallsGrid();
            GameField = new GameField(cellField, wallsGrid);
            FirstPlayer = new Player(GameField.Cells[0, 4]);
            SecondPlayer = new Player(GameField.Cells[8, 4]);
        }

        public void Win(Player player)
        {
            if(player == FirstPlayer)
            {
                State = GameState.FirstPlayerWin;
            }
            if (player == SecondPlayer)
            {
                State = GameState.SecondPlayerWin;
            }
        }

        public void NextTurn()
        {
            if(State == GameState.FirstPlayerTurn)
            {
                State = GameState.SecondPlayerTurn;
            }
            if (State == GameState.SecondPlayerTurn)
            {
                State = GameState.FirstPlayerTurn;
            }
        }

        public void Start()
        {
            if(State == GameState.Pregame)
            {
                State = GameState.FirstPlayerTurn;
            }
        }
    }
}
