using QuoridorGame.Model.Exceptions; // Когда логику вынесу, убрать
namespace QuoridorGame.Model.Entities
{
    public class WallsGrid
    {
        public const int GridSize = 8;
        public Wall[,] Grid;

        public WallsGrid()
        {
           Grid = new Wall[GridSize, GridSize];
        }
        public void WallIsPlaceable(WallType walltype, int x, int y)
        {
            if (x >= GridSize || y >= GridSize)
            {
                throw new QuoridorGameException("WallsGrid index is out of bounds.");
            }
            if (Grid[x, y].Type != WallType.None) 
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
                var up_neigh = x == 0 ? false : (Grid[x - 1, y].Type == WallType.Vertical);
                var low_neigh = x == GridSize - 1 ? false : (Grid[x + 1, y].Type == WallType.Vertical);

                if (up_neigh || low_neigh)
                {
                    throw new QuoridorGameException( "Position is blocked by another vertical wall.");
                }
            }
            else
            {
                // Check if no other horizontal walls in adjacent positions
                var left_neigh = y == 0 ? false : (Grid[x, y - 1].Type == WallType.Horizontal);
                var right_neigh = y == GridSize - 1 ? false : (Grid[x, y+1].Type == WallType.Horizontal);

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
