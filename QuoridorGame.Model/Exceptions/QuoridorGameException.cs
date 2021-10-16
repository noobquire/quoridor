using System;

namespace QuoridorGame.Model.Exceptions
{
    public class QuoridorGameException : Exception
    {
        public QuoridorGameException() : base()
        {

        }

        public QuoridorGameException(string message) : base(message)
        {

        }
    }
}
