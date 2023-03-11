using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApiDTO.Models
{
    public enum TodoItemActionResult: byte
    {
        Success = 0,
        Failed = 1,
        NotFound = 2
    }
}
