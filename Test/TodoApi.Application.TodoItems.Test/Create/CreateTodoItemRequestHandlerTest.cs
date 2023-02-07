using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TodoApi.Application.TodoItems.Create;

namespace TodoApi.Application.TodoItems.Test.Create
{
    public class CreateTodoItemRequestHandlerTest
    {
        private CreateTodoItemRequestHandler _subject;

        private CreateTodoItemRequestHandlerTestContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new CreateTodoItemRequestHandlerTestContext();
            
            _subject = _context.CreateSubject();
        }

        [Test]
        public async Task Handle_GivenRequest_SuccessfulResult()
        {
            _context
                .GivenRequest()
                .GivenEntityModificationService();
            
            var result = await _subject.Handle(_context.Request , CancellationToken.None);

            _context
                .VerifyResultIsNotNull(result)
                .VerifyScenario();
        }
    }
}