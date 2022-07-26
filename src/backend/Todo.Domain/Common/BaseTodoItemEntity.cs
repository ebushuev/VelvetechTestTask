namespace Todo.Domain.Common
{
    public class BaseTodoItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
