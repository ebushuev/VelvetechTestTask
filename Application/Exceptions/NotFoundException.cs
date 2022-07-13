using System;

namespace TodoApiDTO.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : this(string.Empty) { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}
