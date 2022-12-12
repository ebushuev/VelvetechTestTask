using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TodoApiDTO.Domain.Todo;

namespace TodoApiDTO.Application.Todo
{
    public class TodoItemDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }

        /// <summary>
        /// Конвертация данных из бд в модель
        /// </summary>
        public static Expression<Func<TodoItem, TodoItemDto>> Expression = item => GetDto(item);

        public static Func<TodoItem, TodoItemDto> GetDto =
            item => new TodoItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
    }
}
