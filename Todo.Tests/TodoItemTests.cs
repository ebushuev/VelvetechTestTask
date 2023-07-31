using Todo.Core.Business.TodoItem.Commands.Create;
using Todo.Core.Business.TodoItem.Commands.Delete;
using Todo.Core.Business.TodoItem.Commands.Update;
using Todo.Core.Business.TodoItem.Entities;
using Todo.Core.Exceptions;
using Todo.Core.Tests.Data;
using Xunit;

namespace Todo.Core.Tests
{
    public class TodoItemTests
    {
        private readonly DataFixture _dataFixture;

        public TodoItemTests()
        {
            _dataFixture = new DataFixture();
        }

        [Fact]
        public async Task AddTodo_Succeess()
        {
            var createCommand = new CreateCommand
            {
                Name = "Test add project success"
            };

            var addedTodoItem = await _dataFixture.Mediator.Send(createCommand, CancellationToken.None);

            var todoItemfromDb = await _dataFixture.TodoRepository.FindByIdAsync(addedTodoItem.Id, CancellationToken.None);

            Assert.NotNull(todoItemfromDb);
            Assert.Equal(todoItemfromDb.Name, addedTodoItem.Name);
        }

        [Fact]
        public async Task AddTodo_BadRequestException()
        {
            var createCommand = new CreateCommand
            {
                Name = "    "
            };

            await Assert.ThrowsAsync<BadRequestException>(async () => await _dataFixture.Mediator.Send(createCommand, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateTodo_Success()
        {
            var itemToUpdate = _dataFixture.TodoItems[0];
            var updateCommand = new UpdateCommand
            {
                Id = itemToUpdate.Id,
                Name = $"{itemToUpdate.Name}_updated",
                IsComplete = true
            };

            await _dataFixture.Mediator.Send(updateCommand, CancellationToken.None);

            var updatedItemfromDb = await _dataFixture.TodoRepository.FindByIdAsync(updateCommand.Id, CancellationToken.None);

            Assert.NotNull(updatedItemfromDb);
            Assert.Equal(updatedItemfromDb.Name, updateCommand.Name);
            Assert.Equal(updatedItemfromDb.IsComplete, updateCommand.IsComplete);
        }

        [Fact]
        public async Task UpdateTodo_NotFoundException()
        {
            var itemToUpdate = _dataFixture.TodoItems[0];
            var updateCommand = new UpdateCommand
            {
                Id = Guid.NewGuid(),
                Name = $"{itemToUpdate.Name}_updated",
                IsComplete = true
            };

            await Assert.ThrowsAsync<NotFoundException>(async () => await _dataFixture.Mediator.Send(updateCommand, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteTodo_Succeess()
        {
            var itemToDelete = _dataFixture.TodoItems[0];

            await _dataFixture.Mediator.Send(new DeleteCommand(itemToDelete.Id), CancellationToken.None);

            var removedTodoItem = await _dataFixture.TodoRepository.FindByIdAsync(itemToDelete.Id, CancellationToken.None);

            Assert.Null(removedTodoItem);
        }

        [Fact]
        public async Task DeleteTodo_NotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _dataFixture.Mediator.Send(new DeleteCommand(Guid.NewGuid()), CancellationToken.None));
        }
    }
}
