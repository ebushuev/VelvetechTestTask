using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDTO.Domain
{
    public class TodoItem
    {
        internal TodoItem(long id, string name, bool isComplete, string secret)
        {
            Id = id;
            Update(name, isComplete);
            SetSecret(secret);
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public bool IsComplete { get; private set; }
        public string Secret { get; private set; }

        public void Update(string name, bool isComplete)
        {
            Name = name ?? throw new ValidationException(nameof(name));
            IsComplete = isComplete;
        }

        public void SetSecret(string secret)
        {
            Secret = secret;
        }

        public static TodoItem Create(string name, bool isComplete, string secret = null)
        {
            return new TodoItem(default, name, isComplete, secret);
        }
    }
}
