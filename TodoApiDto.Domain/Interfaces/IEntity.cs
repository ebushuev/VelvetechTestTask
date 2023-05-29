using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDto.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id
        {
            get; set;
        }
    }
}
