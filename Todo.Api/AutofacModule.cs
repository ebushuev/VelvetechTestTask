using System;
using Autofac;
using Velvetech.Todo.Logic;
using Velvetech.Todo.Repositories;
using Velvetech.Todo.Repositories.Interfaces;

namespace Velvetech.Todo.Api
{
  public class AutofacModule : Module
  {
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

    public AutofacModule(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(c => new DbConnectionSettings
      {
        ConnectionString = _configuration["Database:ConnectionString"],
        CommandTimeout = Convert.ToInt32(_configuration["Database:CommandTimeout"])
      }).As<DbConnectionSettings>().SingleInstance();

      builder.RegisterType<DbTodoItemsRepository>().As<IDbTodoItemsRepository>().InstancePerLifetimeScope();
      builder.RegisterType<TodoItemService>().As<ITodoItemService>().InstancePerLifetimeScope();
    }
  }
}
