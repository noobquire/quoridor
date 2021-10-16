namespace QuoridorGame.Model.Interfaces
{
    /// <summary>
    /// Facade used to access game logic.
    /// </summary>
    public interface IQuoridorGameFacade
    {
        IMovementLogic MovementLogic { get; }
        IWallPlacer WallPlacer { get; }
    }
}
