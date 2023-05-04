using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza_WebAPI.Migrations
{
    public partial class AddCook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "СookId",
                table: "OrderHeaders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_СookId",
                table: "OrderHeaders",
                column: "СookId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_СookId",
                table: "OrderHeaders",
                column: "СookId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_СookId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_СookId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "СookId",
                table: "OrderHeaders");
        }
    }
}
