using AutoMapper;
using GeekStore.API.Core.Configurations;
using TodoApi.Models;
using TodoApiDTO.DTOs;

namespace Tests
{
    [TestFixture]
    public class TodoMapperTests
    {
        private readonly IMapper _mapper;

        public TodoMapperTests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TodoProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Test]
        public void ModelToDtoTest()
        {
            var todo = new TodoItem { Id = 1, IsComplete = true, Name = "model", Secret = "secret" };
            var dto = _mapper.Map<TodoItemDTO>(todo);

            Assert.NotNull(dto);
            Assert.IsTrue(dto.IsComplete);
            Assert.AreEqual(todo.Id, dto.Id);
            Assert.AreEqual(todo.Name, dto.Name);
        }

        [Test]
        public void DtoToModelTest()
        {
            var dto = new TodoItemDTO { Id = 1, IsComplete = true, Name = "model" };
            var todo = _mapper.Map<TodoItem>(dto);

            Assert.NotNull(todo);
            Assert.IsTrue(todo.IsComplete);
            Assert.AreEqual(dto.Id, todo.Id);
            Assert.AreEqual(todo.Name, dto.Name);
            Assert.Null(todo.Secret);
        }

        [Test]
        public void CreateUpdateDtoToModelTest() 
        {
            var dto = new CreateUpdateItemTodoDTO { Name = "postput", IsComplete = false };
            var todo = new TodoItem { Id = 1 };

            _mapper.Map(dto, todo);

            Assert.IsFalse(todo.IsComplete);
            Assert.AreEqual(dto.Name, todo.Name);
            Assert.AreEqual(1, todo.Id);

        }
    }
}