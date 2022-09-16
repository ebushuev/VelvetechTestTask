create procedure dbo.DeleteTodoItem
  @id bigint
as
begin
  update dbo.TodoItems set
    IsDeleted = 1
  where Id = @id
end