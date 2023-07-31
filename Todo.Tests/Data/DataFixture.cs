using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Todo.Core.Common;
using Todo.Core.Business.TodoItem.Interfaces;
using Todo.DataAccess.Repositories;
using Todo.Core.Business.TodoItem.Mappings;
using System.Reflection;
using Todo.Core.Business.TodoItem.Commands.Create;
using FluentValidation;
using Todo.Core.Business.TodoItem.Entities;

namespace Todo.Core.Tests.Data
{
    public class DataFixture
    {
        public DataFixture()
        {
            ServiceProvider = new ServiceCollection()
                .AddDbContext<TodoContext>(options =>
                {
                    options.UseInMemoryDatabase("TodoTestDb", b => b.EnableNullChecks(false));
                })
                .AddMemoryCache()
                .AddScoped<ITodoRepository, TodoRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddAutoMapper(typeof(TodoItemMappingProfile).GetTypeInfo().Assembly)
                .AddMediatR(c => c.RegisterServicesFromAssembly(typeof(CreateCommand).GetTypeInfo().Assembly))
                .AddValidatorsFromAssemblyContaining<CreateCommandValidator>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .BuildServiceProvider();
            GenerateData();
            UnitOfWork = (IUnitOfWork)ServiceProvider.GetService(typeof(IUnitOfWork));
            Mapper = (IMapper)ServiceProvider.GetService(typeof(IMapper));
            Mediator = (IMediator)ServiceProvider.GetService(typeof(IMediator));
            TodoRepository = (ITodoRepository)ServiceProvider.GetService(typeof(ITodoRepository));
        }

        public IServiceProvider ServiceProvider { get; }

        public IUnitOfWork UnitOfWork { get; }

        public IMapper Mapper { get; }

        public IMediator Mediator { get; }

        public ITodoRepository TodoRepository { get; }

        public IList<TodoItem> TodoItems { get; } = new List<TodoItem>();

        private void GenerateData()
        {
            GenerateTestTodo();
        }


        private void GenerateTestTodo()
        {
            var unitOfWork = (IUnitOfWork)ServiceProvider.GetService(typeof(IUnitOfWork));
            var userRepository = (ITodoRepository)ServiceProvider.GetService(typeof(ITodoRepository));

            TodoItems.Add(new TodoItem { Name = "TodoItem1", IsComplete = false });
            TodoItems.Add(new TodoItem { Name = "TodoItem2", IsComplete = false });

            userRepository.Add(TodoItems[0]);
            userRepository.Add(TodoItems[1]);

            unitOfWork.Commit();
        }
    }
}
