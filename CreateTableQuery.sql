DROP TABLE TodoItems;

CREATE TABLE TodoItems ( 
	id BIGINT NOT NULL PRIMARY KEY IDENTITY,
	name VARCHAR(200) NOT NULL,
	isComplete BIT NOT NULL,
	secret VARCHAR(200)
	);

INSERT INTO TodoItems (name, isComplete, secret)
VALUES
('Add Swagger', 1, 'Placeholder'),
('Integrate SQL in the project', 1, 'Placeholder'),
('Add Logging in file', 0, 'Placeholder'),
('Separate Business Layers', 0, 'Placeholder'),
('Refactoring', 0 , 'Placeholder')

