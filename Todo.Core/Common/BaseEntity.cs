using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Common
{
    public abstract class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}