﻿

namespace BusinessLayer.DTOs
{
    #region snippet
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class CreateOrUpdateTodoItemDTO
    {
 
        public string Name { get; set; }
        public bool IsComplete { get; set; }

    }

    #endregion
}
