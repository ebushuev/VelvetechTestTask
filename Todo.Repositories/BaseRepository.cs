using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Velvetech.Todo.Repositories
{
  public abstract class BaseRepository
  {
    private readonly DbConnectionSettings _config;

    protected BaseRepository(DbConnectionSettings config)
    {
      _config = config;
    }

    protected async Task<IEnumerable<T>> Query<T>(string procedureName, object parameter)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        return await db.QueryAsync<T>(procedureName, parameter, commandType: CommandType.StoredProcedure, commandTimeout: _config.CommandTimeout);
      }
    }

    protected async Task<IEnumerable<T>> Query<T>(string procedureName)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        return await db.QueryAsync<T>(procedureName, commandType: CommandType.StoredProcedure, commandTimeout: _config.CommandTimeout);
      }
    }

    protected async Task<T> QuerySingle<T>(string procedureName, object parameter)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        return await db.QuerySingleOrDefaultAsync<T>(procedureName, parameter,
            commandType: CommandType.StoredProcedure, commandTimeout: _config.CommandTimeout);
      }
    }

    protected async Task<T> QuerySingle<T>(string procedureName)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        return await db.QuerySingleOrDefaultAsync<T>(procedureName,
            commandType: CommandType.StoredProcedure, commandTimeout: _config.CommandTimeout);
      }
    }

    protected async Task Execute(string procedureName, object parameter)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        await db.ExecuteAsync(procedureName, parameter, commandType: CommandType.StoredProcedure, commandTimeout: _config.CommandTimeout);
      }
    }

    protected async Task<T> ExecuteFunction<T>(string sql, object parameter)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        return await db.ExecuteScalarAsync<T>(sql, parameter, commandTimeout: _config.CommandTimeout);
      }
    }
  }
}
