using System;

namespace Business.Exceptions
{
    public class NoContentException : Exception
    {
        public NoContentException()
            : base()
        {
        }

        public NoContentException(string message)
            : base(message)
        {
        }
    }
}