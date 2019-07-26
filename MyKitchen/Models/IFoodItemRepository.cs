using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public interface IFoodItemRepository
    {
        IQueryable<FoodItem> FoodItems { get; }
    }
}
