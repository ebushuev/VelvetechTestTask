namespace TodoApi.DAL.Models
{
    public class TodoItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }
}