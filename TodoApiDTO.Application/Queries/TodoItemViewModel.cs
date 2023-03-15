using System;
using System.Collections.Generic;
using System.Text;
using TodoApiDTO.Domain;

namespace TodoApiDTO.Application.Queries
{
    public class TodoItemViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public static TodoItemViewModel MapFrom(TodoItem item)
        {
            return new TodoItemViewModel
            { 
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }
    }
}
