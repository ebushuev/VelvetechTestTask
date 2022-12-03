using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApiDTO.BL.Interfaces
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        Task<ActionResult<T>> Get(long id);
        Task<int> Update(long id, T itemDTO);
        Task<int> Create(T itemDTO);
        Task<int> Delete(long id);
        bool IsExists(long id);
    }
}
