using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class Events_User_FoodItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FoodItemID",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_AppUserId",
                table: "Events",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_FoodItemID",
                table: "Events",
                column: "FoodItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AppUserId",
                table: "Events",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FoodItems_FoodItemID",
                table: "Events",
                column: "FoodItemID",
                principalTable: "FoodItems",
                principalColumn: "FoodItemID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AppUserId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_FoodItems_FoodItemID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AppUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_FoodItemID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FoodItemID",
                table: "Events");
        }
    }
}
