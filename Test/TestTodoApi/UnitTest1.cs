using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.Json;
using TodoApi.Models;
using TodoApiDTO.BL;
using TodoApiDTO.DAL.Repositories;

namespace TestTodoApi {
    public class Tests {
        private ServiceTodoItemDTO _service;

        [SetUp]
        public void Setup() {
            var mockUoW = new MockUnitOfWork ();
            var mockLogger = new Microsoft.Extensions.Logging.Logger<ServiceTodoItemDTO> ( LoggerFactory.Create ( builder => builder.ClearProviders () ) );
            _service = new ServiceTodoItemDTO ( mockUoW, mockLogger );
        }

        [Test]
        public void GetAllTest() {
            var items = new List<TodoItemDTO> () {
                new TodoItemDTO { Id = 1, IsComplete = true, Name = "Garold" },
                new TodoItemDTO { Id = 2, IsComplete = true, Name = "Yaroslav" },
                new TodoItemDTO { Id = 3, IsComplete = true, Name = "Natalya" },
                new TodoItemDTO { Id = 4, IsComplete = true, Name = "Marina" },
            };
            string expectedJson = JsonSerializer.Serialize ( items );
            string actualJson = JsonSerializer.Serialize ( _service.GetAll () );
            Assert.That ( actualJson, Is.EqualTo ( expectedJson ) );
            Assert.Pass ();
        }
    }
}