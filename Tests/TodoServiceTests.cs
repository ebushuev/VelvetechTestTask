using AutoMapper;
using GeekStore.API.Core.Configurations;
using Moq;
using TodoApi.Models;
using TodoApiDTO.ApiConstans;
using TodoApiDTO.DTOs;
using TodoApiDTO.Repositories.Interfaces;
using TodoApiDTO.Services;

namespace Tests
{
    [TestFixture]
    public class TodoServiceTests
    {
        private readonly IMapper _mapper;
        private readonly TodoService _service;
        private readonly Mock<ITodoRepository> _repository;

        public TodoServiceTests()
        {
            _repository = new Mock<ITodoRepository>();

            var todoProfile = new TodoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(todoProfile));
            _mapper = new Mapper(configuration);

            _service = new TodoService(_repository.Object, _mapper);
        }

        [Test]
        public void UpdateSuccessTest()
        {
            // Arrange
            var dto = new CreateUpdateItemTodoDTO { Name = "update", IsComplete = false };
            _repository.Setup(r => r.Update(It.IsAny<long>(), It.IsAny<TodoItem>()))
                .ReturnsAsync(ApiRequestStatus.Success);
                
            // Act
            var result = _service.Update(123, dto);

            // Assert
            Assert.AreEqual(ApiRequestStatus.Success, result.Result);
        }

        [Test]
        public void UpdateIncorrectIdTest()
        {
            // Arrange
            var dto = new CreateUpdateItemTodoDTO { Name = "update", IsComplete = false };
            _repository.Setup(r => r.Update(It.IsAny<long>(), It.IsAny<TodoItem>()))
                .ReturnsAsync(ApiRequestStatus.ItemDoesntExist);

            // Act
            var result = _service.Update(123, null);

            // Assert
            Assert.AreEqual(ApiRequestStatus.ItemDoesntExist, result.Result);
        }

        [Test]
        public void CreateSuccessTest()
        {
            // Arrange
            var dto = new CreateUpdateItemTodoDTO { Name = "create", IsComplete = false };
            var expectedTodo = new TodoItem { Name = dto.Name, IsComplete = dto.IsComplete };
            _repository.Setup(r => r.Create(It.IsAny<TodoItem>()))
                .ReturnsAsync(expectedTodo);

            // Act
            var result = _service.Create(dto);

            // Assert
            var modelAfterUpdate = result.Result;
            Assert.NotNull(modelAfterUpdate);
            Assert.AreEqual(dto.IsComplete, modelAfterUpdate.IsComplete);
            Assert.AreEqual(dto.Name, modelAfterUpdate.Name);
        }
    }
}
