using System;

namespace QuoridorGame.View.ConsoleGraphics
{
    internal class ColorAttribute : Attribute
    {
        public ConsoleColor Value { get; }

        public ColorAttribute(ConsoleColor color)
        {
            Value = color;
        }
    }
}
