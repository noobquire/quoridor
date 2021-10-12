using QuoridorGame.Model.Entities;
using System.Collections.Generic;

namespace QuoridorGame.Model.Interfaces
{
    public interface IMovementLogic
    {
        void MovePlayer(Player player, Cell destination);
        IEnumerable<Cell> GetAvailableMoves(Cell from);
    }
}
