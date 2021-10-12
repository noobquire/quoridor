using QuoridorGame.Model.Interfaces;

namespace QuoridorGame.Model.Logic
{
    public class QuoridorGameFacade : IQuoridorGameFacade
    {
        public IMovementLogic MovementLogic { get; init; }

        public IWallPlacer WallPlacer { get; init; }

        public QuoridorGameFacade(IMovementLogic movementLogic, IWallPlacer wallPlacer)
        {
            MovementLogic = movementLogic;
            WallPlacer = wallPlacer;
        }
    }
}
