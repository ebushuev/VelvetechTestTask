using System.ComponentModel.DataAnnotations;

namespace TodoApi.Services.Models
{
    #region snippet
    /// <summary>
    /// TodoItem DataTransferObject. Id ignored on Save or Update. Name is required.
    /// </summary>
    public class TodoItemDTO
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
    #endregion
}
