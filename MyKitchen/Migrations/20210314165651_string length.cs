using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Migrations
{
    public partial class stringlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropTable(
                name: "LastCookedMeal");

            migrationBuilder.DropTable(
                name: "MealFoodItems");

            migrationBuilder.DropTable(
                name: "UserFoodItems");

            migrationBuilder.DropTable(
                name: "vwsMealItems");

            migrationBuilder.DropTable(
                name: "vwsMealsAndFoodItems");

            migrationBuilder.DropTable(
                name: "vwsUserMealsAndFoodItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FoodGroups");
        }
    }
}
