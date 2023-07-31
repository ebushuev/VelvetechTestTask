using Todo.Core.Common;

namespace Todo.Core.Business.TodoItem.Entities
{
    public class TodoItem: BaseEntity<Guid>
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}