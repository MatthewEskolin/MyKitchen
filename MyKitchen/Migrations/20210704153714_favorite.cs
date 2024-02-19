using Microsoft.EntityFrameworkCore.Migrations;

namespace MyKitchen.Migrations
{
    public partial class favorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AddColumn<bool>(
            //     name: "IsFavorite",
            //     table: "Meals",
            //     type: "bit",
            //     nullable: false,
            //     defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Meals");
        }
    }
}
