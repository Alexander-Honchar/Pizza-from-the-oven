using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Пицца_WebAPI.Migrations
{
    public partial class MenuItemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_CategoryMenuItems_CategoryMenuItemId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_CategoryMenuItemId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "CategoryMenuItemId",
                table: "MenuItems");

            migrationBuilder.AddColumn<string>(
                name: "NameCategory",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameCategory",
                table: "MenuItems");

            migrationBuilder.AddColumn<long>(
                name: "CategoryMenuItemId",
                table: "MenuItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryMenuItemId",
                table: "MenuItems",
                column: "CategoryMenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_CategoryMenuItems_CategoryMenuItemId",
                table: "MenuItems",
                column: "CategoryMenuItemId",
                principalTable: "CategoryMenuItems",
                principalColumn: "Id");
        }
    }
}
