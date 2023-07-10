using System;

namespace Todo.Domain.Exceptions
{
    public class NotFoundException: BusinessException
    {
        public override int StatusCode => 404;

        public NotFoundException(string message) : base(message)
        {
        }
    }
}