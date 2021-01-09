using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyKitchen.Data
{
    public class Meal
    {
        [Key]
        public int MealID { get; set; }
        public string Comments { get; set; }

        public ApplicationUser AppUser {get; set;}

        public string MealName { get; set; }

        public string Recipe {get; set;}
        public ICollection<MealFoodItems> MealFoodItems { get; set; }

        public Meal()
        {
          //  FoodItems = new List<FoodItem>();
        }
        public void AddFoodItemToMeal(int foodItemId)
        {
            var newItem = new MealFoodItems
            {
                FoodAddedDate = DateTime.Now, MealId = this.MealID, FoodItemId = foodItemId
            };

            MealFoodItems.Add(newItem);
        }

        public bool ContainsFoodItem(int foodItemId)
        {
            return this.MealFoodItems.Any(x => x.FoodItemId == foodItemId);
        }
    }

}
