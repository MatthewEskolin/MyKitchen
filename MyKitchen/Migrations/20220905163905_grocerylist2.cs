using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKitchen.Migrations
{
    public partial class grocerylist2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GroceryListItems");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "GroceryListItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Shopped",
                table: "GroceryListItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "GroceryListItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroceryListItems_UserID",
                table: "GroceryListItems",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryListItems_AspNetUsers_UserID",
                table: "GroceryListItems",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroceryListItems_AspNetUsers_UserID",
                table: "GroceryListItems");

            migrationBuilder.DropIndex(
                name: "IX_GroceryListItems_UserID",
                table: "GroceryListItems");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "GroceryListItems");

            migrationBuilder.DropColumn(
                name: "Shopped",
                table: "GroceryListItems");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "GroceryListItems");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GroceryListItems",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
