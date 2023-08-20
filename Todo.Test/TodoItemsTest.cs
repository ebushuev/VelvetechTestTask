using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.API.Controllers;
using Todo.Core.Interfaces;
using Todo.Core.Models.TodoItem;
using Todo.Infrastructure.Entities;

namespace Todo.Test;

public class TodoItemsTest
{
    [TestFixture]
    public class TodoItemsControllerTests
    {
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly Mock<IItemService> mockTodoItemService = new Mock<IItemService>();

        private readonly IReadOnlyList<TodoItem> todoItems =
            new List<TodoItem>
            {
                new TodoItem { Id = 3, Name = "Buy groceries", IsComplete = false },
                new TodoItem { Id = 1, Name = "Do homework", IsComplete = true },
                new TodoItem { Id = 2, Name = "Read a book", IsComplete = false }
            };

        private TodoItemsController todoItemsController;

        [SetUp]
        public void Setup()
        {
            // Configure AutoMapper mock
            mockMapper.Setup(m => m.Map<IEnumerable<TodoItemDTO>>(It.IsAny<List<TodoItem>>()))
                .Returns((List<TodoItem> items) => items.Select(item => new TodoItemDTO()));

            mockMapper.Setup(m => m.Map<TodoItemDTO>(It.IsAny<TodoItem>()))
                .Returns((TodoItem item) => new TodoItemDTO());

            // Configure TodoItemService mock
            mockTodoItemService.Setup(service => service.GetAll()).ReturnsAsync(todoItems);

            mockTodoItemService.Setup(t => t.Read(1)).ReturnsAsync(todoItems[0]);

            todoItemsController = new TodoItemsController(mockTodoItemService.Object, mockMapper.Object);
        }

        [Test]
        public async Task GetTodoItems_ShouldReturnOkAndCallOnce_WhenTodoItemsExist()
        {
            // Act
            var result = await todoItemsController.GetTodoItems();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<TodoItemDTO>>>(result);
            mockTodoItemService.Verify(mock => mock.GetAll(), Times.Once());
        }
        
        [Test]
        public async Task ShouldReturnOkAndCallOnce_WhenTodoItemExists()
        {
            // Arrange
            var todoItem = new TodoItem { Id = 13, Name = "Test Item", IsComplete = false };
            mockTodoItemService.Setup(t => t.Read(13)).ReturnsAsync(todoItem);

            // Act
            var result = await todoItemsController.GetTodoItem(13);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            mockTodoItemService.Verify(mock => mock.Read(13), Times.Once());
        }

        [Test]
        public async Task Create_Todo_Item_Should_Return_Created_At_Route_Result_With_Created_Todo_Item()
        {
            // Arrange
            var mockCreationDto = new TodoItemDTOCreate() { Name = "Write unit tests", IsComplete = false };
            var mockEntity = new TodoItem { Id = 55, Name = "tests", IsComplete = false };

            mockMapper.Setup(m => m.Map<TodoItem>(mockCreationDto)).Returns(mockEntity);
            mockTodoItemService.Setup(s => s.Create(mockEntity)).Returns(Task.CompletedTask);

            // Act
            var result = await todoItemsController.CreateTodoItem(mockCreationDto);

            // Assert
            Assert.IsInstanceOf<CreatedAtRouteResult>(result.Result); // Use .Result here to access the ActionResult
        }
        
        [Test]
        public async Task UpdateTodoItem_ShouldReturnNoContentResult()
        {
            // Arrange
            var updateItemDto = new TodoItemDTOUpdate() { Name = "Do workout", IsComplete = true };
            var updateEntity = new TodoItem { Id = 3, Name = "Do workout", IsComplete = true };

            mockMapper.Setup(mapper => mapper.Map(updateItemDto, todoItems[0])).Returns(updateEntity);
            mockTodoItemService.Setup(service => service.Update(updateEntity)).Returns(Task.CompletedTask);

            // Act
            var result = await todoItemsController.UpdateTodoItem(3, updateItemDto);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }
        
        [Test]
        public async Task DeleteTodoItem_ShouldReturnNoContentResult()
        {
            // Act
            var result = await todoItemsController.DeleteTodoItem(3);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}