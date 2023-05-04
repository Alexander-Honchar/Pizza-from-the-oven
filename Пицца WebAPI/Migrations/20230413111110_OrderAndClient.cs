using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Пицца_WebAPI.Migrations
{
    public partial class OrderAndClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailes_MenuItems_MenuItemId",
                table: "OrderDetailes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_UserId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_UserId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailes_MenuItemId",
                table: "OrderDetailes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GeoLocation",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OrderHeaders",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "OrderHeaders",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "OrderHeaders",
                newName: "House");

            migrationBuilder.RenameColumn(
                name: "CommentsManager",
                table: "OrderHeaders",
                newName: "Floor");

            migrationBuilder.RenameColumn(
                name: "PriceTotal",
                table: "OrderDetailes",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "OrderDetailes",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderDetailes",
                newName: "MenuId");

            migrationBuilder.AlterColumn<string>(
                name: "DateCreatedOrder",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Apartment",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "OrderHeaders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Entrance",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "OrderHeaders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberOrder",
                table: "OrderHeaders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "TotalSumma",
                table: "OrderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "СookId",
                table: "OrderHeaders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Count",
                table: "OrderDetailes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MenuName",
                table: "OrderDetailes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameCategory",
                table: "OrderDetailes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_СookId",
                table: "OrderHeaders",
                column: "СookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ClientId",
                table: "OrderHeaders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ManagerId",
                table: "OrderHeaders",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailes_MenuId",
                table: "OrderDetailes",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailes_MenuItems_MenuId",
                table: "OrderDetailes",
                column: "MenuId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_СookId",
                table: "OrderHeaders",
                column: "СookId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_ManagerId",
                table: "OrderHeaders",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Clients_ClientId",
                table: "OrderHeaders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailes_MenuItems_MenuId",
                table: "OrderDetailes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_СookId",
                table: "OrderHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_ManagerId",
                table: "OrderHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Clients_ClientId",
                table: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_СookId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_ClientId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_ManagerId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailes_MenuId",
                table: "OrderDetailes");

            migrationBuilder.DropColumn(
                name: "Apartment",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Entrance",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "NumberOrder",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "TotalSumma",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "СookId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderDetailes");

            migrationBuilder.DropColumn(
                name: "MenuName",
                table: "OrderDetailes");

            migrationBuilder.DropColumn(
                name: "NameCategory",
                table: "OrderDetailes");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "OrderHeaders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "OrderHeaders",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "House",
                table: "OrderHeaders",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "Floor",
                table: "OrderHeaders",
                newName: "CommentsManager");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetailes",
                newName: "MenuItemId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderDetailes",
                newName: "PriceTotal");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "OrderDetailes",
                newName: "Amount");

            migrationBuilder.AlterColumn<string>(
                name: "DateCreatedOrder",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderHeaders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoLocation",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_UserId",
                table: "OrderHeaders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailes_MenuItemId",
                table: "OrderDetailes",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailes_MenuItems_MenuItemId",
                table: "OrderDetailes",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_UserId",
                table: "OrderHeaders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
