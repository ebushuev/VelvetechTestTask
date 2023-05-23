using System;
using System.ComponentModel.DataAnnotations;

namespace Velvetech.TodoApp.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
