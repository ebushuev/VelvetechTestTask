using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoApp.BL.DTOs;
using TodoApp.Common.Contract;
using TodoApp.Common.Logging;
using TodoApp.Data.Entities;
using TodoApp.Services;
using TodoApp.Tests.Mocks.Repositories;
using Xunit;

namespace TodoApp.Tests.Services
{
    public class TodoServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<TodoService> _logger;

        public TodoServiceTests(IMapper mapper, IAppLogger<TodoService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }


        [Fact]
        public async void TodoService_GetAll_ShouldGetAllData()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            //Act

            var results = await todoService.GetAll();

            Assert.Equal(mockItems, results.Data);
        }

        [Fact]
        public async void TodoService_GetAll_DataNotFound()
        {
            var mockItems = new List<TodoItemDTO>();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            //Act

            var results = await todoService.GetAll();

            Assert.Throws<GlobalException>(() => results);
        }

        [Fact]
        public async void TodoService_GetItemById_ShouldGetItemById()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            var expected = mockItems.FirstOrDefault(x => x.Id == 1);

            //Act

            var results = await todoService.GetItemById(1);

            Assert.Equal(expected, results.Data);
        }


        [Fact]
        public async void TodoService_GetItemById_DataNotFound()
        {
            var mockItems = new List<TodoItemDTO>();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            //Act

            var results = await todoService.GetItemById(1);

            Assert.Throws<GlobalException>(() => results);
        }

        [Fact]
        public async void TodoService_Delete_ShouldDeleteItem()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            //Act
            var results = await todoService.Delete(1);

            Assert.IsType<ApiResponse>(results);

        }

        [Fact]
        public async void TodoService_Delete_DataNotFound()
        {
            var mockItems = new List<TodoItemDTO>();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            //Act

            var results = await todoService.Delete(1);

            Assert.Throws<GlobalException>(() => results);
        }

        [Fact]
        public async void TodoService_AddItem_ShouldAddItem()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            var item = new TodoItemDTO
            {
                Id = 4,
                Name = "Task4",
                IsComplete = false
            };

            //Act
            var result = await todoService.AddItem(item);

            Assert.IsType<ApiResponse>(result);
        }

        [Fact]
        public async void TodoService_AddItem_ShouldAlreadyExist()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            var item = new TodoItemDTO
            {
                Id = 4,
                Name = "Task4",
                IsComplete = false
            };

            //Act
            var result = await todoService.AddItem(item);

            Assert.Throws<GlobalException>(() => result);
        }

        [Fact]
        public async void TodoService_UpdateItem_ShouldUpdateItem()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            var item = new TodoItemDTO
            {
                Id = 3,
                Name = "Task33",
                IsComplete = false
            };

            //Act
            var result = await todoService.UpdateItem(3, item);

            Assert.IsType<ApiResponse>(result);
        }

        [Fact]
        public async void TodoService_UpdateItem_BadRequest()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            var item = new TodoItemDTO
            {
                Id = 3,
                Name = "Task33",
                IsComplete = false
            };

            //Act
            var result = await todoService.UpdateItem(2, item);

            Assert.Throws<GlobalException>(() => result);
        }

        [Fact]
        public async void TodoService_UpdateItem_DataNotFound()
        {
            var mockItems = GetMockItems();

            var mockTodoItemRepo = new MockTodoRepository().GetAll(_mapper.Map<List<TodoItem>>(mockItems));

            var todoService = new TodoService(_logger, _mapper, mockTodoItemRepo.Object);

            var item = new TodoItemDTO
            {
                Id = 4,
                Name = "Task4",
                IsComplete = false
            };

            //Act
            var result = await todoService.UpdateItem(4, item);

            Assert.Throws<GlobalException>(() => result);
        }


        private List<TodoItemDTO> GetMockItems()
        {
            return new List<TodoItemDTO>()
            {
                new TodoItemDTO()
                {
                    Id = 1,
                    Name = "Task1",
                    IsComplete = false
                },

                new TodoItemDTO()
                {
                    Id = 2,
                    Name = "Task2",
                    IsComplete = true
                },

                new TodoItemDTO()
                {
                    Id = 3,
                    Name = "Task3",
                    IsComplete = true
                }, 
            };
        }
    }
}
