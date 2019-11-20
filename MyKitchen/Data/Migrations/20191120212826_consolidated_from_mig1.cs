using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class consolidated_from_mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Meals_MealID",
                table: "FoodItems");

            migrationBuilder.RenameColumn(
                name: "MealID",
                table: "FoodItems",
                newName: "FoodGroupID");

            migrationBuilder.RenameIndex(
                name: "IX_FoodItems_MealID",
                table: "FoodItems",
                newName: "IX_FoodItems_FoodGroupID");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    ThemeColor = table.Column<string>(nullable: true),
                    IsFullDay = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "FoodGroups",
                columns: table => new
                {
                    FoodGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodGroups", x => x.FoodGroupID);
                });

            migrationBuilder.CreateTable(
                name: "MealFoodItems",
                columns: table => new
                {
                    MealFoodItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodItemId = table.Column<int>(nullable: false),
                    MealId = table.Column<int>(nullable: false),
                    FoodAddedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFoodItems", x => x.MealFoodItemId);
                    table.ForeignKey(
                        name: "FK_MealFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealFoodItems_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "MealID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealFoodItems_MealId",
                table: "MealFoodItems",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoodItems_FoodItemId_MealId",
                table: "MealFoodItems",
                columns: new[] { "FoodItemId", "MealId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_FoodGroups_FoodGroupID",
                table: "FoodItems",
                column: "FoodGroupID",
                principalTable: "FoodGroups",
                principalColumn: "FoodGroupID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql( "create view [dbo].[vwsMealsAndFoodItems] AS select mealname ItemName,mealID ItemID,'MEAL' ItemType From meals union all select FoodItemName ItemName, fooditemID ItemID, 'FOOD ITEM' ItemType from fooditems ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_FoodGroups_FoodGroupID",
                table: "FoodItems");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "FoodGroups");

            migrationBuilder.DropTable(
                name: "MealFoodItems");

            migrationBuilder.RenameColumn(
                name: "FoodGroupID",
                table: "FoodItems",
                newName: "MealID");

            migrationBuilder.RenameIndex(
                name: "IX_FoodItems_FoodGroupID",
                table: "FoodItems",
                newName: "IX_FoodItems_MealID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Meals_MealID",
                table: "FoodItems",
                column: "MealID",
                principalTable: "Meals",
                principalColumn: "MealID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("DROP VIEW IF EXISTS vwsMealsAndFoodItems");


    }
}
}
