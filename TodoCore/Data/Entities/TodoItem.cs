using TodoCore.Data.Common;

namespace TodoCore.Data.Entities
{
    public class TodoItem : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
