using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.Models;

namespace TodoTests
{
    public class TodoTests
    {
        private IMapper _mapper;
        private List<TodoItem> _todoItems;
        private Mock<DbSet<TodoItem>> _dbSetMock;
        private Mock<TodoContext> _contextMock;
        private TodoItemsController _controller;

        [SetUp]
        public void Setup()
        {
            ResetTodoList();

            _dbSetMock = _todoItems.BuildMock().BuildMockDbSet();
            _dbSetMock.Setup(x => x.FindAsync(1L)).ReturnsAsync(_todoItems.Find(e => e.Id == 1L));
            _dbSetMock.Setup(x => x.Add(It.IsAny<TodoItem>())).Callback<TodoItem>((s) => _todoItems.Add(s));
            _dbSetMock.Setup(x => x.Remove(It.IsAny<TodoItem>())).Callback<TodoItem>((s) => _todoItems.Remove(s));

            _contextMock = new Mock<TodoContext>();
            _contextMock.Setup(x => x.TodoItems).Returns(_dbSetMock.Object);

            _mapper = new Mapper(GetMapperConfiguration());

            _controller = new TodoItemsController(_contextMock.Object, _mapper);
        }

        private void ResetTodoList()
        {
            _todoItems = TodoTestDataHelper.GetTodoItemsList();
        }

        public MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItem, TodoItemDTO>()
                    .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
                    .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                    .ForMember(x => x.IsComplete, opt => opt.MapFrom(o => o.IsComplete));
                cfg.CreateMap<TodoItemDTO, TodoItem>()
                    .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
                    .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                    .ForMember(x => x.IsComplete, opt => opt.MapFrom(o => o.IsComplete))
                    .ForMember(x => x.Secret, opt => opt.Ignore());
            });
        }

        [Test]
        public async Task GetTodoItemsTest()
        {
            var controllerResult = await _controller.GetTodoItems();
            var todoItemsDTO = (List<TodoItemDTO>)(controllerResult.Result as OkObjectResult).Value;

            Assert.NotNull(todoItemsDTO);
            Assert.AreEqual(3, todoItemsDTO.Count());
            Assert.AreEqual(todoItemsDTO[0], _mapper.Map<TodoItemDTO>(_todoItems[0]));
            Assert.AreEqual(todoItemsDTO[1], _mapper.Map<TodoItemDTO>(_todoItems[1]));
            Assert.AreEqual(todoItemsDTO[2], _mapper.Map<TodoItemDTO>(_todoItems[2]));
        }

        [Test]
        public async Task GetTodoItemTest()
        {
            var controllerResult = await _controller.GetTodoItem(1);
            var todoItemDTO = (TodoItemDTO)(controllerResult.Result as OkObjectResult).Value;

            Assert.NotNull(todoItemDTO);
            Assert.AreEqual(1L, todoItemDTO.Id);
            Assert.AreEqual(todoItemDTO, _mapper.Map<TodoItemDTO>(_todoItems[1]));
        }

        [Test]
        public async Task CreateTodoItemTest()
        {
            var todoListCount = _todoItems.Count;

            var todoItemToCreate = new TodoItemDTO();
            await _controller.CreateTodoItem(todoItemToCreate);

            var controllerResult = await _controller.GetTodoItems();
            var todoItemsDTO = (List<TodoItemDTO>)(controllerResult.Result as OkObjectResult).Value;
            Assert.NotNull(todoItemsDTO);
            Assert.AreEqual(todoListCount + 1, todoItemsDTO.Count());

            ResetTodoList();
        }

        [Test]
        public async Task UpdateTodoItemTest()
        {
            var getItemControllerResult = await _controller.GetTodoItem(1);
            var todoItemDTO = (TodoItemDTO)(getItemControllerResult.Result as OkObjectResult).Value;

            Assert.NotNull(todoItemDTO);
            Assert.AreEqual(1L, todoItemDTO.Id);

            var wasCompleted = todoItemDTO.IsComplete;
            todoItemDTO.IsComplete = !wasCompleted;

            var updateItemControllerResult = await _controller.UpdateTodoItem(1, todoItemDTO);
            
            getItemControllerResult = await _controller.GetTodoItem(1);
            todoItemDTO = (TodoItemDTO)(getItemControllerResult.Result as OkObjectResult).Value;

            Assert.NotNull(todoItemDTO);
            Assert.AreNotEqual(todoItemDTO.IsComplete, wasCompleted);

            ResetTodoList();
        }

        [Test]
        public async Task DeleteTodoItemTest()
        {
            var todoListCount = _todoItems.Count;
            await _controller.DeleteTodoItem(1);

            var controllerResult = await _controller.GetTodoItems();
            var todoItemsDTO = (List<TodoItemDTO>)(controllerResult.Result as OkObjectResult).Value;
            Assert.NotNull(todoItemsDTO);
            Assert.AreEqual(todoListCount - 1, todoItemsDTO.Count());

            ResetTodoList();
        }
    }
}