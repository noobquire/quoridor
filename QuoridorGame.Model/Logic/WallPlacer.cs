using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Exceptions;
using QuoridorGame.Model.Entities;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Model.Logic
{
    public class WallPlacer : IWallPlacer
    {
        private readonly Game game;
        private IPathFinder<CellField, Cell> pathfinder;
        public WallPlacer(Game game, IPathFinder<CellField, Cell> pathfinder)
        {
            this.game = game;
            this.pathfinder = pathfinder;
        }

        public void PlaceWall(WallType walltype, int x, int y)
        {
            PlaceableIfLegal(walltype, x, y);
            game.NextTurn();

        }
        private void PlaceableIfLegal(WallType walltype, int x, int y)
        {
            /// <summary>
            /// Places wall at given coordinates if it is a legal move.
            /// </summary>
            if (game.CurrentPlayer.WallsCount == 0) 
            {
                throw new QuoridorGameException("No walls left.");
            }
            if (x >= WallsGrid.GridSize || y >= WallsGrid.GridSize)
            {
                throw new QuoridorGameException("WallsGrid index is out of bounds.");
            }
            if (game.GameField.Walls.Grid[x, y].Type != WallType.None)
            {
                throw new QuoridorGameException("Position already taken by other wall.");
            }
            if (walltype == WallType.None)
            {
                throw new QuoridorGameException("WallType can not be WallType:None.");
            }
            if (walltype == WallType.Vertical)
            {
                // Check if no other vertival walls in adjacent positions
                var up_neigh = x != 0 && (game.GameField.Walls[x - 1, y].Type == WallType.Vertical);
                var low_neigh = x != WallsGrid.GridSize - 1 && (game.GameField.Walls[x + 1, y].Type == WallType.Vertical);

                if (up_neigh || low_neigh)
                {
                    throw new QuoridorGameException("Position is blocked by another vertical wall.");
                }
            }
            else
            {
                // Check if no other horizontal walls in adjacent positions
                var left_neigh = y != 0 && (game.GameField.Walls[x, y - 1].Type == WallType.Horizontal);
                var right_neigh = y != WallsGrid.GridSize - 1 && (game.GameField.Walls[x, y + 1].Type == WallType.Horizontal);

                if (left_neigh || right_neigh)
                {
                    throw new QuoridorGameException("Position is blocked by another horizontal wall.");
                }

            }

            //Check if player is not trapped
            if (!PathExists(walltype, x, y))
            {
                throw new QuoridorGameException("Can't block player with a wall.");
            }
            // Wall may be placed if no exeptions occurred
        }

        private bool PathExists(WallType walltype, int x, int y)
        {
            var prevCells = game.GameField.Cells.Save();
            game.GameField.Walls[x, y].Type = walltype;
            var currentCells = game.GameField.Cells;
            var currentWalls = game.GameField.Walls.Grid;


            if (currentWalls[x, y].Type == WallType.Vertical)
            {
                //tear left <-> right upper edge
                currentCells[x, y].RemoveEdge(currentCells[x, y + 1]);
                //tear left <-> right lower edge
                currentCells[x + 1, y].RemoveEdge(currentCells[x + 1, y + 1]);
            }
            if (currentWalls[x, y].Type == WallType.Horizontal)
            {
                //tear up <-> down left node
                currentCells[x, y].RemoveEdge(currentCells[x + 1, y]);
                //tear up -> down right node
                currentCells[x, y + 1].RemoveEdge(currentCells[x + 1, y + 1]);
            }

            Cell firstPlayerCell = game.FirstPlayer.CurrentCell;
            Cell secondPlayerCell = game.SecondPlayer.CurrentCell;
            bool firstPlayerReachable = false;
            bool secondPlayerReachable = false;

            for (int n = 0; n < CellField.FieldSize; n++)
            {
                if (pathfinder.PathExists(secondPlayerCell, game.GameField.Cells[0, n]))
                {
                    firstPlayerReachable = true;
                    break;
                }
            }

            for (int n = 0; n < CellField.FieldSize; n++)
            {
                if (pathfinder.PathExists(firstPlayerCell, game.GameField.Cells[CellField.FieldSize - 1, n]))
                {
                    secondPlayerReachable = true;
                    break;
                }
            }

            if (firstPlayerReachable && secondPlayerReachable)
            {
                game.CurrentPlayer.WallsCount -= 1;
                return true;
            }
            else
            {
                game.GameField.Walls[x, y].Type = WallType.None;
                game.GameField.Cells.Restore(prevCells);
                game.FirstPlayer.CurrentCell = game.GameField.Cells[game.FirstPlayer.CurrentCell.X, game.FirstPlayer.CurrentCell.Y];
                game.SecondPlayer.CurrentCell = game.GameField.Cells[game.SecondPlayer.CurrentCell.X, game.SecondPlayer.CurrentCell.Y];
                pathfinder = new PathFinder<CellField, Cell>(game.GameField.Cells);
                return false;
            }
        }
    }
}