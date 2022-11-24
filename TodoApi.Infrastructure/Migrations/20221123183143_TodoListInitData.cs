using Microsoft.EntityFrameworkCore.Migrations;
using TodoApi.Domain.Models;

namespace TodoApi.Infrastructure.Migrations
{
    public partial class TodoListInitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[]
                {
                    nameof(TodoItem.Id),
                    nameof(TodoItem.Name),
                    nameof(TodoItem.IsComplete),
                },
                values: new object[]
                {
                    1,
                    "Add swagger",
                    true
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[]
                {
                    nameof(TodoItem.Id),
                    nameof(TodoItem.Name),
                    nameof(TodoItem.IsComplete)
                },
                values: new object[]
                {
                    2,
                    "Store ToDo list in SQL Server DB",
                    true
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[]
                {
                    nameof(TodoItem.Id),
                    nameof(TodoItem.Name),
                    nameof(TodoItem.IsComplete)
                },
                values: new object[]
                {
                    3,
                    "Add file logging",
                    true,
                    string.Empty
                });

        migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[]
                {
                    nameof(TodoItem.Id),
                    nameof(TodoItem.Name),
                    nameof(TodoItem.IsComplete)
                },
                values: new object[]
                {
                    4,
                    "Finish refactoring, add business layer and infrastructure layer",
                    true,
                    string.Empty
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "TodoItems", keyColumn: nameof(TodoItem.Id), 1);
            migrationBuilder.DeleteData(table: "TodoItems", keyColumn: nameof(TodoItem.Id), 2);
            migrationBuilder.DeleteData(table: "TodoItems", keyColumn: nameof(TodoItem.Id), 3);
            migrationBuilder.DeleteData(table: "TodoItems", keyColumn: nameof(TodoItem.Id), 4);
        }
    }
}
