using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApiDTO.DAL.Entities;

[Table(nameof(TodoItemEntity), Schema = "test")]
public class TodoItemEntity
{
    [Key]
    [ConcurrencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsComplete { get; set; }
    public string Secret { get; set; }
}
