using System;
using Todo.DAL.Models;

namespace Todo.BLL.DTO
{
    #region snippet
    public class TodoItemDTO : BaseDTO<MTodoItem>
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public TodoItemDTO(MTodoItem obj) : base(obj)
        {
            if (obj != null)
            {
                Name = obj.Name;
                IsComplete = obj.IsComplete;
            }
        }
    }
    #endregion
}
