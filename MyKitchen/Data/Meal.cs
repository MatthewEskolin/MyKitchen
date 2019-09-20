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

        public string MealName { get; set; }
        public List<FoodItem> FoodItems { get; set; }
       

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
