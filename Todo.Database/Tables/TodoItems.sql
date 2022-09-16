create table dbo.TodoItems
(
  Id bigint identity (1,1) not null constraint PK_TodoItems primary key (Id),
  [Name] nvarchar(255) null,
  IsComplete bit not null default(0),
  IsDeleted bit not null default(0),
  [Secret] nvarchar(255) null
)
