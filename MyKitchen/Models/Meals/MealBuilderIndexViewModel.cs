using System.Collections.Generic;
using MyKitchen.Controllers;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class MealBuilderIndexViewModel
    {
        public MealBuilderIndexViewModel()
        {
            //add default sort state all columns asscending
            SortState.Add("MealName","MealName");
            SortState.Add("SatietyProfile","SatietyProfile");
            SortState.Add("IsFavorite","IsFavorite");
        }


        //MealList
        public PagingInfo MealListPagingInfo { get; set; }
        public IEnumerable<Meal> Meals { get; set; }

        public Dictionary<string,string> SortState {get; set;} = new Dictionary<string,string>();

        public string CurrentSort {get; set;} = "MealName";

        public string NewSort {get; set;} = string.Empty;

        public int CurrentPage {get; set;} = 1;

        public bool ToggleSort {get; set;} = false;

        public MealSearchArgs DashboardSearchArgs { get; set; }
        //public bool ShowQueuedOnly { get; set; }


    }
}