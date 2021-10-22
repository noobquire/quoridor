using System;

namespace QuoridorGame.Model.Events
{
    public class PlayerMovedEventArgs : EventArgs
    {
        public int PlayerNumber { get; }
        public int X { get; }
        public int Y { get; }
        public PlayerMovedEventArgs(int playerNumber, int x, int y)
        {
            PlayerNumber = playerNumber;
            X = x;
            Y = y;
        }
    }
}
