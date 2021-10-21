namespace QuoridorGame.Model.Events
{
    public class FieldUpdatedEventArgs
    {
        public UpdateType Type { get; }
        public Entities.WallType WallType { get; }
        public int PlayerNumber { get; }
        public int X { get; }
        public int Y { get; }
        public FieldUpdatedEventArgs(UpdateType type, int playerNumber, int x, int y, Entities.WallType wallType = Entities.WallType.None)
        {
            Type = type;
            PlayerNumber = playerNumber;
            X = x;
            Y = y;
            WallType = wallType;
        }
    }

    public enum UpdateType
    {
        Move,
        Wall
    }
}