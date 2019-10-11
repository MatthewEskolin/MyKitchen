using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class builder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealFoodItems_FoodItemId",
                table: "MealFoodItems");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoodItems_FoodItemId_MealId",
                table: "MealFoodItems",
                columns: new[] { "FoodItemId", "MealId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealFoodItems_FoodItemId_MealId",
                table: "MealFoodItems");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoodItems_FoodItemId",
                table: "MealFoodItems",
                column: "FoodItemId");
        }
    }
}
