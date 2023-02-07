using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using TodoApi.Application.TodoItems.Delete;
using TodoApi.DataLayer.DataAccess;

namespace TodoApi.Application.TodoItems.Test.Delete
{
    public class DeleteTodoItemRequestValidatorTest
    {
        private DeleteTodoItemRequestValidatorTestContext _context;

        private DeleteTodoItemRequestValidator _subject;

        [SetUp]
        public void Setup()
        {
            _context = new DeleteTodoItemRequestValidatorTestContext();

            _subject = _context.CreateSubject();
        }

        [Test]
        public async Task Validate_GivenValidRequest_GivenEntityInDb_SuccessfulResult()
        {
            _context
                .GivenValidRequest()
                .GivenEntityInDb();

            var validationResult = await _subject.ValidateAsync(_context.Request, CancellationToken.None);

            _context
                .Verify(() => validationResult.Errors.ShouldBeEmpty())
                .VerifyScenario();
        }

        [Test]
        public void Validate_GivenValidRequest_NoEntityInDb_Failed()
        {
            _context
                .GivenValidRequest();

            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await _subject.ValidateAsync(_context.Request, CancellationToken.None));

            _context
                .VerifyScenario();
        }
    }
}