using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Data.Migrations
{
    public partial class foodgroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodGroupID",
                table: "FoodItems",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodGroupID",
                table: "FoodItems",
                column: "FoodGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_FoodGroups_FoodGroupID",
                table: "FoodItems",
                column: "FoodGroupID",
                principalTable: "FoodGroups",
                principalColumn: "FoodGroupID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_FoodGroups_FoodGroupID",
                table: "FoodItems");

            migrationBuilder.DropTable(
                name: "FoodGroups");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_FoodGroupID",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "FoodGroupID",
                table: "FoodItems");
        }
    }
}
