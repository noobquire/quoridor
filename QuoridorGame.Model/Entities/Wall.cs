using System;

namespace QuoridorGame.Model.Entities
{
    public enum WallType
    {
        None,
        Vertical, 
        Horizontal
    }

    [Serializable]
    public class Wall
    {
        public int X { get; }
        public int Y { get; }
         
        public WallType Type { get; set; }

        public Wall(int x, int y)
        {
            X = x;
            Y = y;
            Type = WallType.None;
        }
    }
}
