using System.Collections.Generic;
using MyKitchen.Data;

namespace MyKitchen.Models.Meals
{
    public class MealBuilderSelectFoodItemsViewModel
    {
        public Meal TheMeal { get; set; }
        public IEnumerable<FoodItem> FoodItems { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}