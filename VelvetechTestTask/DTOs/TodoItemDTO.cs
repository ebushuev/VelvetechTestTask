using System.ComponentModel.DataAnnotations;

namespace TodoApiDTO.DTOs
{
  #region snippet
  public class TodoItemDTO
  {
    public long Id { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Task can not be short than 1 letter")]
    public string Name { get; set; }
    public bool IsComplete { get; set; } = false;
  }
  #endregion
}
