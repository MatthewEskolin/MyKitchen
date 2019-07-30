using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class EFFoodItemRepository:IFoodItemRepository
    {
        private ApplicationDbContext context;
        public EFFoodItemRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<FoodItem> FoodItems => context.FoodItems;
        public Task<int> Add(FoodItem foodItem)
        {
            context.FoodItems.Add(foodItem);
            return context.SaveChangesAsync();
        }

        public Task<FoodItem> Find(int id)
        {
            return context.FoodItems.FindAsync(id);
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(FoodItem foodItem)
        {
            context.FoodItems.Update(foodItem);
            context.SaveChanges();
        }

        public void Remove(FoodItem foodItem)
        {
            context.FoodItems.Remove(foodItem);
        }
    }
}
