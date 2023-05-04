using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Пицца_WebAPI.Migrations
{
    public partial class PizzaKingSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PizzaKingSizeId",
                table: "CategoryMenuItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PizzaKingSizeIdId",
                table: "CategoryMenuItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PizzaKingSizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaKingSizes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMenuItems_PizzaKingSizeIdId",
                table: "CategoryMenuItems",
                column: "PizzaKingSizeIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryMenuItems_PizzaKingSizes_PizzaKingSizeIdId",
                table: "CategoryMenuItems",
                column: "PizzaKingSizeIdId",
                principalTable: "PizzaKingSizes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryMenuItems_PizzaKingSizes_PizzaKingSizeIdId",
                table: "CategoryMenuItems");

            migrationBuilder.DropTable(
                name: "PizzaKingSizes");

            migrationBuilder.DropIndex(
                name: "IX_CategoryMenuItems_PizzaKingSizeIdId",
                table: "CategoryMenuItems");

            migrationBuilder.DropColumn(
                name: "PizzaKingSizeId",
                table: "CategoryMenuItems");

            migrationBuilder.DropColumn(
                name: "PizzaKingSizeIdId",
                table: "CategoryMenuItems");
        }
    }
}
