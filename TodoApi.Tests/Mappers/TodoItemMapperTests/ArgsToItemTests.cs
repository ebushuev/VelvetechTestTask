using AutoFixture;
using FluentAssertions;
using TodoApi.Core.Mappers;
using TodoApi.Core.Requests;
using TodoApiRepository.Models;
using Xunit;

namespace TodoApi.Tests.Mappers.TodoItemMapperTests
{
    public class ArgsToItemTests : TodoItemMappersBase
    {
        [Fact]
        public void WhenSuccess_Map_TodoItemArgs_To_TodoItem() 
        {
            //Arrange
            var todoItemArgs = Fixture.Create<TodoItemArgs>();

            //Act
            var result = todoItemArgs.ArgsToItem();

            //Assert
            result.Should().BeOfType<TodoItem>();
            result.Name.Should().Be(todoItemArgs.Name);
            result.IsComplete.Should().Be(todoItemArgs.IsComplete);
            result.Id.Should().Be(default(long));
            result.Rowversion.Should().BeNull();
            result.Secret.Should().BeNull();
        }
    }
}
