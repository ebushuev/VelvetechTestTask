using System;

namespace Todo.Domain.Exceptions
{
    public class BadRequestException: BusinessException
    {
        public override int StatusCode => 400;

        public BadRequestException(string message) : base(message)
        {
        }
    }
}