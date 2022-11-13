using TodoApi.Core.DTOs;
using TodoApi.Core.Requests;
using TodoApiRepository.Models;

namespace TodoApi.Core.Mappers
{
    public static class TodoItemMappers
    {
        /// <summary>
        /// Map Todo item to Todo Dto
        /// </summary>
        /// <param name="todoItem">Todo item</param>
        /// <returns></returns>
        public static TodoItemDTO ItemToDTO(this TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        /// <summary>
        /// Map Todo args to Todo item
        /// </summary>
        /// <param name="todoItemArgs">Todo args</param>
        /// <returns></returns>
        public static TodoItem ArgsToItem(this TodoItemArgs todoItemArgs) =>
            new TodoItem
            {
                Name = todoItemArgs.Name,
                IsComplete = todoItemArgs.IsComplete
            };
    }
}
