using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using TodoApi.Application.Test.Common;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.Application.TodoItems.Get;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Test.Get
{
    public class GetTodoItemRequestHandlerTestContext :
        BaseTestContext<GetTodoItemRequestHandler, GetTodoItemRequestHandlerTestContext>
    {
        public GetTodoItemRequest Request { get; private set; }
        
        private TodoItem TodoItem { get; set; }

        public GetTodoItemRequestHandlerTestContext GivenRequest()
        {
            Request = Builder<GetTodoItemRequest>.CreateNew().Build();

            return this;
        }

        public GetTodoItemRequestHandlerTestContext GivenEntityAccessService()
        {
            TodoItem = Builder<TodoItem>.CreateNew().Build();

            Mock.Setup<IEntityAccessService<TodoItem>, ValueTask<TodoItem>>(x => x.Find(It.IsAny<object>()))
                .ReturnsAsync(TodoItem)
                .Verifiable();

            return this;
        }
    }
}