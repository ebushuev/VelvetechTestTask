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
    public class DeleteTodoItemRequestValidatorTestContext :
        BaseTestContext<DeleteTodoItemRequestValidator, DeleteTodoItemRequestValidatorTestContext>
    {
        public DeleteTodoItemRequest Request { get; private set; }

        public DeleteTodoItemRequestValidatorTestContext GivenValidRequest()
        {
            Request = Builder<DeleteTodoItemRequest>.CreateNew().Build();

            return this;
        }

        public DeleteTodoItemRequestValidatorTestContext GivenEntityInDb()
        {
            var todoItem = Builder<TodoItem>.CreateNew().Build();

            Mock.Setup<IEntityAccessService<TodoItem>, ValueTask<TodoItem>>(x => x.Find(It.IsAny<object>()))
                .ReturnsAsync(todoItem)
                .Verifiable();

            return this;
        }
    }
}