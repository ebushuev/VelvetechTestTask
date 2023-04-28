using System;

namespace TodoCore.Exceptions
{
    public class SomethingWentWrongException : Exception
    {
        public SomethingWentWrongException(string message) : base(message) { }
    }
}
