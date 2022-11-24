# Migrations. How to start:

## Instal the dotnet tools using this command:
dotnet tool install --global dotnet-ef

## Create and update database using this command: 

dotnet ef database update -p "TodoApi.Infrastructure" -- --connection "Server=localhost;Database=TodoApi;Trusted_Connection=True;"
