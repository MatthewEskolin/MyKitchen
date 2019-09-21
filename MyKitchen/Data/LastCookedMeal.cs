using System;
using System.ComponentModel.DataAnnotations;

//Use this to track meals that I have actually cooked..accept or deny meals.

namespace MyKitchen.Data
{
    public class LastCookedMeal
    {
        [Key]
        public DateTime LastMeal { get; set; }
    }
}