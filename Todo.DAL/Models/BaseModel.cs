using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Todo.DAL.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public bool IsDeleted => Deleted.HasValue;

        protected BaseModel()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }

        protected BaseModel(Guid id)
        {
            Id = id;
            Created = DateTime.Now;
        }
    }
}
