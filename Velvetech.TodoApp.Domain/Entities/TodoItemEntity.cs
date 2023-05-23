using System.ComponentModel.DataAnnotations.Schema;

namespace Velvetech.TodoApp.Domain.Entities
{
    [Table("TodoItem")]
    public class TodoItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}