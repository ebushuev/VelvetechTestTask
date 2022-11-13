using AutoFixture;
using Moq;
using TodoApi.Core.Services.Contract;
using TodoApi.Core.Services.Implementation;
using TodoApiRepository.Repositories.Contract;

namespace TodoApi.Tests.Services.TodoItemServiceTests
{
    public abstract class TodoItemServiceBase
    {
        protected readonly IFixture Fixture;
        protected readonly Mock<ITodoItemRepository> TodoItemRepositoryMock;
        protected readonly ITodoItemService TodoItemService;
        protected TodoItemServiceBase() 
        {
            Fixture = new Fixture();
            TodoItemRepositoryMock = new Mock<ITodoItemRepository>();
            TodoItemService = new TodoItemService(TodoItemRepositoryMock.Object);
        }
    }
}
