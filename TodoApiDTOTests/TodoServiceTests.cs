using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TodoApi.Models;
using TodoApiDTO.Data;
using TodoApiDTO.Services;
using Xunit;

namespace TodoApiDTOTests
{
    public class TodoServiceTests :IDisposable
    {
        private readonly Mock<TodoContext> _context;

        private static readonly List<TodoItem> _todos = new()
        {
            new()
            {
                Id = 202
            },
            new()
            {
                Id = 302
            },
            new()
            {
                Id = 402
            }
        };

        public TodoServiceTests()
        {
            var contextMock = new Mock<TodoContext>();
            contextMock.Setup(x => x.TodoItems).ReturnsDbSet(_todos);
            contextMock.Setup(x => x.TodoItems.FindAsync(It.IsAny<object?[]?>()))
                .ReturnsAsync<object?[]?, TodoContext, TodoItem?>((arr) => {
                    var id = (long)arr![0]!; // it's safe, because we always have a long in our cases
                    return _todos.FirstOrDefault(x => x.Id == id); 
                });

            _context = contextMock;
        }

        [Theory]
        [InlineData(false, null)]
        [InlineData(false, "")]
        [InlineData(true, "my new todo")]
        public async Task Create_ShouldCreateNewItems(bool isComplete, string name)
        {
            var dto = new TodoItemDTO
            {
                IsComplete = isComplete,
                Name = name
            };
            var sut = new TodoService(_context.Object);
            var item = await sut.Create(dto);
            Assert.Equal(isComplete, item.IsComplete);
            Assert.Equal(name, item.Name);
            _context.Verify(x => x.TodoItems.Add(It.IsAny<TodoItem>()), Times.Once);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteUnexistingTodo() 
        {
            var sut = new TodoService(_context.Object);
            var wasDeleted = await sut.Delete(201);

            Assert.False(wasDeleted);
            _context.Verify(x => x.TodoItems.Remove(It.IsAny<TodoItem>()), Times.Never);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingTodo() 
        {
            var sut = new TodoService(_context.Object);
            var wasDeleted = await sut.Delete(202);

            Assert.True(wasDeleted);
            _context.Verify(x => x.TodoItems.Remove(It.IsAny<TodoItem>()), Times.Once);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Get_ShouldGetAllItems() 
        {
            var sut = new TodoService(_context.Object);
            var items = await sut.Get();

            Assert.NotEmpty(items);
            Assert.Equal(_todos.Count, items.Count);
            _context.Verify(x => x.TodoItems, Times.Once);
        }

        [Fact]
        public async Task Get_ShouldReturnDefaultWhenSpecificIdWasNotFound() 
        {
            var sut = new TodoService(_context.Object);
            var item = await sut.Get(301);

            Assert.Null(item);
            _context.Verify(x => x.TodoItems, Times.Once);
        }

        [Fact]
        public async Task Get_ShouldGetSpecificItemById() 
        {
            var sut = new TodoService(_context.Object);
            var item = await sut.Get(302);

            Assert.NotNull(item);
            Assert.Equal(302, item.Id);
            _context.Verify(x => x.TodoItems, Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnFalseWhenSpecificIdWasNotFound()
        {
            var sut = new TodoService(_context.Object);
            var wasUpdated = await sut.Update(401, new TodoItemDTO());

            Assert.False(wasUpdated);
            _context.Verify(x => x.TodoItems.Update(It.IsAny<TodoItem>()), Times.Never);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldThrowDbUpdateConcurrencyExceptionOnExceptionWhenItemExists()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateConcurrencyException());

            var sut = new TodoService(_context.Object);
            
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await sut.Update(402, new TodoItemDTO()));
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnFalseOnDbUpdateConcurrencyException()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateConcurrencyException());
            _context.Setup(x => x.TodoItems.FindAsync(It.IsAny<object?[]?>()))
                .ReturnsAsync(new TodoItem { Id = 403 });

            var sut = new TodoService(_context.Object);
            var wasUpdated = await sut.Update(403, new TodoItemDTO());

            Assert.False(wasUpdated);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldUpdateSpecificIdItem()
        {
            var sut = new TodoService(_context.Object);
            var wasUpdated = await sut.Update(402, new TodoItemDTO());

            Assert.True(wasUpdated);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        public void Dispose() { }
    }
}