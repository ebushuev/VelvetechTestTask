using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TodoApi.Application.TodoItems.Delete;

namespace TodoApi.Application.TodoItems.Test.Delete
{
    public class DeleteTodoItemRequestHandlerTest
    {
        private DeleteTodoItemRequestHandler _subject;

        private DeleteTodoItemRequestHandlerTestContext _context;
        
        [SetUp]
        public void Setup()
        {
            _context = new DeleteTodoItemRequestHandlerTestContext();

            _subject = _context.CreateSubject();
        }

        [Test]
        public async Task Handle_GivenRequest_GivenEntityInDb_SuccessfulResult()
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