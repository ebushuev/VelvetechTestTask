using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public override string Message =>  $"Item with id {Id} is not found";
    }
}
