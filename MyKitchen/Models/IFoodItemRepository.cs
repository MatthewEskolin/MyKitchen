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
        Task<int> Add(FoodItem foodItem);

        Task<FoodItem> Find(int id);
        Task SaveChangesAsync();
        void Update(FoodItem foodItem);
        void Remove(FoodItem foodItem);
    }
}
