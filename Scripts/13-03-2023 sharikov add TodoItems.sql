CREATE TABLE TodoItems
(
	Id bigint NOT NULL IDENTITY(1,1),
	Name nvarchar(255),
	IsComplete bit NOT NULL,
	Secret nvarchar(255),
	PRIMARY KEY (Id)
);