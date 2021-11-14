using System;

namespace QuoridorGame.Model.Entities
{
    [Serializable]
    public enum GameState
    {
        Pregame,
        FirstPlayerTurn,
        SecondPlayerTurn,
        FirstPlayerWin,
        SecondPlayerWin
    }
}
