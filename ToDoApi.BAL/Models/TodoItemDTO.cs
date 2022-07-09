using TodoApi.DAL.Models;

namespace TodoApi.Models
{
    #region snippet
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        /// <summary>
        /// creates new todoItemDTO based on todoItem
        /// </summary>
        /// <param name="toDoItem"></param>
        public TodoItemDTO(TodoItem toDoItem)
        {
            Id = toDoItem.Id;
            Name = toDoItem.Name;
            IsComplete = toDoItem.IsComplete;
        }
    }
    #endregion
}
