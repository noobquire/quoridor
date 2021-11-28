using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Exceptions;
using QuoridorGame.Model.Entities;
using Game = QuoridorGame.Model.Entities.QuoridorGame;
using System;
using System.Collections.Generic;

namespace QuoridorGame.Model.Logic
{
    [Serializable]
    public class WallPlacer : IWallPlacer
    {
        private readonly Game game;
        private IPathFinder<CellField, Cell> pathfinder;
        private string errMessage = "Can't block players with wall";
        public WallPlacer(Game game, IPathFinder<CellField, Cell> pathfinder)
        {
            this.game = game;
            this.pathfinder = pathfinder;
        }

        public void PlaceWall(Wall wall, WallType wallType)
        {
            var newWall = new Wall(wall.X, wall.Y) { Type = wallType };
            if (CanPlaceWall(newWall) && PlayersCanReachFinish(newWall))
            {
                wall.Type = wallType;
                RemoveGraphEdges(newWall);
                game.CurrentPlayer.WallsCount -= 1;
            }
            else
            {
                throw new QuoridorGameException($"Can't place {newWall}. " + errMessage);
            }
        }

        /// <summary>
        /// Places wall at given coordinates if it is a legal move.
        /// </summary>
        private bool CanPlaceWall(Wall wall)
        {
            int x = wall.X;
            int y = wall.Y;
            if (game.CurrentPlayer.WallsCount == 0)
            {
                errMessage = "No walls left.";
                return false;
            }
            if (x >= WallsGrid.GridSize || y >= WallsGrid.GridSize)
            {
                errMessage = "WallsGrid index is out of bounds.";
                return false;
                //throw new QuoridorGameException();
            }
            if (game.GameField.Walls.Grid[x, y].Type != WallType.None)
            {
                errMessage = "Position already taken by other wall.";
                return false;
            }
            if (wall.Type == WallType.None)
            {
                errMessage = "WallType can not be WallType:None.";
                return false;
            }
            if (wall.Type == WallType.Vertical)
            {
                // Check if no other vertival walls in adjacent positions
                var up_neigh = x != 0 && (game.GameField.Walls[x - 1, y].Type == WallType.Vertical);
                var low_neigh = x != WallsGrid.GridSize - 1 && (game.GameField.Walls[x + 1, y].Type == WallType.Vertical);

                if (up_neigh || low_neigh)
                {
                    errMessage = "Position is blocked by another vertical wall.";
                    return false;
                }
            }
            else
            {
                // Check if no other horizontal walls in adjacent positions
                var left_neigh = y != 0 && (game.GameField.Walls[x, y - 1].Type == WallType.Horizontal);
                var right_neigh = y != WallsGrid.GridSize - 1 && (game.GameField.Walls[x, y + 1].Type == WallType.Horizontal);

                if (left_neigh || right_neigh)
                {
                    errMessage = "Position is blocked by another horizontal wall.";
                    return false;
                }

            }

            // Wall can be placed if no exeptions occurred
            return true;
        }

        private void AddOrRemoveGraphEdges(Wall wall, Action<Cell, Cell> action)
        {
            var cells = game.GameField.Cells;
            int x = wall.X;
            int y = wall.Y;
            if (wall.Type == WallType.Vertical)
            {
                // tear left <-> right upper edge
                action(cells[x, y], cells[x, y + 1]);
                // tear left <-> right lower edge
                action(cells[x + 1, y], cells[x + 1, y + 1]);
            }
            if (wall.Type == WallType.Horizontal)
            {
                // tear up <-> down left node
                action(cells[x, y], cells[x + 1, y]);
                // tear up -> down right node
                action(cells[x, y + 1], cells[x + 1, y + 1]);
            }
        }

        private void RemoveGraphEdges(Wall wall)
        {
            AddOrRemoveGraphEdges(wall, Cell.RemoveEdge);
        }

        private void AddGraphEdges(Wall wall)
        {
            AddOrRemoveGraphEdges(wall, Cell.AddEdge);
        }

        private bool CanReachFinishFrom(Cell cell, int row)
        {
            for (int n = 0; n < CellField.FieldSize; n++)
            {
                if (pathfinder.PathExists(cell, game.GameField.Cells[row, n]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool PlayersCanReachFinish(Wall wall)
        {
            RemoveGraphEdges(wall);

            Cell firstPlayerCell = game.FirstPlayer.CurrentCell;
            Cell secondPlayerCell = game.SecondPlayer.CurrentCell;
            bool firstPlayerReachable = CanReachFinishFrom(firstPlayerCell, CellField.FieldSize - 1);
            bool secondPlayerReachable = CanReachFinishFrom(secondPlayerCell, 0);

            if (firstPlayerReachable && secondPlayerReachable)
            {
                AddGraphEdges(wall); // rollback graph edges
                return true;
            }
            else
            {
                AddGraphEdges(wall); // rollback graph edges
                return false;
            }
            
        }

        public IEnumerable<Wall> GetAvailableWalls()
        {
            var availableWalls = new List<Wall>();
            for (int i = 0; i < WallsGrid.GridSize; i++)
            {
                for (int j = 0; j < WallsGrid.GridSize; j++)
                {
                    var wall = game.GameField.Walls[i, j];
                    var verticalWall = new Wall(wall.X, wall.Y) { Type = WallType.Vertical };
                    if (CanPlaceWall(verticalWall) && PlayersCanReachFinish(verticalWall))
                    {
                        availableWalls.Add(verticalWall);
                    }

                    var horizontalWall = new Wall(wall.X, wall.Y) { Type = WallType.Horizontal };
                    if (CanPlaceWall(horizontalWall) && PlayersCanReachFinish(horizontalWall))
                    {
                        availableWalls.Add(horizontalWall);
                    }
                }
            }
            return availableWalls;
        }

        public void RemoveWall(Wall wall)
        {
            AddGraphEdges(wall);
            wall.Type = WallType.None;
            game.CurrentPlayer.WallsCount += 1;
        }
    }
}
