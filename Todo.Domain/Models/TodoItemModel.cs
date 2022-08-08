using System;

namespace Todo.Domain.Models
{
    public class TodoItemModel
    {
        public long? Id { get; private set; }
        public string Name { get; private set; }
        public bool IsComplete { get; private set; }

        public TodoItemModel(long? id, string name, bool isComplete = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{name} is empty");
            }

            Id = id;
            Name = name;
            IsComplete = isComplete;
        }

        public TodoItemModel UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{name} is empty");
            }

            Name = name;

            return this;
        }

        public TodoItemModel Complete()
        {
            IsComplete = true;

            return this;
        }

        public TodoItemModel UnComplete()
        {
            IsComplete = false;

            return this;
        }
    }
}
