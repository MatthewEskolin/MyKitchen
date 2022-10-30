using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKitchen.Migrations
{
    public partial class isQueuedtoAvailableItemsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
            ALTER view[dbo].[vwsUserMealsAndFoodItems] AS
                select mealname ItemName, mealID ItemID, 'MEAL' ItemType, AppUserId,IsQueued From meals
                union all
                select FoodItemName ItemName, fooditems.fooditemID ItemID, 'FOOD ITEM' ItemType, AppUserId,0 IsQueued from fooditems
                inner join userfooditems t2 on fooditems.fooditemid = t2.FoodItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER view[dbo].[vwsUserMealsAndFoodItems] AS
                select mealname ItemName, mealID ItemID, 'MEAL' ItemType, AppUserId From meals
                union all
                select FoodItemName ItemName, fooditems.fooditemID ItemID, 'FOOD ITEM' ItemType, AppUserId from fooditems
                inner join userfooditems t2 on fooditems.fooditemid = t2.FoodItemID");


        }
    }
}
