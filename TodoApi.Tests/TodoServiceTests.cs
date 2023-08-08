using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.BLL;
using TodoApi.BLL.DTO;
using TodoApi.BLL.Interfaces;
using TodoApi.DAL.EF;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Interfaces;
using TodoApi.DAL.Repositories;
using Xunit;

namespace TodoApi.Tests
{
    public class TodoServiceTests
    {
        private static DbContextOptions<TodoContext> GetOptions(string dbName)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var context = new TodoContext(options))
            {
                context.TodoItems.Add(new TodoItem() { Id = 1, Name = "Finish test task", IsComplete = false });
                context.TodoItems.Add(new TodoItem() { Id = 2, Name = "Take a break", IsComplete = true });
                context.TodoItems.Add(new TodoItem() { Id = 3, Name = "Walk little bit", IsComplete = false });
                context.TodoItems.Add(new TodoItem() { Id = 4, Name = "Learn React", IsComplete = true });
                context.SaveChanges();
            }

            return options;
        }

        [Fact]
        public void TestGetAllWithFourItems()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestGetAllWithFourItems")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);
                
                // Act
                var todoItems = service.GetAllAsync().Result;

                // Assert
                Assert.Equal(4, todoItems.Count);
                Assert.True(todoItems[0].Id == 1);
                Assert.True(todoItems[1].Id == 2);
                Assert.True(todoItems[2].Id == 3);
                Assert.True(todoItems[3].Id == 4);
                Assert.True(todoItems[0].Name == "Finish test task");
                Assert.True(todoItems[1].Name == "Take a break");
                Assert.True(todoItems[2].Name == "Walk little bit");
                Assert.True(todoItems[3].Name == "Learn React");
                Assert.False(todoItems[0].IsComplete);
                Assert.True(todoItems[1].IsComplete);
                Assert.False(todoItems[2].IsComplete);
                Assert.True(todoItems[3].IsComplete);
            }
        }

        [Fact]
        public void TestGetAllWithEmptyData()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TestGetAllWithEmptyData")
                .Options;

            using (var context = new TodoContext(options))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItems = service.GetAllAsync().Result;

                // Assert
                Assert.Empty(todoItems);
            }
        }

        [Fact]
        public void TestGetWhenTodoExist()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestGetWhenTodoExist")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItem = service.GetAsync(2).Result;
                
                // Assert
                Assert.True(todoItem.Id == 2);
                Assert.True(todoItem.Name == "Take a break");
                Assert.True(todoItem.IsComplete);
            }
        }

        [Fact]
        public void TestGetWhenTodoNotExist()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestGetWhenTodoNotExist")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItem = service.GetAsync(5).Result;

                // Assert
                Assert.Null(todoItem);
            }
        }

        [Fact]
        public void TestAddValid()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestAddValid")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItem = service.AddAsync(
                    new TodoItemDTO
                    {
                        Name = "TestAdd",
                        IsComplete = false
                    }).Result;

                // Assert
                Assert.True(todoItem.Id == 5);
                Assert.True(todoItem.Name == "TestAdd");
                Assert.False(todoItem.IsComplete);
            }
        }

        [Fact]
        public void TestAddInvalid()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestAddInvalid")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItem = service.AddAsync(
                    new TodoItemDTO
                    {
                        Name = string.Empty,
                        IsComplete = false
                    }).Result;

                // Assert
                Assert.Null(todoItem);
            }
        }

        [Fact]
        public void TestAddInvalid2()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestAddInvalid2")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItem = service.AddAsync(
                    new TodoItemDTO
                    {
                        Name = "  ",
                        IsComplete = false
                    }).Result;

                // Assert
                Assert.Null(todoItem);
            }
        }

        [Fact]
        public void TestAddInvalid3()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestAddInvalid3")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItem = service.AddAsync(
                    new TodoItemDTO
                    {
                        Name = "\t",
                        IsComplete = false
                    }).Result;

                // Assert
                Assert.Null(todoItem);
            }
        }

        [Fact]
        public void TestAddInvalid4()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestAddInvalid4")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                var todoItem = service.AddAsync(
                    new TodoItemDTO
                    {
                        Id = 1,
                        Name = "CorrectField",
                        IsComplete = false
                    }).Result;

                // Assert
                Assert.Null(todoItem);
            }
        }

        [Fact]
        public void TestUpdateValid()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestUpdateValid")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isUpdated = service.UpdateAsync(
                    new TodoItemDTO
                    {
                        Id = 3,
                        Name = "Run a little bit",
                        IsComplete = true
                    }).Result;
                var todoItem = context.TodoItems.FindAsync(3).Result;

                // Assert
                Assert.True(isUpdated);
                Assert.True(todoItem.Id == 3);
                Assert.True(todoItem.Name == "Run a little bit");
                Assert.True(todoItem.IsComplete);
            }
        }

        [Fact]
        public void TestUpdateInvalid()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestUpdateInvalid")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isUpdated = service.UpdateAsync(
                    new TodoItemDTO
                    {
                        Id = 3,
                        Name = string.Empty,
                        IsComplete = true
                    }).Result;

                // Assert
                Assert.False(isUpdated);
            }
        }

        [Fact]
        public void TestUpdateInvalid2()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestUpdateInvalid2")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isUpdated = service.UpdateAsync(
                    new TodoItemDTO
                    {
                        Id = 3,
                        Name = " ",
                        IsComplete = true
                    }).Result;

                // Assert
                Assert.False(isUpdated);
            }
        }

        [Fact]
        public void TestUpdateInvalid3()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestUpdateInvalid3")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isUpdated = service.UpdateAsync(
                    new TodoItemDTO
                    {
                        Id = 3,
                        Name = "\t",
                        IsComplete = true
                    }).Result;

                // Assert
                Assert.False(isUpdated);
            }
        }

        [Fact]
        public void TestUpdateInvalidId()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestUpdateInvalidId")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isUpdated = service.UpdateAsync(
                    new TodoItemDTO
                    {
                        Id = 5,
                        Name = "ValidField",
                        IsComplete = true
                    }).Result;

                // Assert
                Assert.False(isUpdated);
            }
        }

        [Fact]
        public void TestRemoveValid()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestRemoveValid")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isDeleted = service.RemoveAsync(1).Result;

                // Assert
                Assert.True(isDeleted);
                Assert.True(context.TodoItems.FirstOrDefault(t => t.Id == 1) == null);
            }
        }

        [Fact]
        public void TestRemoveInvalid()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestRemoveInvalid")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isDeleted = service.RemoveAsync(5).Result;

                // Assert
                Assert.False(isDeleted);
            }
        }

        [Fact]
        public void TestRemoveInvalid2()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestRemoveInvalid2")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isDeleted = service.RemoveAsync(0).Result;

                // Assert
                Assert.False(isDeleted);
            }
        }

        [Fact]
        public void TestRemoveInvalid3()
        {
            // Arrange
            using (var context = new TodoContext(GetOptions("TestRemoveInvalid3")))
            {
                IUnitOfWork uow = new UnitOfWork(context);
                ITodoService service = new TodoService(uow);

                // Act
                bool isDeleted = service.RemoveAsync(-1).Result;

                // Assert
                Assert.False(isDeleted);
            }
        }
    }
}
