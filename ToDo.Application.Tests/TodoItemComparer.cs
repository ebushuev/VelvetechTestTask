using System;
using System.Collections.Generic;
using ToDo.Domain.Models;

namespace ToDo.Application.Tests
{
    public class TodoItemComparer : IEqualityComparer<ToDoItem>
    {
        public bool Equals(ToDoItem x, ToDoItem y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Name == y.Name && x.IsComplete == y.IsComplete && x.Secret == y.Secret;
        }

        public int GetHashCode(ToDoItem obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.IsComplete, obj.Secret);
        }
    }
}