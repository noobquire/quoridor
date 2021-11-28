namespace QuoridorGame.Model.Entities
{
    public class Turn
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int PlayerNumber { get; set; }
        public GameState StateBefore { get; set; }
    }
}
