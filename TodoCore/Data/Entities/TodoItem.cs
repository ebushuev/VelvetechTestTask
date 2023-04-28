using System.ComponentModel.DataAnnotations;
using TodoCore.Data.Common;

namespace TodoCore.Data.Entities
{
    public class TodoItem : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
    }
}
