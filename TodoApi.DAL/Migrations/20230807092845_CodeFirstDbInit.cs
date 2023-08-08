using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.DAL.Migrations
{
    public partial class CodeFirstDbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: false),
                    Secret = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "IsComplete", "Name", "Secret" },
                values: new object[,]
                {
                    { 1, false, "Finish test task", null },
                    { 2, true, "Take a break", null },
                    { 3, false, "Walk little bit", null },
                    { 4, false, "Learn React", null },
                    { 5, true, "Practice", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
