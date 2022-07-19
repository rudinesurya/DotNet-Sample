using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNet_Sample.Migrations
{
    public partial class refactordb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_Carts_CartId",
                table: "Cart_Items");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Cart_Items",
                newName: "ECartId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_Items_CartId",
                table: "Cart_Items",
                newName: "IX_Cart_Items_ECartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_Carts_ECartId",
                table: "Cart_Items",
                column: "ECartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_Carts_ECartId",
                table: "Cart_Items");

            migrationBuilder.RenameColumn(
                name: "ECartId",
                table: "Cart_Items",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_Items_ECartId",
                table: "Cart_Items",
                newName: "IX_Cart_Items_CartId");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_Carts_CartId",
                table: "Cart_Items",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
