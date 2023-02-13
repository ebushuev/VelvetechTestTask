using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoItem",
                columns: new[] { "Id", "IsComplete", "Name", "Secret" },
                values: new object[,]
                {
                    { 1, false, "TEST", "SECRET" },
                    { 2, false, "TEST-2", "SECRET" },
                    { 3, true, "TEST-3", "SECRET" },
                    { 4, false, "TEST-4", "SECRET" },
                    { 5, true, "TEST-5", "SECRET" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
