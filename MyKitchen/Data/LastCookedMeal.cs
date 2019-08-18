using System;
using System.ComponentModel.DataAnnotations;

namespace MyKitchen.Data
{
    public class LastCookedMeal
    {
        [Key]
        public DateTime LastMeal { get; set; }
    }
}