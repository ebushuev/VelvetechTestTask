using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Core.DTOs;
using TodoApi.Core.Requests;
using TodoApiRepository.Models;
using Xunit;

namespace TodoApi.Tests.Services.TodoItemServiceTests
{
    public sealed class GetTodoItemPagedTests : TodoItemServiceBase
    {
        [Fact]
        public async Task WhenSuccess_Returns_Array() 
        {
            //Arrange
            var pagedItemRequest = Fixture.Create<PagedTodoItemRequest>();
            var todoItems = Fixture.CreateMany<TodoItem>(pagedItemRequest.PageSize)
                .ToList();
            TodoItemRepositoryMock.Setup(p => p.GetPagedToDoItemsAsync(pagedItemRequest.PageSize, pagedItemRequest.PageNumber))
                .ReturnsAsync(todoItems);

            //Act
            var result = await TodoItemService.GetPagedTodoItems(pagedItemRequest);

            //Assert
            result.Should().NotBeEmpty();
            result.Should().BeOfType<TodoItemDTO[]>();
            result.Count.Should().Be(pagedItemRequest.PageSize);
            
            TodoItemRepositoryMock
                .Verify(p => p.GetPagedToDoItemsAsync(pagedItemRequest.PageSize, pagedItemRequest.PageNumber), Times.Once);
        }

        [Fact]
        public async Task WhenTodoItemDbEmpty_Return_EmptyArray() 
        {
            //Arrange 
            var pagedItemRequest = Fixture.Create<PagedTodoItemRequest>();
            TodoItemRepositoryMock.Setup(p => p.GetPagedToDoItemsAsync(pagedItemRequest.PageSize, pagedItemRequest.PageNumber))
                .ReturnsAsync(Array.Empty<TodoItem>());

            //Act
            var result = await TodoItemService.GetPagedTodoItems(pagedItemRequest);

            //Assert
            result.Should().BeOfType<TodoItemDTO[]>();
            result.Should().BeEmpty();

            TodoItemRepositoryMock
                .Verify(p => p.GetPagedToDoItemsAsync(pagedItemRequest.PageSize, pagedItemRequest.PageNumber), Times.Once);
        }
    }
}
