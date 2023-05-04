using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Пицца_WebAPI.Migrations
{
    public partial class DateClosedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateClosedOrder",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateClosedOrder",
                table: "OrderHeaders");
        }
    }
}
