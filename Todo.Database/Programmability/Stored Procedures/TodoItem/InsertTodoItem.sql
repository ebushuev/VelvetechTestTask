create procedure dbo.InsertTodoItem
  @name nvarchar(255),
  @secret nvarchar(255),
  @isComplete bit
as
begin
  insert into dbo.TodoItems
  (
    [Name],
    IsComplete,
    [Secret]
  )
  values
  (
    @name,
    @isComplete,
    @secret
  );
   
  declare @insertedId bigint = SCOPE_IDENTITY();

  exec dbo.GetTodoItemById @insertedId
end