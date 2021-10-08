using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Exceptions;
using  QuoridorGame.Model.Entities;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Model.Logic
{
    public class WallPlacer : IWallPlacer
    {
        private readonly Game game;
        private readonly IPathFinder<CellField, Cell> pathfinder;
        public WallPlacer(Game game, IPathFinder<CellField, Cell> pathfinder)
        {
            this.game = game;
            this.pathfinder = pathfinder;
        }

        public void PlaceWall(WallType walltype, int x, int y)
        {
            WallIsPlaceable(walltype, x, y);
            game.GameField.Walls.Grid[x, y].Type = walltype;

        }
        private void WallIsPlaceable(WallType walltype, int x, int y)
        {
            if (x >= WallsGrid.GridSize || y >= WallsGrid.GridSize)
            {
                throw new QuoridorGameException("WallsGrid index is out of bounds.");
            }
            if (game.GameField.Walls.Grid[x, y].Type != WallType.None) 
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
                var up_neigh = x == 0 ? false : (game.GameField.Walls.Grid[x - 1, y].Type == WallType.Vertical);
                var low_neigh = x == WallsGrid.GridSize - 1 ? false : (game.GameField.Walls.Grid[x + 1, y].Type == WallType.Vertical);

                if (up_neigh || low_neigh)
                {
                    throw new QuoridorGameException( "Position is blocked by another vertical wall.");
                }
            }
            else
            {
                // Check if no other horizontal walls in adjacent positions
                var left_neigh = y == 0 ? false : (game.GameField.Walls.Grid[x, y - 1].Type == WallType.Horizontal);
                var right_neigh = y == WallsGrid.GridSize - 1 ? false : (game.GameField.Walls.Grid[x, y+1].Type == WallType.Horizontal);

                if (left_neigh || right_neigh)
                {
                    throw new QuoridorGameException("Position is blocked by another horizontal wall.");
                }

            }

            //Check if player is not trapped
            if (!PathExists()){
                throw new QuoridorGameException("Can't block plauer with a wall.");
            }
            
            // Wall may be placed if no exeptions occurred
        }

        private bool PathExists()
        {
            //var revState = game.GameField.Cells.MemberwiseClone();// Make Deepcopy method
            //game.GameField.Cells[0,0].AdjacentNodes = game.GameField.Cells[0,0].AdjacentNodes.Where(cell => cell != removedCell)
            //#removedCell = game.GameField.Cells[0,1]
            //game.GameField.Cells[0,1].AdjacentNodes = game.GameField.Cells[0,1].AdjacentNodes.Where(cell => cell != removedCell)
            //#removedCell = game.GameField.Cells[0,0]

            Cell firstPlayerCell = game.FirstPlayer.CurrentCell;
            Cell secondPlayerCell = game.SecondPlayer.CurrentCell;
            bool firstPlayerReachable = false;
            bool secondPlayerReachable = false;


            for (int i = 0; i < CellField.FieldSize; i++)
            {
                if (pathfinder.PathExists(firstPlayerCell, game.GameField.Cells[0, i]))
                {
                    firstPlayerReachable = true;
                    break;
                }
            }

            for (int i = 0; i < CellField.FieldSize; i++)
            {
                if (pathfinder.PathExists(firstPlayerCell, game.GameField.Cells[CellField.FieldSize - 1, i]))
                {
                    secondPlayerReachable = true;
                    break;
                }
            }

            return firstPlayerReachable && secondPlayerReachable;

        }
    }
}