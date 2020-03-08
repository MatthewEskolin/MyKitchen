using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class EFFoodItemRepository:IFoodItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public EFFoodItemRepository(ApplicationDbContext ctx,ILogger<EFFoodItemRepository> logger)
        {
            _context = ctx;
            _logger = logger;
        }

        public IQueryable<FoodItem> FoodItems => _context.FoodItems.Include(x => x.FoodGroup);
        public Task<int> Add(FoodItem foodItem)
        {
            _context.FoodItems.Add(foodItem);
            _logger.LogError("ERROR TEST - added item");
            return _context.SaveChangesAsync();
        }

        public Task<FoodItem> Find(int id)
        {
            _logger.LogTrace("TRACE TEST - search for item");
            _logger.LogDebug("DEUB TEST - search for item");
            _logger.LogInformation("Information TEST - search for item");
            _logger.LogWarning("WARNING TEST - search for item");
            _logger.LogError("ERROR TEST - search for item");
            _logger.LogCritical("CRITICAL TEST - search for item");
            return _context.FoodItems.FindAsync(id).AsTask();
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
