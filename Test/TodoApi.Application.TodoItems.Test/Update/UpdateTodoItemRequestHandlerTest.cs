using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TodoApi.Application.TodoItems.Update;

namespace TodoApi.Application.TodoItems.Test.Update
{
    public class UpdateTodoItemRequestHandlerTest
    {
        private UpdateTodoItemRequestHandlerTestContext _context;

        private UpdateTodoItemRequestHandler _subject;

        [SetUp]
        public void Setup()
        {
            _context = new UpdateTodoItemRequestHandlerTestContext();

            _subject = _context.CreateSubject();
        }

        [Test]
        public async Task Handle_GivenRequest_SuccessfulResult()
        {
            _context
                .GivenRequest()
                .GivenEntityAccessService()
                .GivenEntityModificationService()
                .GivenCommitter();

            await _subject.Handle(_context.Request, CancellationToken.None);

            _context.VerifyScenario();
        }
    }
}