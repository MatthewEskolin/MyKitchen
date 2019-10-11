using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class changecollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Meals_MealID",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_MealID",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "MealID",
                table: "FoodItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MealID",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_MealID",
                table: "FoodItems",
                column: "MealID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Meals_MealID",
                table: "FoodItems",
                column: "MealID",
                principalTable: "Meals",
                principalColumn: "MealID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
