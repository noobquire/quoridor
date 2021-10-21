using System;

namespace QuoridorGame.View
{
    public class ViewCell
    {
        public ViewCellType Type { get; set; }

        public ViewCell(ViewCellType type)
        {
            Type = type;
        }

        public ConsoleColor Color => Type.GetAttribute<ColorAttribute>().Value;
        public string Display => Type.GetAttribute<DisplayAttribute>().Value;
    }
}
