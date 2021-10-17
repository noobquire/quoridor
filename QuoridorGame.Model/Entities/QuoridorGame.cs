using QuoridorGame.Model.Exceptions;
using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Logic;
using System;

namespace QuoridorGame.Model.Entities
{
    public class QuoridorGame
    {
        private readonly IWallPlacer wallPlacer;
        private readonly IMovementLogic movementLogic;

        public event Action<GameField> GameStarted;
        public event Action<GameField> FieldUpdated;
        public event Action<Player> PlayerWon;

        public GameState State { get; private set; }
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

            wallPlacer = new WallPlacer(this, new PathFinder<CellField, Cell>(GameField.Cells));
            movementLogic = new MovementLogic(this);
        }

        public void Win(Player player)
        {
            if (player == FirstPlayer)
            {
                State = GameState.FirstPlayerWin;
            }
            if (player == SecondPlayer)
            {
                State = GameState.SecondPlayerWin;
            }
            PlayerWon?.Invoke(player);
        }

        public void NextTurn()
        {
            if (State == GameState.FirstPlayerTurn)
            {
                State = GameState.SecondPlayerTurn;
            }
            else if (State == GameState.SecondPlayerTurn)
            {
                State = GameState.FirstPlayerTurn;
            }
        }

        public void Start()
        {
            if (State == GameState.Pregame)
            {
                State = GameState.FirstPlayerTurn;
            }
            GameStarted?.Invoke(GameField);
        }

        public void Move(int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                throw new QuoridorGameException("Index was out of bounds");
            }
            movementLogic.MovePlayer(CurrentPlayer, GameField.Cells[x, y]);
            FieldUpdated?.Invoke(GameField);
        }

        public void SetWall(WallType wallType, int x, int y)
        {
            wallPlacer.PlaceWall(wallType, x, y);
            FieldUpdated?.Invoke(GameField);
        }
    }
}
