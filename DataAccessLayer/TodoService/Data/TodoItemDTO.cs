using System;

namespace TodoApi.Models
{
    #region snippet
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TodoItemDTO todoItemDTO = (TodoItemDTO)obj;
                return (Id == todoItemDTO.Id) && (Name == todoItemDTO.Name);
            }
        }
    }
    #endregion
}
