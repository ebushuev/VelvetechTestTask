using System;
using System.Collections.Generic;
using System.Text;
using Todo.DAL.Models;

namespace Todo.BLL.DTO
{
    public abstract class BaseDTO<T> where T : BaseModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public bool IsDeleted { get; set; }

        protected BaseDTO(T obj)
        {
            if (obj != null)
            {
                Id = obj.Id;
                Created = obj.Created;
                Updated = obj.Updated;
                Deleted = obj.Deleted;
                IsDeleted = obj.Deleted.HasValue;
            }
        }
    }
}
