using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class IndexViewModel
    {
        public IEnumerable<FoodItem> FoodItemList { get; set; }
        public FoodItem NewFoodItem { get; set; }
    }
}

