using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKitchen.Data
{
    public class Meal
    {
        [Key]
        public int MealID { get; set; }
        public string Comments { get; set; }

        public string MealName { get; set; }
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

    public  class MealFactory
    {
        public MealFactory(ApplicationDbContext ctx) { }
        public Meal NewMeal()
        {
            var name = "Satisfying Meal 1";
            var rtn = new Meal() {MealName = name};
            return rtn;
        }
    }

}
