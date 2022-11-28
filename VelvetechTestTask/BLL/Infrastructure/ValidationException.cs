using System;

namespace BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }

        public ValidationException(string message, string property, Exception innerException) : base(message, innerException)
        {
            Property = property;
        }

        public ValidationException(string message, string property) : base(message)
        {
            Property = property;
        }
    }
}
