using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    IsComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "IsComplete", "Name" },
                values: new object[,]
                {
                    { new Guid("7bf8bc46-a6ad-4ca8-b398-781639dc0214"), false, "Купить хлебушек!" },
                    { new Guid("863bed94-d1c9-4c51-bbfa-4e78a794eed8"), true, "Проснутся, улыбнутся" },
                    { new Guid("e9331a6f-aed4-4dfb-9095-c04704a1a1b2"), true, "Делать проект!" },
                    { new Guid("82d51728-d3f4-4626-9d55-69c6a1d7a1d3"), false, "Позвонить на работу" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
