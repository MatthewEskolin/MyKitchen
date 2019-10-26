using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKitchen.Data
{
    public class MealFoodItems
    {

        [Key]
        [Column(Order = 0)]
        public int MealFoodItemId { get; set; }

        [ForeignKey("FoodItems")]
        public int FoodItemId { get; set; }

        [ForeignKey("Meals")]
        public int MealId { get; set; }

        public DateTime FoodAddedDate { get; set; }

        public virtual Meal Meals { get; set; }

        public virtual FoodItem FoodItems { get; set; }

    }
}