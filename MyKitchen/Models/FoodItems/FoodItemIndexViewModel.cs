using System.Collections.Generic;
using MyKitchen.Data;

namespace MyKitchen.Models.FoodItems
{
    public class FoodItemIndexViewModel
    {
        public IEnumerable<FoodItem> FoodItems { get; set; }
        public PagingInfo PagingInfo { get; set; }



    }
}
