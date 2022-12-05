using Microsoft.Extensions.Logging;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
        }

        [Test]
        public void GetByIdTest() {
            string expectedJson = JsonSerializer.Serialize ( new TodoItemDTO { Id = 3, IsComplete = true, Name = "Natalya" } );
            string actualJson = JsonSerializer.Serialize ( _service.Get ( 3 ).Result.Value );
            Assert.That ( actualJson, Is.EqualTo ( expectedJson ) );
        }

        [Test]
        public void UpdateTest() {
            var items = new List<TodoItemDTO> () {
                new TodoItemDTO { Id = 1, IsComplete = true, Name = "Garold" },
                new TodoItemDTO { Id = 2, IsComplete = true, Name = "Yaroslav" },
                new TodoItemDTO { Id = 3, IsComplete = true, Name = "Sara" },
                new TodoItemDTO { Id = 4, IsComplete = true, Name = "Marina" },
            };
            string expectedJson = JsonSerializer.Serialize ( items );
            _service.Update ( 3, new TodoItemDTO { Id = 3, IsComplete = true, Name = "Sara" } );
            string actualJson = JsonSerializer.Serialize ( _service.GetAll () );
            Assert.That ( actualJson, Is.EqualTo ( expectedJson ) );
        }

        [Test]
        public void CreateTest() {
            var items = new List<TodoItemDTO> () {
                new TodoItemDTO { Id = 1, IsComplete = true, Name = "Garold" },
                new TodoItemDTO { Id = 2, IsComplete = true, Name = "Yaroslav" },
                new TodoItemDTO { Id = 3, IsComplete = true, Name = "Natalya" },
                new TodoItemDTO { Id = 4, IsComplete = true, Name = "Marina" },
                new TodoItemDTO { Id = 5, IsComplete = true, Name = "Petr" },
            };
            string expectedJson = JsonSerializer.Serialize ( items );
            var code = _service.Create ( new TodoItemDTO { Id = 0, IsComplete = true, Name = "Petr" } ).Result;
            string actualJson = JsonSerializer.Serialize ( _service.GetAll () );
            Assert.Multiple ( () => {
                Assert.That ( actualJson, Is.EqualTo ( expectedJson ) );
                Assert.That ( code, Is.EqualTo ( 201 ) );
            } );
        }

        [Test]
        public void DeleteTest() {
            var items = new List<TodoItemDTO> () {
                new TodoItemDTO { Id = 1, IsComplete = true, Name = "Garold" },
                new TodoItemDTO { Id = 3, IsComplete = true, Name = "Natalya" },
                new TodoItemDTO { Id = 4, IsComplete = true, Name = "Marina" },
            };
            string expectedJson = JsonSerializer.Serialize ( items );
            var code = _service.Delete ( 2 ).Result;
            string actualJson = JsonSerializer.Serialize ( _service.GetAll () );
            Assert.Multiple ( () => {
                Assert.That ( actualJson, Is.EqualTo ( expectedJson ) );
                Assert.That ( code, Is.EqualTo ( 204 ) );
            } );
        }

        [Test]
        public void DeleteNegativeTest() {
            var items = new List<TodoItemDTO> () {
                new TodoItemDTO { Id = 1, IsComplete = true, Name = "Garold" },
                new TodoItemDTO { Id = 2, IsComplete = true, Name = "Yaroslav" },
                new TodoItemDTO { Id = 3, IsComplete = true, Name = "Natalya" },
                new TodoItemDTO { Id = 4, IsComplete = true, Name = "Marina" },
            };
            string expectedJson = JsonSerializer.Serialize ( items );
            var code = _service.Delete ( long.MaxValue ).Result;
            string actualJson = JsonSerializer.Serialize ( _service.GetAll () );
            Assert.Multiple ( () => {
                Assert.That ( actualJson, Is.EqualTo ( expectedJson ) );
                Assert.That ( code, Is.EqualTo ( 404 ) );
            } );
        }

        [Test]
        public void IsExistsTest() {
            Assert.IsTrue ( _service.IsExists(1) );
        }

        [Test]
        public void IsExistsNegativeTest() {
            Assert.IsFalse ( _service.IsExists ( long.MaxValue ) );
        }
    }
}