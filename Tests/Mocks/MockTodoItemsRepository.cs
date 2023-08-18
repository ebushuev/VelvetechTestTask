using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Moq;

namespace Tests.Mocks;

internal class MockTodoItemsRepository
{
    public static Mock<ITodoItemsRepository> GetMock()
    {
        var mock = new Mock<ITodoItemsRepository>();
        IEnumerable<TodoItem> todoItems = new List<TodoItem>
        {
            new TodoItem
            {
                Id = 1,
                Name = "First Todo",
                IsComplete = false,
                Secret = "Secret"
            },
            new TodoItem
            {
                Id = 2,
                Name = "Second Todo",
                IsComplete = false,
                Secret = "Secret"
            },
            new TodoItem
            {
                Id = 3,
                Name = "Third Todo",
                IsComplete = false,
                Secret = "Secret"
            },
        };

        mock.Setup(m => m.GetAll())
            .Returns(() => Task.FromResult(todoItems));

        mock.Setup(m => m.GetAllPaginated(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((int page, int pageSize) => 
                Task.FromResult(todoItems.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize)));

        mock.Setup(m => m.GetById(It.IsAny<long>()))
            .Returns((long id) => Task.FromResult(todoItems.First(x => x.Id == id)));

        mock.Setup(m => m.CreateTodoItem(It.IsAny<TodoItem>()))
            .Callback(() => { });
        
        mock.Setup(m => m.UpdateTodoItem(It.IsAny<long>(), It.IsAny<TodoItem>()))
            .Callback(() => { });
        
        mock.Setup(m => m.DeleteTodoItem(It.IsAny<long>()))
            .Callback(() => { });
        
        return mock;
    }
}