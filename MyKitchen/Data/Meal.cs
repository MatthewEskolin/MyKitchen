using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyKitchen.Data
{
    public class Meal
    {
        public int MealID { get; set; }
        public string Comments { get; set; }

        public List<FoodItem> FoodItems { get; set; }




        
    }

    public class LastCookedMeal
    {
        [Key]
        public DateTime LastMeal { get; set; }
    }

    
}
