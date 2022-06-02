using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNet_Sample.Migrations
{
    public partial class RefactorClassNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_Carts_CartId",
                table: "Cart_Items",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_Carts_CartId",
                table: "Cart_Items");

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
    }
}
