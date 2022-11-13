using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Core.DTOs;
using TodoApi.Core.Requests;
using TodoApiRepository.Models;
using Xunit;

namespace TodoApi.Tests.Services.TodoItemServiceTests
{
    public sealed class AddTodoItemTests : TodoItemServiceBase
    {
        [Fact]
        public async Task WhenSuccess_Returns_AddedTodoItem() 
        {
            //Arrange
            var todoItemArgs = Fixture.Create<TodoItemArgs>();
            var todoItem = Fixture.Build<TodoItem>()
                .With(p => p.Name, todoItemArgs.Name)
                .With(p => p.IsComplete, todoItemArgs.IsComplete)
                .Create();
            //Act
            var result = await TodoItemService.AddTodoItem(todoItemArgs);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TodoItemDTO>();
            result.Name.Should().Be(todoItemArgs.Name);
            result.IsComplete.Should().Be(todoItem.IsComplete);

            TodoItemRepositoryMock.Verify(p => p.AddTodoItemAsync(It.IsAny<TodoItem>()), Times.Once);
        }
    }
}
