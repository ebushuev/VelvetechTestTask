using TodoApiDTO.Components.TodoList.Models;
using TodoApiDTO.Interfaces;

namespace TodoApiDTO.Components.TodoList.Dto
{
    /// <summary>
    /// Dto для сущности TO-DO.
    /// </summary>
    public class TodoItemDto : IHaveId
    {
        /// <summary>
        /// Проекция сущности TO-DO в Dto.
        /// </summary>
        /// <param name="todoItem">Сущность TO-DO.</param>
        public static TodoItemDto ProjectionExpression(TodoItem todoItem) =>
            new TodoItemDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}