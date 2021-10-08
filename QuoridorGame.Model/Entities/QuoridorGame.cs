namespace QuoridorGame.Model.Entities
{
    public class QuoridorGame
    {
        public GameField GameField { get; }
        public Player FirstPlayer { get; }
        public Player SecondPlayer { get; }

        public QuoridorGame()
        {
            GameField = new GameField();
            FirstPlayer = new Player(GameField.Nodes[0, 4]);
            SecondPlayer = new Player(GameField.Nodes[8, 4]);
        }

        public void Move(Cell destination)
        {

        }

        public void SetWall(WallType wallType, int x, int y)
        {

        }
    }
}
