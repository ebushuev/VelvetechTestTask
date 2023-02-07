using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using TodoApi.Application.Test.Common;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.Application.TodoItems.GetAll;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Test.GetAll
{
    public class GetTodoItemsRequestHandlerTestContext :
        BaseTestContext<GetTodoItemsRequestHandler, GetTodoItemsRequestHandlerTestContext>
    {
        public GetTodoItemsRequest Request { get; private set; }

        public GetTodoItemsRequestHandlerTestContext GivenRequest()
        {
            Request = Builder<GetTodoItemsRequest>.CreateNew().Build();

            return this;
        }

        public GetTodoItemsRequestHandlerTestContext GivenEntitiesInDb(int count)
        {
            var items = Builder<TodoItem>.CreateListOfSize(count).Build().ToList();

            Mock.Setup<IEntityAccessService<TodoItem>, ValueTask<List<TodoItem>>>(x => x.GetAll())
                .ReturnsAsync(items)
                .Verifiable();

            return this;
        }
    }
}