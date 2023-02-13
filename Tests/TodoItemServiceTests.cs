using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Services;
using Domain;
using Domain.Enums;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Xunit;

namespace Tests
{
	public class TodoItemServiceTests
	{

		private readonly Mock<ITodoItemRepository> _todoRepo = new Mock<ITodoItemRepository>();
		private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
		private readonly TodoItemService _sut;

		public TodoItemServiceTests()
		{
			_sut = new TodoItemService(_unitOfWork.Object);
		}

		[Fact]
		public async Task GetTodoItemAsync_ItemWithThatIdDoesNotExist_ReturnsNotFoundItemState()
		{
			//Arrange
			_unitOfWork.SetupGet(x => x.TodoItemRepository).Returns(_todoRepo.Object);

			//Act
			var response = await _sut.GetTodoItemAsync(2);

			//Assert
			response.State.Should().Be(ItemState.NotFound);
		}

		[Fact]
		public async Task GetTodoItemAsync_ItemWithIdExists_ReturnsDataWithDefaultState()
		{
			//Arrange
			_todoRepo.Setup(x => x.ExistAsync(It.IsAny<Expression<Func<TodoItem, bool>>>())).ReturnsAsync(true);
			_todoRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
				.ReturnsAsync(new TodoItem { Id = 10, IsComplete = true, Name = "DummyName", Secret = "DummySecret" });
			_unitOfWork.SetupGet(x => x.TodoItemRepository).Returns(_todoRepo.Object);

			//Act
			var response = await _sut.GetTodoItemAsync(2);

			//Assert
			response.State.Should().Be(ItemState.Default);
		}

		[Fact]
		public async Task AddTodoItemAsync_DtoIsNull_ReturnsItemStateOfNull()
		{
			//Arrange

			//Act
			var response = await _sut.AddTodoItemAsync(null);

			//Assert
			response.State.Should().Be(ItemState.Null);
		}

		[Fact]
		public async Task AddTodoItemAsync_DtoHasData_CallsAddAsyncAndSaveAsyncMethodsAndReturnsAddedState()
		{
			//Arrange
			var dto = new TodoItemDTO { IsComplete = true, Name = "DummyName " };

			_todoRepo.Setup(x => x.AddAsync(It.IsAny<TodoItem>()));
			_unitOfWork.Setup(x => x.SaveAsync());
			_unitOfWork.SetupGet(x => x.TodoItemRepository).Returns(_todoRepo.Object);

			//Act
			var response = await _sut.AddTodoItemAsync(dto);

			//Assert
			response.State.Should().Be(ItemState.Added);
			_todoRepo.Verify(x => x.AddAsync(It.IsAny<TodoItem>()), Times.Exactly(1));
			_unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(1));
		}

		[Fact]
		public async Task DeleteItemAsync_ItemWithThatIdDoesNotExist_ReturnsNotFoundItemState()
		{
			//Arrange
			_unitOfWork.SetupGet(x => x.TodoItemRepository).Returns(_todoRepo.Object);

			//Act
			var response = await _sut.DeleteTodoItemAsync(2);

			//Assert
			response.State.Should().Be(ItemState.NotFound);
		}

		[Fact]
		public async Task DeleteItemAsync_ItemWithThatIdExists_ReturnsDeletedItemStateWithAllVerificationsExpected()
		{
			//Arrange
			_todoRepo.Setup(x => x.ExistAsync(It.IsAny<Expression<Func<TodoItem, bool>>>())).ReturnsAsync(true);
			_todoRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(new TodoItem { Id = 2, Name = "DummyName", Secret = "DummySecret" });
			_todoRepo.Setup(x => x.DeleteAsync(It.IsAny<TodoItem>()));
			_unitOfWork.Setup(x => x.SaveAsync());
			_unitOfWork.SetupGet(x => x.TodoItemRepository).Returns(_todoRepo.Object);

			//Act
			var response = await _sut.DeleteTodoItemAsync(2);

			//Assert
			response.State.Should().Be(ItemState.Deleted);
			_todoRepo.Verify(x => x.DeleteAsync(It.IsAny<TodoItem>()), Times.Exactly(1));
			_unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(1));
		}

		[Fact]
		public async Task UpdateItemAsync_ItemWithThatIdDoesNotExist_ReturnsNotFoundItemState()
		{
			//Arrange
			_unitOfWork.SetupGet(x => x.TodoItemRepository).Returns(_todoRepo.Object);

			//Act
			var response = await _sut.UpdateTodoItemAsync(new TodoItemDTO());

			//Assert
			response.State.Should().Be(ItemState.NotFound);
		}

		[Fact]
		public async Task UpdateItemAsync_UpdatesItemWithNewValues_ReturnsUpdatedState()
		{
			//Arrange
			var item = new TodoItem { Id = 2, Name = "DummyName", Secret = "DummySecret" };
			var toUpdate = new TodoItemDTO { Id = 2, IsComplete = true, Name = "UpdatedName" };
			_todoRepo.Setup(x => x.ExistAsync(It.IsAny<Expression<Func<TodoItem, bool>>>())).ReturnsAsync(true);
			_todoRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(item);
			_todoRepo.Setup(x => x.UpdateAsync(It.Is<TodoItem>(y=> y.Id == item.Id)));
			_unitOfWork.Setup(x => x.SaveAsync());
			_unitOfWork.SetupGet(x => x.TodoItemRepository).Returns(_todoRepo.Object);

			//Act
			var response = await _sut.UpdateTodoItemAsync(toUpdate);

			//Assert
			response.State.Should().Be(ItemState.Updated);
			item.IsComplete.Should().BeTrue();
			item.Name.Should().BeEquivalentTo(toUpdate.Name);
		}
	}
}
