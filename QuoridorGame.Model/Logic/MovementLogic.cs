using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Model.Logic
{
    public class MovementLogic
    {
        private readonly Game game;

        public MovementLogic(Game game)
        {
            this.game = game;
        }

        public void MovePlayer(Player player, Cell destination)
        {
            if(player != game.CurrentPlayer)
            {
                throw new QuoridorGameException("Currently it's not seleted player's turn");
            }

            if (!GetAvailableMoves(player.CurrentCell).Contains(destination))
            {
                throw new QuoridorGameException("This move is not allowed.");
            }

            player.CurrentCell = destination;

            if(IsOnEnemySide(destination))
            {
                game.Win(game.CurrentPlayer);
                return;
            }


        }

        /// <summary>
        /// Get list of available cells to which we can move from current cell.
        /// </summary>
        /// <param name="from">Current cell.</param>
        /// <returns>Available moves.</returns>
        public IEnumerable<Cell> GetAvailableMoves(Cell from)
        {
            if (from.AdjacentNodes.Contains(game.OpponentPlayer.CurrentCell))
            {
                var enemyCell = game.OpponentPlayer.CurrentCell;
                var enemySide = GetSide(from, enemyCell);
                var cellAfterEnemy = GetCellAtSide(enemyCell, enemySide);
                if (cellAfterEnemy != null)
                {
                    return from.AdjacentNodes
                        .Where(cell => cell != enemyCell)
                        .Concat(new[] { cellAfterEnemy });
                }

                return from.AdjacentNodes
                        .Where(cell => cell != enemyCell)
                        .Concat(enemyCell.AdjacentNodes.Where(cell => cell != from));
            }
            else
            {
                return from.AdjacentNodes;
            }
        }

        public Side GetSide(Cell from, Cell to)
        {
            if (from.X - 1 > 0 && to == game.GameField.Cells[from.X - 1, from.Y])
            {
                return Side.Top;
            }
            if (from.X + 1 < CellField.FieldSize && to == game.GameField.Cells[from.X + 1, from.Y])
            {
                return Side.Bottom;
            }
            if (from.Y - 1 > 0 && to == game.GameField.Cells[from.X, from.Y - 1])
            {
                return Side.Left;
            }
            if (from.Y + 1 < CellField.FieldSize && to == game.GameField.Cells[from.X, from.Y + 1])
            {
                return Side.Right;
            }

            return Side.NonAdjacent;
        }

        public Cell GetCellAtSide(Cell from, Side side)
        {
            return side switch
            {
                Side.Top => from.X - 1 > 0 ? game.GameField.Cells[from.X - 1, from.Y] : null,
                Side.Bottom => from.X + 1 < CellField.FieldSize ? game.GameField.Cells[from.X + 1, from.Y] : null,
                Side.Left => from.Y - 1 > 0 ? game.GameField.Cells[from.X, from.Y - 1] : null,
                Side.Right => from.Y + 1 < CellField.FieldSize ? game.GameField.Cells[from.X, from.Y + 1] : null,
                _ => null,
            };
        }

        public bool IsOnEnemySide(Cell cell)
        {
            if (game.State == GameState.FirstPlayerTurn && cell.X == CellField.FieldSize - 1)
            {
                return true;
            }
            if (game.State == GameState.SecondPlayerTurn && cell.X == 0)
            {
                return true;
            }
            return false;
        }
    }
}
