namespace QuoridorGame.View.Bot
{
    public interface IBot
    {
        void ChoosePlayer(int playerNumber);
        int PlayerNumber { get; }
        void MakeTurn(Model.Entities.QuoridorGame game);
    }
}
