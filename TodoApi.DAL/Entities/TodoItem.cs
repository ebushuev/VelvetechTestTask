using System.ComponentModel.DataAnnotations;

namespace TodoApi.DAL.Entities
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}