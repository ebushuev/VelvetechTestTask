create procedure dbo.UpdateTodoItem
  @id bigint,
  @name nvarchar(255),
  @isComplete bit,
  @secret nvarchar(255)
as
begin
  update dbo.TodoItems set
    [Name] = @name,
    IsComplete = @isComplete,
    [Secret] = @isComplete
  where Id = @id

  exec dbo.GetTodoItemById @id
end