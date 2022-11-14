using System.ComponentModel.DataAnnotations;

namespace TodoApi.Application.Dto
{
    public class TodoItemDto
    {
        [Required]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(1024)]
        public string Name { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
