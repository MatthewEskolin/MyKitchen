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
}
