using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.TodoItems.Queries;
using Application.TodoItems.QueryHandlers;
using Domain.Entities;
using MediatR;
using Moq;
using Tests.Mocks;
using Xunit;

namespace Tests;

public class TodoItemsTests
{
    [Fact]
    public async void WhenGettingAllTodoItems_ThenAllTodoItemsReturn()
    {
        var mockTodoItemsRepository = MockTodoItemsRepository.GetMock();
        
        var query = new GetAllTodoItems();
        var handler = new GetAllTodoItemsHandler(mockTodoItemsRepository.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<TodoItem>>(result);
    }
    
    [Fact]
    public async void WhenGettingTodoItemById_ThenTodoItemWithMatchingIdReturns()
    {
        var mockTodoItemsRepository = MockTodoItemsRepository.GetMock();
        
        var query = new GetTodoItemById(1);
        var handler = new GetTodoItemByIdHandler(mockTodoItemsRepository.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.NotNull(result);
        Assert.IsAssignableFrom<TodoItem>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async void WhenGettingTodoItemsPaginated_ThenTodoItemsPaginatedReturn()
    {
        var mockTodoItemsRepository = MockTodoItemsRepository.GetMock();
        
        var query = new GetTodoItemsPaginated(2, 2);
        var handler = new GetTodoItemsPaginatedHandler(mockTodoItemsRepository.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<TodoItem>>(result);
        
        var todoItems = result.ToList();
        Assert.Single(todoItems.ToList());
        Assert.Equal(3, todoItems.First().Id);
    }
    
    
}