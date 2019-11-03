using System.Collections.Generic;
using MyKitchen.Data;

namespace MyKitchen.Models.FoodItems
{
    public class FoodItemEditViewModel
    {
        public FoodItem FoodItem { get; set; }

        public IEnumerable<FoodGroup> FoodGroups { get; set; }
    }


    public class FoodItemDetailsViewModel
    {
        public FoodItem FoodItem { get; set; }

    }

}