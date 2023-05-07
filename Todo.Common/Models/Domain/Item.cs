using System;

namespace Todo.Common.Models.Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
