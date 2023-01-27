using System;

namespace Domain
{
    #region snippet
    public class TodoItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
    #endregion
}
