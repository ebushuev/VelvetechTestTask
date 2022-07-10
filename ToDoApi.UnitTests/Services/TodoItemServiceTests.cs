using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoApi.Data.Interfaces;
using TodoApi.Data.Models;
using TodoApi.Services.Models;
using TodoApi.Services.Services;

namespace TodoApi.UnitTests.Services
{
    [TestClass]
    public class TodoItemServiceTests
    {
        private TodoItemService _service;
        private IRepository<TodoItem, long> _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository<TodoItem, long>>();
            _service = new TodoItemService(_repository);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnTodoItems_IfExists()
        {
            //Arrange
            var items = new Fixture().Create<ReadOnlyCollection<TodoItem>>();
            _repository.GetAsync().Returns(items);

            // Act
            var result = await _service.GetAsync();

            //Assert
            Assert.AreEqual(result, items);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnTodoItem_IfExists()
        {
            //Arrange
            var item = new Fixture().Create<TodoItem>();
            _repository.GetAsync(item.Id).Returns(item);

            // Act
            var result = await _service.GetAsync(item.Id);

            //Assert
            Assert.AreEqual(result, item);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void GetAsync_ShouldReturnArgumentException_IfIdIncorrect(long id)
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.GetAsync(id));
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCreateItem_IfCorrect()
        {
            //Arrange
            var newItem = new Fixture().Create<TodoItemDTO>();

            _repository.CreateAsync(default).Returns(Task.CompletedTask);
            _repository.SaveAsync().Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(newItem);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, newItem.Name);
            Assert.AreEqual(result.IsComplete, newItem.IsComplete);
        }

        [TestMethod]
        public void CreateAsync_ShouldReturnArgumentException_IfTodoItemIsNull()
        {
            // Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.CreateAsync(null));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateItem_IfExists()
        {
            //Arrange
            var existedItem = new Fixture().Create<TodoItem>();
            var newItem = new Fixture().Build<TodoItemDTO>().With(i => i.Id, existedItem.Id).Create();

            _repository.GetAsync(existedItem.Id).Returns(existedItem);

            // Act
            var result = await _service.UpdateAsync(existedItem.Id, newItem);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, newItem.Name);
            Assert.AreEqual(result.IsComplete, newItem.IsComplete);
        }

        [TestMethod]
        public void UpdateAsync_ShouldReturnException_IfTodoItemNotExists()
        {
            //Arrange
            var existedItem = new Fixture().Create<TodoItem>();
            var newItem = new Fixture().Build<TodoItemDTO>().With(i => i.Id, existedItem.Id + 1).Create();

            _repository.GetAsync(existedItem.Id).Returns(existedItem);

            // Act
            Assert.ThrowsExceptionAsync<Exception>(async () => await _service.UpdateAsync(newItem.Id, newItem));
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void UpdateAsync_ShouldReturnArgumentException_IfIdIncorrect(long id)
        {
            var item = new Fixture().Create<TodoItemDTO>();
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.UpdateAsync(id, item));
        }

        [TestMethod]
        public void UpdateAsync_ShouldReturnArgumentException_IfIdIncorrect()
        {
            var id = new Fixture().Create<long>();

            // Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.UpdateAsync(id, null));
            _repository.Received(1);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteAsync_ShouldReturnArgumentException_IfIdIncorrect(long id)
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.DeleteAsync(id));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnCompletedTask_IfOK()
        {
            //Arrange
            var item = new Fixture().Create<TodoItem>();
            _repository.DeleteAsync(item.Id).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(item.Id);

            //Assert
            _repository.Received(1);
        }
    }
}
