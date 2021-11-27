using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class MealBuilderMealDetails_VM
    {
        public MealBuilderMealDetails_VM()
        {
            this.MealImages = new List<string>();
            
        }


        public Meal Meal { get; set; }

        public bool EditMealMode {get; set;}

        public List<string> MealImages {get; set;}

        

    }
}
