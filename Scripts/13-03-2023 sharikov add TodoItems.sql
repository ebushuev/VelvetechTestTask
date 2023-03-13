CREATE TABLE TodoItems
(
	Id int NOT NULL,
	Name nvarchar(255) NOT NULL,
	IsComplete bit NOT NULL,
	Secret nvarchar(255),
	PRIMARY KEY (Id)
);