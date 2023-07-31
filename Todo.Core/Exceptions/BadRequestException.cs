using System.Runtime.Serialization;

namespace Todo.Core.Exceptions
{
    public class BadRequestException : BusinessException
    {
        public IDictionary<string, IEnumerable<string>> Errors { get; }

        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, IDictionary<string, IEnumerable<string>> errors) : base(message)
        {
            Errors = errors;
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
