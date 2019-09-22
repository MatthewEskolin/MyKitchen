using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class FoodItemIndexViewModel
    {
        public IEnumerable<FoodItem> FoodItems { get; set; }
        public PagingInfo PagingInfo { get; set; }


    }

    public class MealBuilderSelectFoodItemsViewModel
    {
        public Meal TheMeal { get; set; }
        public IEnumerable<FoodItem> FoodItems { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }


    public class MealBuilderIndexViewModel
    {
        //MealList
        public PagingInfo MealListPagingInfo { get; set; }

        public IEnumerable<Meal> Meals { get; set; }

    }
}
