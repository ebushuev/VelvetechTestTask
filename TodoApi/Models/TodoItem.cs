using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    #region snippet
    public class TodoItem
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}