using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TodoApi.Common.Constants;
using TodoApi.Common.Exceptions;
using TodoApiRepository.Models;
using Xunit;

namespace TodoApi.Tests.Services.TodoItemServiceTests
{
    public class DeleteTodoItemTests : TodoItemServiceBase
    {
        [Fact]
        public async Task WhenSuccess_DeletesTodoEntity() 
        {
            //Arrange
            var todoItemId = Fixture.Create<long>();

            var todoItem = Fixture.Build<TodoItem>()
                .With(p => p.Id, todoItemId)
                .Create();

            TodoItemRepositoryMock.Setup(p => p.GetTodoItemByIdAsync(todoItemId))
                .ReturnsAsync(todoItem);

            //Act
            await TodoItemService.DeleteTodoItem(todoItemId);

            //Assert
            TodoItemRepositoryMock.Verify(p => p.GetTodoItemByIdAsync(todoItemId), Times.Once);
            TodoItemRepositoryMock.Verify(p => p.DeleteTodoItem(It.IsAny<TodoItem>()), Times.Once);
        }

        [Fact]
        public async Task WhenTodoItemNotFound_Throws_DataNotFoundException()
        {
            //Arrange
            var todoItemId = Fixture.Create<long>();

            //Act
            var exception = await Assert.ThrowsAnyAsync<Exception>(() => TodoItemService.DeleteTodoItem(todoItemId));

            //Assert
            exception.Should().BeOfType<DataNotFoundException>();
            exception.Message.Should().Be(string.Format(ErrorMessages.DataNotFoundErrorMessage, todoItemId));

            TodoItemRepositoryMock.Verify(p => p.GetTodoItemByIdAsync(todoItemId), Times.Once);
            TodoItemRepositoryMock.Verify(p => p.DeleteTodoItem(It.IsAny<TodoItem>()), Times.Never);
        }
    }
}
