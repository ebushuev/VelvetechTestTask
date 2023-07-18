using TodoApiDTO.Repositories.Entities;

namespace TodoApiDTO.IToDoServices
{
    public interface IToDoService
    {
        IToDoRepository _todoRepository { get; }
        void Save();
    }
}
