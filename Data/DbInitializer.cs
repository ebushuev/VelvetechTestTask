using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApiDTO.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TodoContext context)
        {
            context.Database.EnsureCreated();

            if (context.TodoItems.Any())
            {
                return;
            }

            context.TodoItems.AddRange(new List<TodoItem> {
                new TodoItem{ IsComplete = true, Name = "TodoItem1" },
                new TodoItem{ IsComplete = false, Name = "TodoItem2" },
                new TodoItem{ IsComplete = true, Name = "TodoItem3" },
                new TodoItem{ IsComplete = false, Name = "TodoItem4" }
            });

            context.SaveChanges();
        }
    }
}
