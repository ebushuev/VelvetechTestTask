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

        [TestCleanup]
        public void Cleanup()
        {
            _repository.ClearReceivedCalls();
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
            await _repository.Received(1).GetAsync();
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
            await _repository.Received(1).GetAsync(item.Id);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task GetAsync_ShouldReturnArgumentException_IfIdIncorrect(long id)
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.GetAsync(id));

            //Assert
            await _repository.DidNotReceive().GetAsync(id);
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

            await _repository.Received(1).CreateAsync(Arg.Any<TodoItem>());
            await _repository.Received(1).SaveAsync();
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnArgumentException_IfTodoItemIsNull()
        {
            // Act
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _service.CreateAsync(null));

            // Assert
            await _repository.DidNotReceive().CreateAsync(default);
            await _repository.DidNotReceive().SaveAsync();
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

            await _repository.Received(1).GetAsync(existedItem.Id);
            _repository.Received(1).Update(Arg.Any<TodoItem>());
            await _repository.Received(1).SaveAsync();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnException_IfTodoItemNotExists()
        {
            //Arrange
            var existedItem = new Fixture().Create<TodoItem>();
            var newItem = new Fixture().Build<TodoItemDTO>().With(i => i.Id, existedItem.Id + 1).Create();
            _repository.GetAsync(newItem.Id).Returns<TodoItem>(l => null);

            // Act
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _service.UpdateAsync(newItem.Id, newItem));

            // Assert
            await _repository.Received(1).GetAsync(newItem.Id);
            _repository.DidNotReceive().Update(Arg.Any<TodoItem>());
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task UpdateAsync_ShouldReturnArgumentException_IfIdIncorrect(long id)
        {
            // Arrange
            var item = new Fixture().Create<TodoItemDTO>();

            //Act
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.UpdateAsync(id, item));

            // Assert
            await _repository.DidNotReceive().GetAsync(item.Id);
            await _repository.DidNotReceive().SaveAsync();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnArgumentException_IfIdIncorrect()
        {
            //Arrange
            var id = new Fixture().Create<long>();

            // Act
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.UpdateAsync(id, null));

            //Assert
            await _repository.DidNotReceive().GetAsync(id);
            await _repository.DidNotReceive().SaveAsync();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteAsync_ShouldReturnArgumentException_IfIdIncorrect(long id)
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.DeleteAsync(id));

            //Assert
            await _repository.DidNotReceive().DeleteAsync(id);
            await _repository.DidNotReceive().SaveAsync();
        }

        [TestMethod]
        public async Task DeleteAsyncc_ShouldReturnException_IfTodoItemNotExists()
        {
            //Arrange
            var item = new Fixture().Create<TodoItem>();
            _repository.GetAsync(item.Id).Returns<TodoItem>(l => null);

            // Act
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _service.DeleteAsync(item.Id));

            // Assert
            await _repository.Received(1).GetAsync(item.Id);
            await _repository.DidNotReceive().DeleteAsync(item.Id);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnCompletedTask_IfOK()
        {
            //Arrange
            var item = new Fixture().Create<TodoItem>();
            _repository.GetAsync(item.Id).Returns(item);
            _repository.DeleteAsync(item.Id).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(item.Id);

            //Assert
            await _repository.Received(1).DeleteAsync(item.Id);
            await _repository.Received(1).SaveAsync();
        }
    }
}
