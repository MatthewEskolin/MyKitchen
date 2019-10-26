using System.Collections.Generic;
using MyKitchen.Data;

namespace MyKitchen.Models.FoodItems
{
    public class FoodItemCreateViewModel
    {
        public FoodItem FoodItem { get; set; }

        public IEnumerable<FoodGroup> FoodGroups { get; set; }
    }
}