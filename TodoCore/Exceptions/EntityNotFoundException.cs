using System;
using TodoCore.Data.Common;

namespace TodoCore.Exceptions
{
    public class EntityNotFoundException<TEnt> : Exception
        where TEnt : BaseEntity
    {
        public EntityNotFoundException(string message) : base($"{typeof(TEnt).Name} {message}")
        {
        }
    }
}
