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

        [Display(Name="Meal Name")]
        [MaxLength(100)]
        public string MealName { get; set; }

        public bool IsFavorite {get; set;}

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

        public void RemoveFoodItemFromMeal(int mealFoodItemId)
        {
            var removeRec = MealFoodItems.FirstOrDefault(x => x.MealFoodItemId == mealFoodItemId);
            MealFoodItems.Remove(removeRec);
            
        }

        public bool ContainsFoodItem(int foodItemId)
        {
            return this.MealFoodItems.Any(x => x.FoodItemId == foodItemId);
        }
    }

}
