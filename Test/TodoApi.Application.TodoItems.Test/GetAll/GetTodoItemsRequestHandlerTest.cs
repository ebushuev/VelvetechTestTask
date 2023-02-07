using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using TodoApi.Application.TodoItems.GetAll;

namespace TodoApi.Application.TodoItems.Test.GetAll
{
    public class GetTodoItemsRequestHandlerTest
    {
        private GetTodoItemsRequestHandlerTestContext _context;

        private GetTodoItemsRequestHandler _subject;

        [SetUp]
        public void Setup()
        {
            _context = new GetTodoItemsRequestHandlerTestContext();

            _subject = _context.CreateSubject();
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(10)]
        public async Task Handle_GivenRequest_GivenSeveralEntitiesInDb_SuccessfulResult(int entitiesCount)
        {
            _context
                .GivenRequest()
                .GivenEntitiesInDb(entitiesCount);

            var result = await _subject.Handle(_context.Request, CancellationToken.None);

            _context
                .Verify(() => result.Count.ShouldBe(entitiesCount))
                .VerifyScenario();
        }
    }
}