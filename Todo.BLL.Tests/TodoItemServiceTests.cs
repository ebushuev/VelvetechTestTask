using Moq;
using Todo.BLL.Interfaces;
using Todo.BLL.Services;
using Todo.DAL;
using Todo.DAL.Entities;
using Xunit;

namespace Todo.BLL.Tests;

public class TodoItemServiceTests
{
    private readonly Mock<IRepository<TodoItem>> repositoryMock;
    private readonly ITodoItemService todoItemService;

    public TodoItemServiceTests()
    {
        repositoryMock = new Mock<IRepository<TodoItem>>();
        todoItemService = new TodoItemService(repositoryMock.Object);
    }

    [Fact]
    public async Task GetTodoItemsAsync_ShouldReturnAllTodoItems()
    {
        // Arrange
        repositoryMock.Setup(repo => repo.GetAll(false)).ReturnsAsync(GetTestTodoItems());

        // Act
        var todoItems = await todoItemService.GetTodoItemsAsync(false);

        // Assert
        repositoryMock.Verify(t => t.GetAll(false));
        Assert.NotNull(todoItems);
    }

    [Fact]
    public async Task GetTodoItemAsync_ShouldReturnTodoItemById()
    {
        // Arrange
        var todoItems = GetTestTodoItems();
        var id = todoItems[0].Id;

        repositoryMock.Setup(repo => repo.GetByCondition(l => l.Id == id, false)).ReturnsAsync(todoItems[0]);

        // Act
        var result = await todoItemService.GetTodoItemAsync(id, false);

        // Assert
        Assert.Equal(todoItems[0], result);
    }

    [Fact]
    public async Task CreateToDoItemAsync_ShouldAddNewTodoItem()
    {
        // Arrange
        var todoItem = GetTestTodoItems()[0];

        // Act
        await todoItemService.CreateToDoItemAsync(todoItem);

        // Assert
        repositoryMock.Verify(t => t.Create(It.IsAny<TodoItem>()));
        repositoryMock.Verify(t => t.SaveChanges());
    }

    [Fact]
    public async Task UpdateTodoItemAsync_ShouldUpdateTodoItem_CallOnce()
    {
        // Arrange
        var todoItem = GetTestTodoItems()[0];

        // Act
        await todoItemService.UpdateTodoItemAsync(todoItem);

        // Assert
        repositoryMock.Verify(t => t.Update(todoItem), Times.Once);
        repositoryMock.Verify(t => t.SaveChanges());
    }

    [Fact]
    public async Task DeleteTodoItemAsync_ShouldDeleteTodoItem_CallOnce()
    {
        // Arrange
        var todoItem = GetTestTodoItems()[0];

        // Act
        await todoItemService.DeleteTodoItemAsync(todoItem);

        // Assert
        repositoryMock.Verify(t => t.Delete(todoItem), Times.Once);
        repositoryMock.Verify(t => t.SaveChanges());
    }

    private List<TodoItem> GetTestTodoItems()
    {
        return new List<TodoItem>
        {
            new TodoItem { Id = Guid.NewGuid(), Name = "Buy groceries", IsComplete = false },
            new TodoItem { Id = Guid.NewGuid(), Name = "Do laundry", IsComplete = true },
            new TodoItem { Id = Guid.NewGuid(), Name = "Read a book", IsComplete = false }
        };
    }

}
