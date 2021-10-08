using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Exceptions;
using  QuoridorGame.Model.Entities;

namespace QuoridorGame.Model.Logic
{
    public class WallPlacer : IWallPlacer
    {
        private readonly GameField gamefield;
        private readonly IPathFinder<CellField, Cell> pathfinder;
        public WallPlacer(GameField gamefield, IPathFinder<CellField, Cell> pathfinder)
        {
            this.gamefield = gamefield;
            this.pathfinder = pathfinder;
        }

        public void PlaceWall(WallType walltype, int x, int y)
        {
            try
            {
                WallIsPlaceable(walltype, x, y);
            }
            catch(QuoridorGameException ex)
            {
                throw ex;
            }
            gamefield.Walls.Grid[x, y].Type = walltype;

        }
        private void WallIsPlaceable(WallType walltype, int x, int y)
        {
            if (x >= WallsGrid.GridSize || y >= WallsGrid.GridSize)
            {
                throw new QuoridorGameException("WallsGrid index is out of bounds.");
            }
            if (gamefield.Walls.Grid[x, y].Type != WallType.None) 
            {
                throw new QuoridorGameException( "Position already taken by other wall.");
            }
            if (walltype == WallType.None)
            {
                throw new QuoridorGameException( "WallType can not be WallType:None.");
            }
            if (walltype == WallType.Vertical)
            {
                // Check if no other vertival walls in adjacent positions
                var up_neigh = x == 0 ? false : (gamefield.Walls.Grid[x - 1, y].Type == WallType.Vertical);
                var low_neigh = x == WallsGrid.GridSize - 1 ? false : (gamefield.Walls.Grid[x + 1, y].Type == WallType.Vertical);

                if (up_neigh || low_neigh)
                {
                    throw new QuoridorGameException( "Position is blocked by another vertical wall.");
                }
            }
            else
            {
                // Check if no other horizontal walls in adjacent positions
                var left_neigh = y == 0 ? false : (gamefield.Walls.Grid[x, y - 1].Type == WallType.Horizontal);
                var right_neigh = y == WallsGrid.GridSize - 1 ? false : (gamefield.Walls.Grid[x, y+1].Type == WallType.Horizontal);

                if (left_neigh || right_neigh)
                {
                    throw new QuoridorGameException("Position is blocked by another horizontal wall.");
                }

            }

            //TODO: check if player is not trapped 

            // Wall may be placed if no exeptions occurred
        }
    }
}