using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDTO.Domain.Todo
{
    /// <summary>
    /// Запись из списка дел
    /// </summary>
    public class TodoItem
    {
        public TodoItem(string name, bool isComplete) : this(0, name, isComplete)
        {
        }

        public TodoItem(long id, string name, bool isComplete)
        {
            Id = id;
            Name = name;
            IsComplete = isComplete;
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public bool IsComplete { get; private set; }
    }
}
