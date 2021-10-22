using System;

namespace QuoridorGame.Model.Events
{
    public class GameStartedEventArgs : EventArgs
    {
        public bool Restart { get; }

        public GameStartedEventArgs(bool restart = false)
        {
            Restart = restart;
        }
    }
}
