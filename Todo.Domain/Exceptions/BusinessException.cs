using System;

namespace Todo.Domain.Exceptions
{
    public abstract class BusinessException : Exception
    {
        public abstract int StatusCode { get; }

        protected BusinessException(string message) : base(message)
        {
        }
    }
}