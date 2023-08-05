using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.BLL.Interfaces;
using Todo.Controllers;
using Todo.DAL.Entities;
using Todo.Dtos;
using Todo.Exceptions.TodoItem;
using Xunit;

namespace Todo.Tests;

public class TodoItemsControllerTests
{
    private static readonly Guid todoItemId = Guid.NewGuid();
    private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
    private readonly Mock<ITodoItemService> mockTodoItemService = new Mock<ITodoItemService>();

    private readonly IReadOnlyList<TodoItem> todoItems =
        new List<TodoItem>
        {
            new TodoItem { Id = todoItemId, Name = "Buy groceries", IsComplete = false },
            new TodoItem { Id = Guid.NewGuid(), Name = "Do homework", IsComplete = true },
            new TodoItem { Id = Guid.NewGuid(), Name = "Read a book", IsComplete = false }
        };

    private TodoItemsController todoItemsController;

    public TodoItemsControllerTests()
    {
        mockMapper.Setup(m => m.Map<IEnumerable<TodoItemDto>>(It.IsAny<List<TodoItem>>()))
            .Returns((List<TodoItem> items) => items.Select(item => new TodoItemDto(item.Id, item.Name, item.IsComplete)));

        mockMapper.Setup(m => m.Map<TodoItemDto>(It.IsAny<TodoItem>()))
            .Returns((TodoItem item) => new TodoItemDto(item.Id, item.Name, item.IsComplete));

        mockTodoItemService.Setup(service => service.GetTodoItemsAsync(false)).ReturnsAsync(todoItems);

        mockTodoItemService.Setup(t => t.GetTodoItemAsync(todoItemId, false)).ReturnsAsync(todoItems[0]);

        todoItemsController = new TodoItemsController(mockTodoItemService.Object, mockMapper.Object);
    }

    [Fact]
    public async Task GetTodoItems_ShouldReturnOkAndCallOnce_WhenTodoItemsExist()
    {
        // Act
        var result = await todoItemsController.GetTodoItems();

        // Assert
        Assert.IsType<ActionResult<IEnumerable<TodoItemDto>>>(result);
        mockTodoItemService.Verify(mock => mock.GetTodoItemsAsync(false), Times.Once());
    }

    [Fact]
    public async Task GetTodoItem_ShouldReturnOkAndCallOnce_WhenTodoItemExists()
    {
        // Act
        var result = await todoItemsController.GetTodoItem(todoItemId);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        mockTodoItemService.Verify(mock => mock.GetTodoItemAsync(todoItemId, false), Times.Once());
    }

    [Fact]
    public async Task GetTodoItem_ShouldThrowException_WhenTodoItemDoesNotExist()
    {
        // Arrange
        var mockTodoItemId = Guid.NewGuid();
        mockTodoItemService.Setup(t => t.GetTodoItemAsync(mockTodoItemId, false)).ReturnsAsync((TodoItem)null);

        // Act and Assert
        await Assert.ThrowsAsync<TodoItemNotFoundException>(() => todoItemsController.GetTodoItem(mockTodoItemId));
    }

    [Fact]
    public async Task CreateTodoItem_ShouldReturnCreatedAtRouteResultWithCreatedTodoItem()
    {
        // Arrange
        var mockCreationDto = new TodoItemForCreationDto { Name = "Write unit tests", IsComplete = false };
        var mockEntity = new TodoItem { Id = Guid.NewGuid(), Name = "Write unit tests", IsComplete = false };

        mockMapper.Setup(m => m.Map<TodoItem>(mockCreationDto)).Returns(mockEntity);
        mockTodoItemService.Setup(s => s.CreateToDoItemAsync(mockEntity)).Returns(Task.CompletedTask);

        // Act
        var result = await todoItemsController.CreateTodoItem(mockCreationDto);

        // Assert
        Assert.IsType<CreatedAtRouteResult>(result);
    }

    [Fact]
    public async Task UpdateTodoItem_ShouldReturnNoContentResult()
    {
        // Arrange
        var updateItemDto = new TodoItemForUpdateDto { Name = "Do workout", IsComplete = true };
        var updateEntity = new TodoItem { Id = todoItemId, Name = "Do workout", IsComplete = true };

        mockMapper.Setup(mapper => mapper.Map(updateItemDto, todoItems[0])).Returns(updateEntity);
        mockTodoItemService.Setup(service => service.UpdateTodoItemAsync(updateEntity)).Returns(Task.CompletedTask);

        // Act
        var result = await todoItemsController.UpdateTodoItem(todoItemId, updateItemDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateTodoItem_ShouldThrowException_WhenTodoItemDoesNotExist()
    {
        // Arrange
        var mockTodoItemId = Guid.NewGuid();
        var updateItemDto = new TodoItemForUpdateDto { Name = "Do workout", IsComplete = true };
        mockTodoItemService.Setup(t => t.GetTodoItemAsync(mockTodoItemId, false)).ReturnsAsync((TodoItem)null);

        // Act and Assert
        await Assert.ThrowsAsync<TodoItemNotFoundException>(() => todoItemsController.UpdateTodoItem(mockTodoItemId, updateItemDto));
    }

    [Fact]
    public async Task DeleteTodoItem_ShouldReturnNoContentResult()
    {
        // Act
        var result = await todoItemsController.DeleteTodoItem(todoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTodoItem_ShouldThrowException_WhenTodoItemDoesNotExist()
    {
        // Arrange
        var mockTodoItemId = Guid.NewGuid();
        mockTodoItemService.Setup(t => t.GetTodoItemAsync(mockTodoItemId, false)).ReturnsAsync((TodoItem)null);

        // Act and Assert
        await Assert.ThrowsAsync<TodoItemNotFoundException>(() => todoItemsController.DeleteTodoItem(mockTodoItemId));
    }
}
