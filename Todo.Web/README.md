# TodoService
This is the test task.

Requirements were to refactor the code and apply best practices to design the application.

- Core, DataAccess, DbMigrations, Tests projects were added
- The Main project was renamed Web
- Introduced MediatR and moved business logic from Controller to the commands and queries
- Introduced Repository pattern to be able to not have the Business layer be dependent on the DataAccess layer
- Introduced FluentValidation
- Introduced Swagger docs
- Introduced AutoMapper
