using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class lastmeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LastCookedMeal",
                columns: table => new
                {
                    LastMeal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastCookedMeal", x => x.LastMeal);
                });

            //add test data for last meal
            migrationBuilder.InsertData(
                table: "LastCookedMeal",
                columns: new[] { "LastMeal" },
                values: new object[] { new DateTime(2019,03,24) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LastCookedMeal");
        }
    }
}
