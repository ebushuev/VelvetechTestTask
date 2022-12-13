using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.BL.DTOs;
using TodoApp.Common.Contract;
using TodoApp.Data.Entities;
using TodoApp.Data.Todo.Repositories;

namespace TodoApp.Tests.Mocks.Repositories
{
    public class MockTodoRepository : Mock<ITodoRepository>
    {
        public MockTodoRepository MockAddAsync()
        {
            Setup(x => x.AddAsync(It.IsAny<TodoItem>()));

            return this;
        }

        public MockTodoRepository MockUpdate()
        {
            Setup(x => x.Update(It.IsAny<TodoItem>()));

            return this;
        }

        public MockTodoRepository Remove()
        {
            Setup(x => x.Remove(It.IsAny<TodoItem>()));

            return this;
        }

        public MockTodoRepository Get(TodoItem result)
        {
            Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(result);

            return this;
        }

        public MockTodoRepository GetAll(List<TodoItem> results)
        {
            Setup(x => x.GetAll()).ReturnsAsync(results);

            return this;
        }

        public MockTodoRepository SaveAll(bool result)
        {
            Setup(x => x.SaveAll()).ReturnsAsync(result);

            return this;
        }
    }
}
