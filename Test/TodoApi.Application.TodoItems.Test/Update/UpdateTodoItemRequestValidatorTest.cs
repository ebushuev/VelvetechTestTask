using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using TodoApi.Application.TodoItems.Update;
using TodoApi.DataLayer.DataAccess;

namespace TodoApi.Application.TodoItems.Test.Update
{
    public class UpdateTodoItemRequestValidatorTest
    {
        private UpdateTodoItemRequestValidatorTestContext _context;

        private UpdateTodoItemRequestValidator _subject;

        [SetUp]
        public void Setup()
        {
            _context = new UpdateTodoItemRequestValidatorTestContext();

            _subject = _context.CreateSubject();
        }

        [Test]
        public async Task Validate_GivenValidRequest_GivenEntityInDb_SuccessfulResult()
        {
            _context
                .GivenValidRequest()
                .GivenEntityInDb();

            var result = await _subject.ValidateAsync(_context.Request, CancellationToken.None);

            _context
                .Verify(() => result.Errors.ShouldBeEmpty())
                .VerifyScenario();
        }

        [Test]
        public void Validate_GivenValidRequest_GivenNoEntityInDb_Failed()
        {
            _context
                .GivenValidRequest();

            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await _subject.ValidateAsync(_context.Request, CancellationToken.None));

            _context
                .VerifyScenario();
        }
        
        [Test]
        public async Task Validate_GivenInvalidRequest_GivenEntityInDb_Failed()
        {
            _context
                .GivenInvalidRequest()
                .GivenEntityInDb();

            var result = await _subject.ValidateAsync(_context.Request, CancellationToken.None);

            _context
                .Verify(() => result.Errors.ShouldNotBeEmpty())
                .VerifyScenario();
        }
    }
}