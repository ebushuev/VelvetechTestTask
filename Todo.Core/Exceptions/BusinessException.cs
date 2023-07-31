using System;
using System.Runtime.Serialization;

namespace Todo.Core.Exceptions
{
    public abstract class BusinessException : Exception
    {
        protected BusinessException()
        {
        }

        protected BusinessException(string message) : base(message)
        {
        }

        protected BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
