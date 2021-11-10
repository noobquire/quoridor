namespace QuoridorGame.View.Bot
{
    public interface IBot
    {
        void ChoosePlayer(int playerNumber);
        int PlayerNumber { get; }
        void MakeMove(Model.Entities.QuoridorGame game);
    }
}