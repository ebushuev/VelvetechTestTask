using Moq;
using TodoApiDTO.Api.Validation;
using TodoApiDTO.Core.Models;
using TodoApiDTO.Core.Services;

namespace TodoApiDTO.Api.Test;

public class TodoItemDtoValidatorTest
{
    [Fact]
    public async Task NameIsNull_MustBeFail()
    {
        var mocks = new MockRepository(MockBehavior.Strict);
        var todoService = mocks.Create<ITodoService>();

        var dto = new TodoItemDTO
        {
            Name = null
        };

        var validator = new TodoItemDTOValidator(todoService.Object);
        var validationResult = await validator.CheckAsync(dto);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name is required.", validationResult.ErrorMessage);
    }

    [Fact]
    public async Task NameIsUsed_MustBeFail()
    {
        const long id = 1;
        const string name = "used";

        var mocks = new MockRepository(MockBehavior.Strict);
        var todoService = mocks.Create<ITodoService>();

        todoService
            .Setup(s => s.GetNameIsUsedExceptOneAsync(id, name))
            .Returns(Task.FromResult(true));

        var dto = new TodoItemDTO
        {
            Id = id,
            Name = name
        };

        var validator = new TodoItemDTOValidator(todoService.Object);
        var validationResult = await validator.CheckAsync(dto);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name 'used' is not unique.", validationResult.ErrorMessage);
    }

    [Fact]
    public async Task MustBeSuccess()
    {
        const long id = 1;
        const string name = "not_used";

        var mocks = new MockRepository(MockBehavior.Strict);
        var todoService = mocks.Create<ITodoService>();

        todoService
            .Setup(s => s.GetNameIsUsedExceptOneAsync(id, name))
            .Returns(Task.FromResult(false));

        var dto = new TodoItemDTO
        {
            Id = id,
            Name = name
        };

        var validator = new TodoItemDTOValidator(todoService.Object);
        var validationResult = await validator.CheckAsync(dto);

        Assert.True(validationResult.IsValid);
        Assert.Null(validationResult.ErrorMessage);
    }
}