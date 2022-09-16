using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Velvetech.Todo.Repositories.Entities;
using Velvetech.Todo.Repositories.Interfaces;

namespace Velvetech.Todo.Repositories
{
  public class DbTodoItemsRepository : BaseRepository, IDbTodoItemsRepository
  {
    public DbTodoItemsRepository(DbConnectionSettings config) : base(config) { }

    public Task<DbTodoItem> InsertTodoItemAsync(DbTodoItem item)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@name", item.Name);
      parameters.Add("@secret", item.Secret);
      parameters.Add("@isComplete", item.IsComplete);

      return QuerySingle<DbTodoItem>(Consts.ProcedureNames.InsertTodoItem, parameters);
    }

    public Task DeleteTodoItemAsync(long id)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", id);

      return Execute(Consts.ProcedureNames.DeleteTodoItem, parameters);
    }

    public Task<IEnumerable<DbTodoItem>> GetAllTodoItemsAsync()
    {
      return Query<DbTodoItem>(Consts.ProcedureNames.GetAllTodoItems);
    }

    public Task<DbTodoItem> GetTodoItemByIdAsync(long id)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", id);

      return QuerySingle<DbTodoItem>(Consts.ProcedureNames.GetTodoItemById, parameters);
    }

    public Task<DbTodoItem> UpdateTodoItemAsync(DbTodoItem item)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", item.Id);
      parameters.Add("@name", item.Name);
      parameters.Add("@secret", item.Secret);
      parameters.Add("@isComplete", item.IsComplete);

      return QuerySingle<DbTodoItem>(Consts.ProcedureNames.UpdateTodoItem, parameters);
    }
  }
}
