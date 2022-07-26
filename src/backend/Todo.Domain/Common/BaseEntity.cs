using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Todo.Domain.Abstractions;

namespace Todo.Domain.Common
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public long Id { get; set; }
    }

    public class BaseGuidEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((BaseGuidEntity)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private bool Equals(BaseGuidEntity other)
        {
            return Id == other.Id;
        }
    }
}
