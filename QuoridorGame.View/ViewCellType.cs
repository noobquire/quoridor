using System;

namespace QuoridorGame.View
{
    public enum ViewCellType
    {
        [Display("████")]
        [Color(ConsoleColor.White)]
        EmptyCell,
        [Display("▓▓")]
        [Color(ConsoleColor.DarkGray)]
        Wall,
        [Display("▓▓▓▓")]
        [Color(ConsoleColor.DarkGray)]
        WallUnderCell,
        [Display("  ")]
        [Color(ConsoleColor.Black)]
        NoWall,
        [Display("    ")]
        [Color(ConsoleColor.Black)]
        NoWallUnderCell,
        [Display("████")]
        [Color(ConsoleColor.Red)]
        FirstPlayer,
        [Display("████")]
        [Color(ConsoleColor.Blue)]
        SecondPlayer,
        [Display("████")]
        [Color(ConsoleColor.Green)]
        AvailableMoveCell
    }
}
