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
        public WallType Type { get; set; }

        public Wall()
        {
            Type = WallType.None;
        }
    }
}
