using Autofac;
using Autofac.Extras.Moq;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.BusinessLayer;
using TodoApiDTO.DataAccessLayer;
using TodoApiDTO.Extensions;
using Xunit;

namespace TodoApiDTO.Tests.BusinessLayerTests
{
    public class BusinessLayerTests
    {
        [Fact]
        public void GetAllTodos()
        {
            using (AutoMock mock = AutoMock.GetStrict())
            {
                mock.Mock<IDataAccessLayer>()
                    .Setup(x => x.GetTodoItems())
                    .Returns(GetSampleTodoItems());

                var cls = mock.Create<BusinessLayerImplementation>();
                OperationEventResult expected = new OperationEventResult
                {
                    Type = OperationEventType.Done,
                    Payload = GetSampleTodoItems().Result.Select(x => x.ToDTO()).ToList(),
                };

                OperationEventResult actual = cls.ExecuteTodoItemFetch().Result;

                Assert.True(actual != null);
                Assert.Equivalent(expected, actual);
            };
        }

        [Theory]
        [InlineData(1, OperationEventType.Done)]
        [InlineData(2, OperationEventType.Done)]
        [InlineData(3, OperationEventType.Done)]
        [InlineData(4, OperationEventType.NotFound)]
        [InlineData(-1, OperationEventType.NotFound)]
        [InlineData(10000, OperationEventType.NotFound)]
        [InlineData(long.MaxValue, OperationEventType.NotFound)]
        public void GetTodoById(long id, OperationEventType type)
        {
            using (AutoMock mock = AutoMock.GetStrict())
            {
                mock.Mock<IDataAccessLayer>()
                    .Setup(x => x.GetTodoItem(id))
                    .Returns(Task.FromResult(GetSampleTodoItems().Result.FirstOrDefault(x => x.Id == id)));

                var cls = mock.Create<BusinessLayerImplementation>();
                OperationEventResult expected = new OperationEventResult
                {
                    Type = type,
                    Payload = GetSampleTodoItems().Result.FirstOrDefault(x => x.Id == id) != null ?
                    new List<TodoItemDTO?>()
                    {
                        GetSampleTodoItems().Result.FirstOrDefault(x => x.Id == id)?.ToDTO()
                    }
                    :
                    null
                };

                OperationEventResult actual = cls.ExecuteTodoItemFetch(id).Result;

                Assert.Equivalent(expected, actual);
                Assert.True(actual.Payload == null || actual.Payload.Count == 1);
            }
        }

        [Theory]
        [InlineData(1, OperationEventType.NoContent)]
        [InlineData(2, OperationEventType.NoContent)]
        [InlineData(3, OperationEventType.NoContent)]
        [InlineData(4, OperationEventType.NotFound)]
        [InlineData(-1, OperationEventType.NotFound)]
        [InlineData(10000, OperationEventType.NotFound)]
        [InlineData(long.MaxValue, OperationEventType.NotFound)]
        public async void DeleteTodoById(long id, OperationEventType type)
        {

            using (AutoMock mock = AutoMock.GetStrict())
            {
                TodoItem? target = GetSampleTodoItems().Result.FirstOrDefault(x => x.Id == id);

                mock.Mock<IDataAccessLayer>()
                    .Setup(x => x.GetTodoItem(id))
                    .Returns(Task.FromResult(target))
                    .Verifiable();

                mock.Mock<IDataAccessLayer>()
                    .When(() => target != null)
                    .Setup(x => x.DeleteTodoItem(target))
                    .Returns(Task.FromResult(target))
                    .Verifiable();

                var cls = mock.Create<BusinessLayerImplementation>();

                OperationEventResult result = await cls.ExecuteTodoItemDelete(id);

                mock.Mock<IDataAccessLayer>()
                    .VerifyAll();

                Assert.Equal(type, result.Type);
            }
        }

        [Theory]
        [InlineData(-1, OperationEventType.BadRequest, 0, "Feed The Crocodile", false)]
        [InlineData(-134, OperationEventType.BadRequest, 0, "Feed The Crocodile", true)]
        [InlineData(123, OperationEventType.BadRequest, 0, "Feed The Crocodile", false)]
        [InlineData(1, OperationEventType.NoContent, 1, "Feed the dog", true)]
        [InlineData(2, OperationEventType.NoContent, 2, "Feed the cat", false)]
        [InlineData(3, OperationEventType.NoContent, 3, "Feed the eagle", false)]
        [InlineData(long.MaxValue, OperationEventType.NotFound, long.MaxValue, "Feed the dog", true)]
        [InlineData(4, OperationEventType.NotFound, 4, "Feed the dog", true)]
        [InlineData(1000, OperationEventType.NotFound, 1000, "Feed the dog", true)]
        public async void UpdateTodo(long id, OperationEventType type, long dtoId, string newName, bool isComplete)
        {
            using (AutoMock mock = AutoMock.GetStrict())
            {
                TodoItemDTO dto = new TodoItemDTO
                {
                    Id = dtoId,
                    Name = newName,
                    IsComplete = isComplete
                };

                mock.Mock<IDataAccessLayer>()
                    .When(()=>id == dto.Id)
                    .Setup(x => x.GetTodoItem(id))
                    .Returns(Task.FromResult(GetSampleTodoItems().Result.FirstOrDefault(x=>x.Id == id)))
                    .Verifiable();

                mock.Mock<IDataAccessLayer>()
                    .When(() => id == dto.Id)
                    .Setup(x => x.UpdateTodoItem(dto))
                    .Returns(Task.FromResult(dto))
                    .Verifiable();

                var cls = mock.Create<BusinessLayerImplementation>();

                OperationEventResult result = await cls.ExecuteTodoItemUpdate(id, dto);

                mock.Mock<IDataAccessLayer>()
                    .VerifyAll();

                Assert.Equal(type, result.Type);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void CreateTodo(long id)
        {
            using(AutoMock mock = AutoMock.GetStrict())
            {
                TodoItem toBeCreated = GetSampleTodoItems().Result.First(x=>x.Id == id);
                TodoItemDTO toBeCreatedDTO = toBeCreated.ToDTO();

                mock.Mock<IDataAccessLayer>()
                    .Setup(x => x.CreateTodoItem(toBeCreatedDTO))
                    .Returns(Task.FromResult(toBeCreated))
                    .Verifiable();

                var cls = mock.Create<BusinessLayerImplementation>();

                OperationEventResult result = await cls.ExecuteTodoItemCreate(toBeCreatedDTO);

                mock.Mock<IDataAccessLayer>()
                    .VerifyAll();
                Assert.True(result.Payload != null);
            }
        }

        private static Task<List<TodoItem>> GetSampleTodoItems()
        {
            List<TodoItem> todoList = new List<TodoItem>
            {
                new TodoItem
                {
                    Id = 1,
                    Name = "Feed the dog",
                    IsComplete = false
                },
                new TodoItem
                {
                    Id = 2,
                    Name = "Cleanup a bedroom",
                    IsComplete = false
                },
                new TodoItem
                {
                    Id = 3,
                    Name = "Go for a walk",
                    IsComplete = false
                },
            };

            return Task.FromResult(todoList);
        }
    }
}
