namespace QuoridorGame.Model.Entities
{
    public enum WallType
    {
        None,
        Vertical, 
        Horizontal
    }

    public class Wall
    {
        public WallType Type { get; set; }

        public Wall()
        {
            this.Type = WallType.None;
        }
    }
}