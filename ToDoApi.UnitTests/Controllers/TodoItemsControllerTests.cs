using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Data.Models;
using TodoApi.Services.Models;
using TodoApi.Services.Services.Interfaces;
using TodoApi.UnitTests;

namespace TodoApiDTO.Controllers.Tests
{
    [TestClass]
    public class TodoItemsControllerTests : BaseUnitTest
    {
        private TodoItemsController _controller;
        private ITodoItemService _todoItemService;
        private ITodoItemMappingService _todoItemMappingService;

        [TestInitialize]
        public void SetUp()
        {
            _todoItemService = Substitute.For<ITodoItemService>();
            _todoItemMappingService = Substitute.For<ITodoItemMappingService>();
            _controller = new TodoItemsController(_todoItemService, _todoItemMappingService);
        }

        [TestMethod]
        public async Task GetTodoItems_ShouldReturnTodoItems_IfExists()
        {
            // Arrange
            Fixture fixture = new Fixture();
            var items = fixture.Create<IReadOnlyCollection<TodoItem>>();
            _todoItemService.GetAsync().Returns(items);

            // Act
            var actionResult = await _controller.GetTodoItems();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            _todoItemMappingService.Received(1).MapTodoItemToDTO(items);
        }


        [TestMethod]
        public async Task GetTodoItem_ShouldReturnTodoItem_IfExists()
        {
            // Arrange
            var item = new Fixture().Create<TodoItem>();
            var itemDTO = new Fixture().Create<TodoItemDTO>();
            _todoItemService.GetAsync(item.Id).Returns(item);
            _todoItemMappingService.MapTodoItemToDTO(item).Returns(itemDTO);

            // Act
            var actionResult = await _controller.GetTodoItem(item.Id);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual(result.Value, itemDTO);
            _todoItemMappingService.Received(1).MapTodoItemToDTO(item);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void GetTodoItem_ShouldReturnArgumentException_IfIdIsIncorrect(long id)
        {
            // Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _controller.GetTodoItem(id));
        }

        [TestMethod]
        public async Task GetTodoItem_ShouldReturnNotFound_IfNotExists()
        {
            // Arrange
            Fixture fixture = new Fixture();
            var item = fixture.Create<TodoItem>();
            _todoItemService.GetAsync(item.Id).Returns(null as TodoItem);

            // Act
            var actionResult = await _controller.GetTodoItem(item.Id);

            // Assert
            CheckNotFoundException(actionResult.Result);
        }

        [TestMethod]
        public async Task UpdateTodoItem_ShouldUpdateItem_IfExistsAndServiceRetunsOK()
        {
            // Arrange
            Fixture fixture = new Fixture();
            var newItemDTO = fixture.Create<TodoItemDTO>();
            var updateItem = fixture.Create<TodoItem>();
            _todoItemService.UpdateAsync(newItemDTO.Id, newItemDTO).Returns(updateItem);

            // Act
            var actionResult = await _controller.UpdateTodoItem(newItemDTO.Id, newItemDTO);

            // Assert
            var result = actionResult as IStatusCodeActionResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public void UpdateTodoItem_ShouldReturnBadRequest_IfTodoItemDTOIsNull()
        {
            // Arrange
            var newItemDTO = new Fixture().Create<TodoItemDTO>();

            // Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _controller.UpdateTodoItem(newItemDTO.Id, null));
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void UpdateTodoItem_ShouldReturnArgumentException_IfIdIsIncorrect(long id)
        {
            // Arrange
            var newItemDTO = new Fixture().Create<TodoItemDTO>();

            // Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _controller.UpdateTodoItem(id, newItemDTO));
        }

        [TestMethod]
        public async Task CreateTodoItem_ShouldCreateTodoItem_IfServiceSuccessfull()
        {
            // Arrange
            Fixture fixture = new Fixture();
            var itemDTO = fixture.Create<TodoItemDTO>();
            var createdItem = fixture.Create<TodoItem>();
            _todoItemService.CreateAsync(itemDTO).Returns(createdItem);

            // Act
            var actionResult = await _controller.CreateTodoItem(itemDTO);

            // Assert
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        [TestMethod]
        public void CreateTodoItem_ShouldReturnArgumentException_IfTodoItemDTOIsNull()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _controller.CreateTodoItem(null));
        }

        [TestMethod]
        public async Task DeleteTodoItemTest_ShouldRetunOk_IfServiceNotFailed()
        {
            // Arrange
            Fixture fixture = new Fixture();
            var item = fixture.Create<TodoItem>();
            _todoItemService.DeleteAsync(item.Id).Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteTodoItem(item.Id);

            // Assert
            var result = actionResult as IStatusCodeActionResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteAsync_ShouldReturnArgumentException_IfIdIsIncorrect(long id)
        {
            // Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _controller.DeleteTodoItem(id));
        }
    }
}