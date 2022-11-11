using TodoApiDTO.DAL.Entities.Abstractions;

namespace TodoApiDTO.BLL.Services.Abstractions
{
    public interface IBaseService<TEntity> where TEntity : IBaseEntity
    {
    }
}
