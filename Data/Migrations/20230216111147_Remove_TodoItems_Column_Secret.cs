using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Remove_TodoItems_Column_Secret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "TodoItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "TodoItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
