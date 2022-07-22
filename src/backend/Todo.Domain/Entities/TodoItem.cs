using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class TodoItem : BaseTodoItemEntity
    {
        public string Secret { get; set; }
    }
}
