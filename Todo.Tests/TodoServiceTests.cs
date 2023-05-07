using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Bll.Services;
using Todo.Common.Models.Domain;
using Todo.Common.Models.DTO;
using Todo.Common.Models.Responses;
using Todo.Common.RepositoryInterfaces;
using Xunit;

namespace Todo.Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> todoRepositoryMock;
        private readonly IMapper mapper;
        private readonly TodoService todoService;

        public TodoServiceTests()
        {
            todoRepositoryMock = new Mock<ITodoRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Item, ItemDto>();
                cfg.CreateMap<ItemDto, Item>();
            });
            mapper = config.CreateMapper();

            todoService = new TodoService(todoRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetItemAsync_ShouldReturnCorrectItem()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var item = new Item { Id = itemId, Name = "Test Item", IsComplete = false };
            todoRepositoryMock.Setup(x => x.GetItemAsync(itemId)).ReturnsAsync(item);

            // Act
            var result = await todoService.GetItemAsync(itemId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itemId, result.Id);
            Assert.Equal(item.Name, result.Name);
            Assert.Equal(item.IsComplete, result.IsComplete);
        }

        [Fact]
        public async Task GetItemsAsync_ShouldReturnAllItems()
        {
            // Arrange
            var items = new List<Item>
        {
            new Item { Id = Guid.NewGuid(), Name = "Item 1", IsComplete = false },
            new Item { Id = Guid.NewGuid(), Name = "Item 2", IsComplete = true },
            new Item { Id = Guid.NewGuid(), Name = "Item 3", IsComplete = false }
        };
            todoRepositoryMock.Setup(x => x.GetItemsAsync()).ReturnsAsync(items);

            // Act
            var result = await todoService.GetItemsAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(items.Count, result.Count);
            for (int i = 0; i < items.Count; i++)
            {
                Assert.Equal(items[i].Id, result[i].Id);
                Assert.Equal(items[i].Name, result[i].Name);
                Assert.Equal(items[i].IsComplete, result[i].IsComplete);
            }
        }
        [Fact]
        public async Task CreateItemAsync_ShouldCreateNewItem()
        {
            // Arrange
            var itemName = "New Item";
            var item = new Item { Id = Guid.NewGuid(), Name = itemName, IsComplete = false };
            todoRepositoryMock.Setup(x => x.CreateItemAsync(item)).Returns(Task.CompletedTask);

            // Act
            var result = await todoService.CreateItemAsync(itemName, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(item.Name, result.Name);
            Assert.Equal(item.IsComplete, result.IsComplete);
        }
        [Fact]
        public async Task DeleteItemAsync_ShouldDeleteItem()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var item = new Item { Id = itemId, Name = "Test Item", IsComplete = false };
            todoRepositoryMock.Setup(x => x.GetItemAsync(itemId)).ReturnsAsync(item);
            todoRepositoryMock.Setup(x => x.RemoveItemAsync(item)).Returns(Task.CompletedTask);

            // Act
            await todoService.DeleteItemAsync(itemId);

            // Assert
            todoRepositoryMock.Verify(x => x.RemoveItemAsync(item), Times.Once);
        }


        [Fact]
        public async Task UpdateItemAsync_WithNonexistentItem_ReturnsNotFoundResponse()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var itemDto = new ItemDto
            {
                Id = itemId,
                Name = "Test Item",
                IsComplete = true
            };
            todoRepositoryMock.Setup(repo => repo.GetItemAsync(itemId)).ReturnsAsync((Item)null);

            // Act
            var result = await todoService.UpdateItemAsync(itemDto);

            // Assert
            Assert.Equal("Entity not found", result.Message);
            Assert.Equal(itemDto, result.Item);
        }

        [Fact]
        public async Task UpdateItemAsync_WithConcurrencyException_ReturnsConflictResponse()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var item = new ItemDto
            {
                Id = itemId,
                Name = "Test Item",
                IsComplete = false
            };

            var storedItem = new Item
            {
                Id = itemId,
                Name = "Stored Item",
                IsComplete = true,
                CreatedOn = DateTime.UtcNow
            };

            todoRepositoryMock.Setup(x => x.GetItemAsync(itemId)).ReturnsAsync(storedItem);

            todoRepositoryMock
                .Setup(x => x.UpdateItemAsync(storedItem))
                .Throws(new DbUpdateConcurrencyException());

            var service = new TodoService(todoRepositoryMock.Object, null);

            // Act
            var result = await service.UpdateItemAsync(item);

            // Assert
            Assert.IsType<ItemResponse>(result);
            Assert.Equal("Entity locked by other user, check new state and try later", result.Message);
            Assert.Equal(item.Id, result.Item.Id);
            Assert.Equal(item.Name, result.Item.Name);
            Assert.Equal(item.IsComplete, result.Item.IsComplete);
        }
    }
}
