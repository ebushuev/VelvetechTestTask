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
    public class UpdateTodoItemRequestValidatorTestContext :
        BaseTestContext<UpdateTodoItemRequestValidator, UpdateTodoItemRequestValidatorTestContext>
    {
        public UpdateTodoItemRequest Request { get; private set; }

        public UpdateTodoItemRequestValidatorTestContext GivenValidRequest()
        {
            Request = Builder<UpdateTodoItemRequest>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.TodoItem = Builder<TodoItem>.CreateNew()
                    .With(x => x.Id = 1)
                    .Build())
                .Build();

            return this;
        }

        public UpdateTodoItemRequestValidatorTestContext GivenInvalidRequest()
        {
            Request = Builder<UpdateTodoItemRequest>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.TodoItem = Builder<TodoItem>.CreateNew()
                    .With(x => x.Id = 2)
                    .Build())
                .Build();

            return this;
        }

        public UpdateTodoItemRequestValidatorTestContext GivenEntityInDb()
        {
            var todoItem = Builder<TodoItem>.CreateNew().Build();

            Mock.Setup<IEntityAccessService<TodoItem>, ValueTask<TodoItem>>(x => x.Find(It.IsAny<object>()))
                .ReturnsAsync(todoItem)
                .Verifiable();

            return this;
        }
    }
}