namespace QuoridorGame.Model.Events
{
    public class NextTurnEventArgs
    {
        public int PlayerNumber { get; }
        public NextTurnEventArgs(int playerNumber)
        {
            PlayerNumber = playerNumber;
        }
    }
}
