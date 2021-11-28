using System;

namespace QuoridorGame.Model.Entities
{
    public class WallsGrid
    {
        public const int GridSize = 8;
        public Wall[,] Grid { get; set; }

        public WallsGrid()
        {
           Grid = new Wall[GridSize, GridSize];
           for (int i = 0; i < GridSize; i++) 
           {
                for (int j = 0; j < GridSize; j++)
                {
                    Grid[i, j] = new Wall(i, j);
                }
            }
        }
        public Wall this[int x, int y]
        {
            get { return Grid[x, y]; }
            set { Grid[x, y] = value; }
        }
    }
}
