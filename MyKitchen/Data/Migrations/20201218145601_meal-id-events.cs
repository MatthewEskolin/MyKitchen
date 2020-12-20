using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class mealidevents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_FoodItems_FoodItemID",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "FoodItemID",
                table: "Events",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MealID",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_MealID",
                table: "Events",
                column: "MealID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FoodItems_FoodItemID",
                table: "Events",
                column: "FoodItemID",
                principalTable: "FoodItems",
                principalColumn: "FoodItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Meals_MealID",
                table: "Events",
                column: "MealID",
                principalTable: "Meals",
                principalColumn: "MealID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_FoodItems_FoodItemID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Meals_MealID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_MealID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MealID",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "FoodItemID",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FoodItems_FoodItemID",
                table: "Events",
                column: "FoodItemID",
                principalTable: "FoodItems",
                principalColumn: "FoodItemID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
