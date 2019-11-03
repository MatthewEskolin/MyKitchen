using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class EFFoodItemRepository:IFoodItemRepository
    {
        private readonly ApplicationDbContext _context;
        public EFFoodItemRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<FoodItem> FoodItems => _context.FoodItems;
        public Task<int> Add(FoodItem foodItem)
        {
            _context.FoodItems.Add(foodItem);
            return _context.SaveChangesAsync();
        }

        public Task<FoodItem> Find(int id)
        {
            return _context.FoodItems.FindAsync(id);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(FoodItem foodItem)
        {
            _context.FoodItems.Update(foodItem);
            _context.SaveChanges();
        }

        public void Remove(FoodItem foodItem)
        {
            _context.FoodItems.Remove(foodItem);
        }

        public FoodItem GetRandomItem()
        {
            return _context.FoodItems.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }

        public IEnumerable<FoodItem> GetFoodItems()
        {
            return FoodItems.AsEnumerable();
        }
    }
}
