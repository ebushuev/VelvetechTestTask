# TodoService

A simple service for managing a list of tasks - TODO list.

### Used packages
| Package Name                               |  Ver  |
|--------------------------------------------|-------|
| Microsoft.EntityFrameworkCore.Design       | 3.1.0 |
| Microsoft.EntityFrameworkCore.SqlServer    | 3.1.0 |
| NLog                                       | 5.1.0 |
| NLog.Web.AspNetCore                        | 5.2.0 |
| Swashbuckle.AspNetCore.Swagger             | 6.4.0 |
| Swashbuckle.AspNetCore.SwaggerGen          | 6.4.0 |
| Swashbuckle.AspNetCore.SwaggerUI           | 6.4.0 |

### Migration
If you want to use your own migrations or you want
to test migration mechanism follow next steps:

* Set the connection string of the following form:

```
"ConnectionStrings": {
    Server=(localdb)\\mssqllocaldb;Database=<your_database_name>;Trusted_Connection=True;Integrated Security=True;User Instance=False;
}
```

> note that you have to do next steps from your working directory!

* install the ```ef``` utility:

```PS> dotnet tool install --global dotnet-ef```

* If you have migrations files delete them:

```PS> dotnet ef migrations remove```

* Create new migrations:

```PS> dotnet ef migrations add <migration_name>```

* Update SqlLocalDb by your migrations:

```PS> dotnet ef database update```

### Logs
You can find errors logs in project ```logs\``` directory.
All logs have the format: ```LOG_PREFIX_${shortDate}.log```