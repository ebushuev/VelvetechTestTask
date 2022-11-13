using AutoFixture;
using Moq;
using System;
using System.Threading.Tasks;
using TodoApi.Core.Requests;
using TodoApiRepository.Models;
using Xunit;

namespace TodoApi.Tests.Services.TodoItemServiceTests
{
    public class UpdateTodoItemTests : TodoItemServiceBase
    {
        [Fact]
        public async Task WhenSuccess_UpdateTodoItem() 
        {
            //Arrange
            var todoItemId = Fixture.Create<long>();
            var todoItemArgs = Fixture.Create<TodoItemArgs>();
            var todoItem = Fixture.Build<TodoItem>()
                .With(p => p.Id, todoItemId)
                .With(p => p.IsComplete, todoItemArgs.IsComplete)
                .With(p => p.Name, todoItemArgs.Name)
                .Create();

            TodoItemRepositoryMock.Setup(p => p.GetTodoItemByIdAsync(todoItemId))
                .ReturnsAsync(todoItem);

            //Act
            await TodoItemService.UpdateTodoItem(todoItemId, todoItemArgs);

            //Arrange
            TodoItemRepositoryMock.Verify(p => p.GetTodoItemByIdAsync(todoItemId), Times.Once);
            TodoItemRepositoryMock.Verify(p => p.UpdateTododItem(It.IsAny<TodoItem>()), Times.Once);
        }

        [Fact]
        public async Task WhenTodoItemNotFound_Throws_DataNotFoundException() 
        {
            //Arrange
            var todoItemId = Fixture.Create<long>();
            var todoItemArgs = Fixture.Create<TodoItemArgs>();

            //Act
            var exception = Assert.ThrowsAnyAsync<Exception>(() => TodoItemService.UpdateTodoItem(todoItemId, todoItemArgs));

            //Assert
            TodoItemRepositoryMock.Verify(p => p.GetTodoItemByIdAsync(todoItemId), Times.Once);
            TodoItemRepositoryMock.Verify(p => p.UpdateTododItem(It.IsAny<TodoItem>()), Times.Never);
        }
    }
}
