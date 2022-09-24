using System;

namespace TodoApiDTO.Services
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
    }
}
