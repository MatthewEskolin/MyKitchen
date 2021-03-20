using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class MealBuilderMealDetails_VM
    {
        public Meal Meal { get; set; }
        public bool EditMealMode {get; set;}

        public List<string> MealImages {get; set;}

        

    }
}
