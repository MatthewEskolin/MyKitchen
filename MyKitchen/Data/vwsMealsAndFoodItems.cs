using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKitchen.Data
{
    public class vwsMealsAndFoodItems
    {
        public string ItemName { get; set; }
        public int ItemID { get; set; }

        public string ItemType { get; set; }

    }


    public partial class vwsMealItems
    {
        public string Mealname { get; set; }
        public int Mealid { get; set; }
        public string Comments { get; set; }
        public string MealAppUser { get; set; }
        public int FooditemId { get; set; }
        public string Fooditemname { get; set; }
        public string Email { get; set; }
        public string UserFoodItemsAppUser { get; set; }
    }
}
