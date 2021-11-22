using QuoridorGame.Model.Events;
using QuoridorGame.Model.Exceptions;
using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoridorGame.Model.Entities
{
    public class QuoridorGame
    {
        internal IWallPlacer wallPlacer;
        private readonly IMovementLogic movementLogic;

        public Cell[] AvailableMoves => CurrentPlayer == null ? Array.Empty<Cell>() : movementLogic.GetAvailableMoves(CurrentPlayer.CurrentCell).ToArray();
        public Wall[] AvailableWalls => (CurrentPlayer?.WallsCount ?? 0) == 0 ? Array.Empty<Wall>() : wallPlacer.GetAvailableWalls().ToArray();

        public event EventHandler<GameStartedEventArgs> GameStarted;
        public event EventHandler<GameWonEventArgs> GameWon;
        public event EventHandler<WallPlacedEventArgs> WallPlaced;
        public event EventHandler<PlayerMovedEventArgs> PlayerMoved;
        public event EventHandler<NextTurnEventArgs> NewTurn;

        public GameState State { get; private set; }
        public GameField GameField { get; }
        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }
        public Stack<Turn> Turns { get; }
        public bool IsBotTurn { get; set; }

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
            Turns = new Stack<Turn>();

            wallPlacer = new WallPlacer(this, new PathFinder<CellField, Cell>(GameField.Cells));
            movementLogic = new MovementLogic(this);
        }

        public void Win(Player player)
        {
            if (player == FirstPlayer)
            {
                State = GameState.FirstPlayerWin;
                if(!IsBotTurn)
                {
                    GameWon?.Invoke(this, new GameWonEventArgs(1));
                }
            }
            if (player == SecondPlayer)
            {
                State = GameState.SecondPlayerWin;
                if(!IsBotTurn)
                {
                    GameWon?.Invoke(this, new GameWonEventArgs(2));
                }
            }
        }

        public void NextTurn()
        {
            if (State == GameState.FirstPlayerTurn)
            {
                State = GameState.SecondPlayerTurn;
                if (!IsBotTurn)
                {
                    NewTurn?.Invoke(this, new NextTurnEventArgs(2));
                }
            }
            else if (State == GameState.SecondPlayerTurn)
            {
                State = GameState.FirstPlayerTurn;
                if (!IsBotTurn)
                {
                    NewTurn?.Invoke(this, new NextTurnEventArgs(1));
                }
            }
        }

        public void Start()
        {
            if (!new[]
            {
                GameState.FirstPlayerWin,
                GameState.SecondPlayerWin,
                GameState.Pregame
            }
            .Contains(State))
            {
                throw new QuoridorGameException("Cannot start, game is already in progress.");
            }

            if (State == GameState.Pregame)
            {
                State = GameState.SecondPlayerTurn;
                GameStarted?.Invoke(this, new GameStartedEventArgs());
            }
            if (State == GameState.FirstPlayerWin || State == GameState.SecondPlayerWin)
            {
                // re-init game
                var cellField = new CellField();
                var wallsGrid = new WallsGrid();
                GameField.Cells = cellField;
                GameField.Walls = wallsGrid;
                FirstPlayer = new Player(GameField.Cells[0, 4]);
                SecondPlayer = new Player(GameField.Cells[8, 4]);
                wallPlacer = new WallPlacer(this, new PathFinder<CellField, Cell>(GameField.Cells));
                State = GameState.FirstPlayerTurn;
                GameStarted?.Invoke(this, new GameStartedEventArgs(true));
            }
            NewTurn?.Invoke(this, new NextTurnEventArgs(2));
        }

        public bool PopTurn()
        {
            var turnsLeft = Turns.TryPop(out var turn);
            if (!turnsLeft)
            {
                return false;
            }

            if (turn is MoveTurn moveTurn)
            {
                State = moveTurn.StateBefore;
                var fromCell = GameField.Cells[moveTurn.FromX, moveTurn.FromY];
                movementLogic.RollbackPlayerMove(CurrentPlayer, fromCell);
            }
            else if (turn is WallTurn wallTurn)
            {
                State = wallTurn.StateBefore;
                var wall = GameField.Walls[wallTurn.X, wallTurn.Y];
                wallPlacer.RemoveWall(wall);
            }

            return true;
        }

        public void Move(int x, int y)
        {
            var turn = new MoveTurn
            {
                FromX = CurrentPlayer.CurrentCell.X,
                FromY = CurrentPlayer.CurrentCell.Y,
                X = x,
                Y = y,
                PlayerNumber = CurrentPlayer == FirstPlayer ? 1 : 2,
                StateBefore = State
            };
            if (State != GameState.FirstPlayerTurn && State != GameState.SecondPlayerTurn)
            {
                throw new QuoridorGameException("Cannot move, no game in progress.");
            }
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                throw new QuoridorGameException("Index was out of bounds");
            }
            Turns.Push(turn);
            movementLogic.MovePlayer(CurrentPlayer, GameField.Cells[x, y]);
            if (!IsBotTurn)
            {
                var playerNumber = CurrentPlayer == FirstPlayer ? 1 : 2;
                PlayerMoved?.Invoke(this, new PlayerMovedEventArgs(playerNumber, x, y));
            }
        }

        public void SetWallWithoutEvent(WallType wallType, int x, int y)
        {
            var turn = new WallTurn
            {
                X = x,
                Y = y,
                PlayerNumber = CurrentPlayer == FirstPlayer ? 1 : 2,
                StateBefore = State
            };
            if (!Enum.IsDefined(typeof(WallType), wallType))
            {
                throw new QuoridorGameException($"Cannot place wall of unknown type {wallType}");
            }
            if (State != GameState.FirstPlayerTurn && State != GameState.SecondPlayerTurn)
            {
                throw new QuoridorGameException("Cannot place wall, no game in progress.");
            }
            var wall = GameField.Walls[x, y];
            wallPlacer.PlaceWall(wall, wallType);
            Turns.Push(turn);
            if(!IsBotTurn)
            {
                var playerNumber = CurrentPlayer == FirstPlayer ? 1 : 2;
                WallPlaced?.Invoke(this, new WallPlacedEventArgs(wallType, x, y, OpponentPlayer.WallsCount, playerNumber));
            }
        }
    }
}
