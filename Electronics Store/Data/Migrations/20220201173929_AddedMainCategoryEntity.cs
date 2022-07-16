using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Electronics_Store.Data.Migrations
{
    public partial class AddedMainCategoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Main_CategoryId",
                table: "Categories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Main_CategoryId",
                table: "Categories",
                column: "Main_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MainCategories_Main_CategoryId",
                table: "Categories",
                column: "Main_CategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainCategories_Main_CategoryId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "MainCategories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Main_CategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Main_CategoryId",
                table: "Categories");
        }
    }
}
