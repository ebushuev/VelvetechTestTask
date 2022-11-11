using TodoApiDTO.DAL.Entities.Abstractions;

namespace TodoApiDTO.DAL.Entities
{
    public class TodoItem : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }
}
