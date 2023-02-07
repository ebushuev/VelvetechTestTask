using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TodoApi.Application.TodoItems.Get;

namespace TodoApi.Application.TodoItems.Test.Get
{
    public class GetTodoItemRequestHandlerTest
    {
        private GetTodoItemRequestHandlerTestContext _context;

        private GetTodoItemRequestHandler _subject;

        [SetUp]
        public void Setup()
        {
            _context = new GetTodoItemRequestHandlerTestContext();

            _subject = _context.CreateSubject();
        }

        [Test]
        public async Task Handle_GivenRequest_SuccessfulResult()
        {
            _context
                .GivenRequest()
                .GivenEntityAccessService();

            var result = await _subject.Handle(_context.Request, CancellationToken.None);

            _context
                .VerifyResultIsNotNull(result)
                .VerifyScenario();
        }
    }
}