using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApiDTO.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    Secret = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsComplete", "ModifiedDate", "Name", "Secret" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 11, 11, 18, 23, 47, 326, DateTimeKind.Local).AddTicks(3345), null, true, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3012), "Name1", null },
                    { 2L, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3511), null, false, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3525), "Name2", null },
                    { 3L, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3536), null, false, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3537), "Name3", null },
                    { 4L, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3538), null, true, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3539), "Name4", null },
                    { 5L, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3542), null, true, new DateTime(2022, 11, 11, 18, 23, 47, 327, DateTimeKind.Local).AddTicks(3543), "Name5", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
