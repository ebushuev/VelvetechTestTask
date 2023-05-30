using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Models;

namespace TodoTests
{
    class TodoTestDataHelper
    {
        internal static List<TodoItem> GetTodoItemsList()
        {
            return new List<TodoItem>()
            {
                new TodoItem
                {
                    Id = 0,
                    Name = "First thing to do",
                    IsComplete = true,
                    Secret = "secretString"
                },
                new TodoItem
                {
                    Id = 1,
                    Name = "To do something",
                    IsComplete = true,
                    Secret = "secretString"
                },
                new TodoItem
                {
                    Id = 2,
                    Name = "To do something else",
                    IsComplete = false,
                    Secret = "secretString"
                }
            };
        }
    }
}
