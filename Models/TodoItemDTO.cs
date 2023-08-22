using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    #region snippet
    public class TodoItemDTO
    {
        /// <summary>Todo's Id</summary>
        public long? Id { get; set; }

        /// <summary>Todo's name</summary>
        [Required]
        public string Name { get; set; } = "";

        /// <summary>Completion condition of this todo</summary>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }

        public static TodoItemDTO From(TodoItem todoItem) 
        {
            return new TodoItemDTO 
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        }

        public TodoItem ToTodoItem() 
        {
            return new TodoItem
            {
                IsComplete = IsComplete,
                Name = Name
            };
        }
    }
    #endregion
}
