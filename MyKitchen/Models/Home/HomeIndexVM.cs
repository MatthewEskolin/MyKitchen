using System.Collections.Generic;
using MyKitchen.Data;

namespace MyKitchen.Models.Home
{
    public class HomeIndexVM {

        public List<FoodItem> FoodItemList {get; set;}

        public FoodItem NewFoodItem {get; set;}

    }
}