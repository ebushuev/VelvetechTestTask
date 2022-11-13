using AutoFixture;

namespace TodoApi.Tests.Mappers.TodoItemMapperTests
{
    public abstract class TodoItemMappersBase
    {
        protected readonly IFixture Fixture;

        protected TodoItemMappersBase() 
        {
            Fixture = new Fixture();
        }
    }
}
