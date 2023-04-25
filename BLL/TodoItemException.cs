using System;

namespace TodoApiDTO.BLL
{
    public class TodoItemException:Exception 
    {
        public TodoItemException() { }

        public TodoItemException(string message)
            : base(message) { }

        public TodoItemException(string message, Exception inner)
            : base(message, inner) { }
    }
}
