using QuoridorGame.Model.Entities;
using System.Collections.Generic;

namespace QuoridorGame.Model.Interfaces
{
    public interface IMovementLogic
    {
        void MovePlayer(Player player, Cell destination);
        void RollbackPlayerMove(Player player, Cell to);
        bool IsOnEnemySide(Player player);
        IEnumerable<Cell> GetAvailableMoves(Cell from);
        IEnumerable<Cell> GetAvailableJumps(Cell from);
    }
}
