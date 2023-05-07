namespace Todo.Dal.Context
{
    internal interface ITodoContextFactory
    {
        TodoContext CreateContext();
    }
}