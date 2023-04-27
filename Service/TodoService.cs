using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using TodoApiDTO.MapperProfiles;
using TodoApiDTO.Models;
using TodoApiDTO.ServiceInterfaces;
using TodoApiDTO.ServiceInterfaces.DatabaseWrappers;

namespace TodoApiDTO.Service;

public class TodoService : ITodoService
{
    private readonly IDataBase _postgres;
    private readonly IMapper _mapper;

    public TodoService(IHostEnvironment env,
        IDataBase postgres)
    {
        _postgres = postgres;
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = true;
            cfg.AllowNullDestinationValues = true;
            cfg.AddProfile(typeof(TodoItemProfile));
        });
        
        if (env.IsDevelopment())
        {
            config.CompileMappings();
            config.AssertConfigurationIsValid();
        }

        _mapper = new Mapper(config);
    }

    public async Task<TodoItemModel?> GetAsync(long id, CancellationToken token)
    {
        var entity = await _postgres.GetAsync(id, token);
        
        if (entity == null)
        {
            return null;
        }
        
        var result = _mapper.Map<TodoItemModel>(entity);

        return result;
    }

    public async Task<List<TodoItemModel>> GetListAsync(CancellationToken token)
    {
        var entities = await _postgres.GetListAsync(token);
        
        var result = _mapper.Map<List<TodoItemModel>>(entities);
        

        return result;
    }

    public async Task<TodoItemModel?> CreateAsync(TodoItemCreateModel model, CancellationToken token)
    {
        var entity = await _postgres.CreateAsync(model, token);
        
        if (entity == null)
        {
            return null;
        }
        
        var result = _mapper.Map<TodoItemModel>(entity);

        return result;
    }

    public async Task<TodoItemModel?> SetCompleted(long id, CancellationToken token)
    {
        var isExecuted = await _postgres.SetCompleted(id, token);
        
        return await GetAsync(id, token);
    }

    public async Task<TodoItemModel?> UpdateAsync(TodoItemUpdateModel model, CancellationToken token)
    {
        var isExecuted = await _postgres.UpdateAsync(model, token);
        
        
        return await GetAsync(model.Id, token);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken token)
    {
        var isExecuted = await _postgres.DeleteAsync(id, token);

        return isExecuted;
    }
}