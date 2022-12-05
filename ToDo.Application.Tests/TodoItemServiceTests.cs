using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ToDo.Application.Mapping;
using ToDo.Application.Models;
using ToDo.Application.Services;
using ToDo.DAL.Exceptions;
using ToDo.DAL.Repositories;
using ToDo.Domain.Models;
using Xunit;

namespace ToDo.Application.Tests
{
    public class TodoItemServiceTests
    {
        private readonly TodoItemService _service;
        private readonly Mock<ITodoItemRepository> _repository = new Mock<ITodoItemRepository>();
        private readonly IMapper _mapper;

        public TodoItemServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(opt => { opt.AddProfile(new AppMapperProfile()); }));
            _service = new TodoItemService(_repository.Object, _mapper);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnToDoDto_WhenExists()
        {
            var todoId = 1;
            var todoItem = new ToDoItem
            {
                Id = todoId,
                IsComplete = false,
                Name = "Name",
                Secret = "secret"
            };

            _repository.Setup(x => x.GetAsync(todoId)).ReturnsAsync(todoItem);

            var todo = await _service.GetAsync(todoId);
            var todoItemDto = _mapper.Map<ToDoDto>(todoItem);

            Assert.Equal(todo.Id, todoId);
            Assert.Equal(JsonSerializer.Serialize(todo), JsonSerializer.Serialize(todoItemDto));
        }

        [Fact]
        public async Task GetAsync_ThrowsNotFoundException_WhenNotExists()
        {
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).ThrowsAsync(new NotFoundException("test"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await _service.GetAsync(It.IsAny<int>()));
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnToDoDto_WhenExists()
        {
            var todoId = 1;
            var todoDto = new ToDoDto
            {
                Id = todoId,
                IsComplete = false,
                Name = "Name",
                Secret = "secret"
            };
            var todoItem = _mapper.Map<ToDoItem>(todoDto);


            _repository.Setup(x => x.CreateAsync(It.Is(todoItem, new TodoItemComparer()))).ReturnsAsync(todoItem);

            var todo = await _service.CreateAsync(todoDto);

            Assert.Equal(JsonSerializer.Serialize(todo), JsonSerializer.Serialize(todoDto));
        }

        [Fact]
        public async Task CreateAsync_ThrowsAlreadyExistsException_WhenTheSameID()
        {
            var todoItem = new ToDoItem
            {
                Id = 2,
                IsComplete = false,
                Name = "Name",
                Secret = "secret"
            };
            var dto = _mapper.Map<ToDoDto>(todoItem);
            _repository.Setup(x => x.CreateAsync(It.Is(todoItem, new TodoItemComparer())))
                .ThrowsAsync(new AlreadyExistsException("test"));

            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await _service.CreateAsync(dto));
        }
        
        [Fact]
        public async Task UpdateAsync_ShouldReturnToDoDto_WhenExists()
        {
            var todoDto = new ToDoDto
            {
                Id = 1,
                IsComplete = false,
                Name = "Name",
                Secret = "secret"
            };
            var todoItem = _mapper.Map<ToDoItem>(todoDto);


            _repository.Setup(x => x.UpdateAsync(It.Is(todoItem, new TodoItemComparer()))).ReturnsAsync(todoItem);

            var todo = await _service.UpdateAsync(todoDto);

            Assert.Equal(JsonSerializer.Serialize(todo), JsonSerializer.Serialize(todoDto));
        }

        [Fact]
        public async Task UpdateAsync_ThrowsNotFoundException_WhenNotExists()
        {
            var dto = new ToDoDto
            {
                Id = 2,
                IsComplete = false,
                Name = "Name",
                Secret = "secret"
            };
            _repository.Setup(x => x.UpdateAsync(It.IsAny<ToDoItem>()))
                .ThrowsAsync(new NotFoundException("test"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await _service.UpdateAsync(dto));
        }
        
        [Fact]
        public async Task DeleteAsync_ThrowsNotFoundException_WhenNotExists()
        {
            _repository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ThrowsAsync(new NotFoundException("test"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await _service.DeleteAsync(1));
        }
    }
}