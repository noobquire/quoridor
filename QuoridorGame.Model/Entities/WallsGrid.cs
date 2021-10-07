namespace QuoridorGame.Model.Entities
{
    public enum WallType
    {
        None = 0,
        Vertical = 1, 
        Horizontal = 2
    }
    public class WallsGrid
    {
        public const int GridSize = 8;
        private int[][] Grid;

        public WallsGrid()
        {
            Grid = new int[GridSize][];
            for (int i = 0; i < GridSize; i++)
            {
                Grid[i] = new int[GridSize];
            }
        }

        public (bool, string) WallIsPlaceable(WallType walltype, int x, int y)
        {
            if (x >= GridSize || y >= GridSize)
            {
                return (false, "WallsGrid index is out of bounds.");
            }

            if (walltype == WallType.None)
            {
                return (false, "WallType can not be WallType:None.");
            }

            else if (walltype == WallType.Vertical)
            {
                // Check if no other vertival walls in adjacent positions
                var up_neigh = x == 0 ? false : (Grid[x - 1][y] == 1);
                var low_neigh = x == GridSize - 1 ? false : (Grid[x + 1][y] == 1);

                if (up_neigh || low_neigh)
                {
                    return (false, "Position is blocked by another vertical wall.");
                }
            }
            else
            {
                // Check if no other horizontal walls in adjacent positions
                var left_neigh = y == 0 ? false : (Grid[x][y - 1] == 2);
                var right_neigh = y == GridSize -1 ? false : (Grid[x][y+1] == 2);

                if (left_neigh || right_neigh)
                {
                    return (false, "Position is blocked by another vertical wall.")
                }

            }

            //TODO: check if player is not trapped 

            // Wall may be placed if no exeptions occurred
            return (true, "")
        }
    }
}
