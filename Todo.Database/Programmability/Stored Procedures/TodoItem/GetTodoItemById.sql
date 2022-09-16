create procedure dbo.GetTodoItemById
  @id bigint
as
begin
  select
    Id,
    [Name],
    IsComplete,
    [Secret]
  from
    dbo.TodoItems
  where Id = @id and IsDeleted = 0
end