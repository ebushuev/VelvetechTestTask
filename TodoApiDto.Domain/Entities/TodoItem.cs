using System;
using TodoApiDto.Domain.Interfaces;

namespace TodoApiDto.Domain.Entities
{
    public class TodoItem: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}
