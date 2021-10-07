namespace QuoridorGame.Model
{
    public class Player
    {
        public Cell CurrentCell { get; set; }
        public int WallsCount { get; set; } = 10;

        public Player(Cell startCell)
        {
            CurrentCell = startCell;
        }
    }
}