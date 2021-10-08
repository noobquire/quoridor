namespace QuoridorGame.Model.Entities
{
    public class WallsGrid
    {
        public const int GridSize = 8;
        public Wall[,] Grid;

        public WallsGrid()
        {
           Grid = new Wall[GridSize, GridSize];
           for (int i = 0; i < GridSize; i++) 
           {
                for (int j = 0; j < GridSize; j++)
                {
                    Grid[i, j] = new Wall();
                }
            }
        }
    }
}
