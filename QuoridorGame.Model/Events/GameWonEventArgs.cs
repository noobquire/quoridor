namespace QuoridorGame.Model.Events
{
    public class GameWonEventArgs
    {
        public int PlayerNumber { get; }

        public GameWonEventArgs(int playerNumber)
        {
            PlayerNumber = playerNumber;
        }
    }
}
