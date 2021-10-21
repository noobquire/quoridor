using System;

namespace QuoridorGame.View
{
    internal class DisplayAttribute : Attribute
    {
        public string Value { get; }

        public DisplayAttribute(string view)
        {
            Value = view;
        }
    }
}
