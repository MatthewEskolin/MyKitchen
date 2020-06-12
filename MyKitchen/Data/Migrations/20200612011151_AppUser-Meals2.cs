using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class AppUserMeals2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Meals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_AppUserId",
                table: "Meals",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_AspNetUsers_AppUserId",
                table: "Meals",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_AspNetUsers_AppUserId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_AppUserId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Meals");
        }
    }
}
