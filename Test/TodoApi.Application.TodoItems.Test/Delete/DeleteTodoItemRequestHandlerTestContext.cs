using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using TodoApi.Application.Test.Common;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.Application.TodoItems.Delete;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Test.Delete
{
    public class DeleteTodoItemRequestHandlerTestContext :
        BaseTestContext<DeleteTodoItemRequestHandler, DeleteTodoItemRequestHandlerTestContext>
    {
        public DeleteTodoItemRequest Request { get; private set; }

        private TodoItem TodoItem { get; set; }

        public DeleteTodoItemRequestHandlerTestContext GivenRequest()
        {
            Request = Builder<DeleteTodoItemRequest>.CreateNew().Build();

            return this;
        }

        public DeleteTodoItemRequestHandlerTestContext GivenEntityModificationService()
        {
            Mock.Setup<IEntityModificationService<TodoItem>>(x => x.Remove(TodoItem)).Verifiable();

            return this;
        }

        public DeleteTodoItemRequestHandlerTestContext GivenEntityAccessService()
        {
            TodoItem = Builder<TodoItem>.CreateNew().Build();

            Mock.Setup<IEntityAccessService<TodoItem>, ValueTask<TodoItem>>(x => x.Find(It.IsAny<object>()))
                .ReturnsAsync(TodoItem)
                .Verifiable();

            return this;
        }
    }
}