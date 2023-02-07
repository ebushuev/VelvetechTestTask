using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using TodoApi.Application.Test.Common;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.Application.TodoItems.Create;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Test.Create
{
    public class CreateTodoItemRequestHandlerTestContext :
        BaseTestContext<CreateTodoItemRequestHandler, CreateTodoItemRequestHandlerTestContext>
    {
        public CreateTodoItemRequest Request { get; private set; }

        private TodoItem TodoItem { get; set; }

        public CreateTodoItemRequestHandlerTestContext GivenRequest()
        {
            TodoItem = Builder<TodoItem>.CreateNew().Build();

            Request = Builder<CreateTodoItemRequest>.CreateNew()
                .With(x => x.TodoItem = TodoItem)
                .Build();

            return this;
        }

        public CreateTodoItemRequestHandlerTestContext GivenEntityModificationService()
        {
            Mock.Setup<IEntityModificationService<TodoItem>, ValueTask<TodoItem>>(x => x.Create(It.IsAny<TodoItem>()))
                .ReturnsAsync(TodoItem)
                .Verifiable();

            return this;
        }
    }
}