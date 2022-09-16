create procedure dbo.GetAllTodoItems
as
begin
  select
    Id,
    [Name],
    IsComplete,
    [Secret]
  from
    dbo.TodoItems
  where IsDeleted = 0
end