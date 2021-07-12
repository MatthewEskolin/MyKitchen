using System.Collections.Generic;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class MealBuilderIndexViewModel
    {
        //MealList
        public PagingInfo MealListPagingInfo { get; set; }
        public IEnumerable<Meal> Meals { get; set; }

    }
}