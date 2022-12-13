using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.BL.DTOs;
using TodoApp.Common.Contract;
using TodoApp.Services;

namespace TodoApp.Tests.Mocks.Services
{
    public class MockTodoService : Mock<ITodoService>
    {
        public MockTodoService MockAddItem(ApiResponse<TodoItemDTO> result)
        {
            Setup(x => x.AddItem(It.IsAny<TodoItemDTO>())).ReturnsAsync(result);

            return this;

        }

        public MockTodoService MockDelete(ApiResponse result)
        {
            Setup(x => x.Delete(It.IsAny<long>())).ReturnsAsync(result);

            return this;
        }

        public MockTodoService MockGetAll()
        {
            Setup(x => x.GetAll());

            return this;
        }

        public MockTodoService MockGetItemById(ApiResponse<TodoItemDTO> result)
        {
            Setup(x => x.GetItemById(It.IsAny<long>())).ReturnsAsync(result);

            return this;
        }

        public MockTodoService MockUpdateItem(ApiResponse result)
        {
            Setup(x => x.UpdateItem(It.IsAny<long>(), It.IsAny<TodoItemDTO>()))
                .ReturnsAsync(result);

            return this;
        }
    }
}
