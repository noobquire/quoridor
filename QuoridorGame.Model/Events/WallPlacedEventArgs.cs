using System;

namespace QuoridorGame.Model.Events
{
    public class WallPlacedEventArgs : EventArgs
    {
        public Entities.WallType WallType { get; }
        public int X { get; }
        public int Y { get; }
        public int WallsLeft { get; }
        public int PlayerNumber { get; }
        public WallPlacedEventArgs(Entities.WallType wallType, int x, int y, int wallsLeft, int playerNumber)
        {
            WallType = wallType;
            X = x;
            Y = y;
            WallsLeft = wallsLeft;
            PlayerNumber = playerNumber;
        }
    }
}
