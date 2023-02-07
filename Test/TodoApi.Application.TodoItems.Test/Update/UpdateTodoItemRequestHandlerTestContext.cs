using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using TodoApi.Application.Test.Common;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.Application.TodoItems.Update;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Test.Update
{
    public class UpdateTodoItemRequestHandlerTestContext :
        BaseTestContext<UpdateTodoItemRequestHandler, UpdateTodoItemRequestHandlerTestContext>
    {
        public UpdateTodoItemRequest Request { get; private set; }

        public UpdateTodoItemRequestHandlerTestContext GivenRequest()
        {
            Request = Builder<UpdateTodoItemRequest>.CreateNew()
                .With(x => x.TodoItem = Builder<TodoItem>.CreateNew().Build())
                .Build();

            return this;
        }

        public UpdateTodoItemRequestHandlerTestContext GivenEntityAccessService()
        {
            var todoItem = Builder<TodoItem>.CreateNew().Build();

            Mock.Setup<IEntityAccessService<TodoItem>, ValueTask<TodoItem>>(x => x.Find(It.IsAny<object>()))
                .ReturnsAsync(todoItem)
                .Verifiable();

            return this;
        }

        public UpdateTodoItemRequestHandlerTestContext GivenEntityModificationService()
        {
            Mock.Setup<IEntityModificationService<TodoItem>>(x => x.Update(It.IsAny<TodoItem>()))
                .Verifiable();

            return this;
        }
    }
}