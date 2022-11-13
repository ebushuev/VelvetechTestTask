using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TodoApi.Common.Constants;
using TodoApi.Common.Exceptions;
using TodoApi.Core.DTOs;
using TodoApiRepository.Models;
using Xunit;

namespace TodoApi.Tests.Services.TodoItemServiceTests
{
    public sealed class GetTodoItemByIdTests : TodoItemServiceBase
    {
        [Fact]
        public async Task WhenSuccess_Returns_TodoItem() 
        {
            //Arrange
            var todoItemId = Fixture.Create<long>();
            var todoItem = Fixture.Build<TodoItem>()
                .With(p => p.Id, todoItemId)
                .Create();

            TodoItemRepositoryMock.Setup(p => p.GetTodoItemByIdAsync(todoItemId))
                .ReturnsAsync(todoItem);

            //Act
            var result = await TodoItemService.GetTodoItemById(todoItemId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TodoItemDTO>();
            result.Id.Should().Be(todoItemId);
            result.Name.Should().Be(todoItem.Name);
            result.IsComplete.Should().Be(result.IsComplete);

            TodoItemRepositoryMock.Verify(p => p.GetTodoItemByIdAsync(todoItemId), Times.Once);
        }

        [Fact]
        public async Task WnenTodoItemNotFound_Throws_DataNotFoundException() 
        {
            //Arrange
            var todoItemId = Fixture.Create<long>();

            //Act
            var exception = await Assert.ThrowsAnyAsync<Exception>(() => TodoItemService.GetTodoItemById(todoItemId));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<DataNotFoundException>();
            exception.Message.Should().Be(string.Format(ErrorMessages.DataNotFoundErrorMessage, todoItemId));

            TodoItemRepositoryMock.Verify(p => p.GetTodoItemByIdAsync(todoItemId), Times.Once);
        }
    }
}
