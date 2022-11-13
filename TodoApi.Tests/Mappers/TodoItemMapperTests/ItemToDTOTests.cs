using AutoFixture;
using FluentAssertions;
using TodoApi.Core.DTOs;
using TodoApi.Core.Mappers;
using TodoApiRepository.Models;
using Xunit;

namespace TodoApi.Tests.Mappers.TodoItemMapperTests
{
    public class ItemToDTOTests : TodoItemMappersBase
    {
        
        [Fact]
        public void ItemToDTO_WhenSuccess_Map_TodoItem_To_ToTodoItemDTO() 
        {
            //Arrange
            var todoIten = Fixture.Create<TodoItem>();

            //Act
            var result = todoIten.ItemToDTO();

            //Assert
            result.Should().BeOfType<TodoItemDTO>();
            result.Id.Should().Be(todoIten.Id);
            result.Name.Should().Be(todoIten.Name);
            result.IsComplete.Should().Be(todoIten.IsComplete);
        }
    }
}
