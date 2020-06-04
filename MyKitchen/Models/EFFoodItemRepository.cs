﻿using System;
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
            _logger.LogInformation($"added food item {foodItem.FoodItemName}");
            return _context.SaveChangesAsync();
        }

        public async Task<int> AddFoodForUser(ApplicationUser user, FoodItem foodItem)
        {

            var userFoodItem = new MyKitchen.Data.UserFoodItem();
            userFoodItem.FoodItemID = foodItem.FoodItemID;
            userFoodItem.AppUser = user;

            _context.UserFoodItems.Add(userFoodItem);
            return await _context.SaveChangesAsync();
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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(FoodItem foodItem)
        {
            _context.FoodItems.Update(foodItem);
            _context.SaveChanges();
        }

        public void Remove(FoodItem foodItem)
        {
            var userFoodItems = _context.UserFoodItems.Where(x => x.FoodItemID == foodItem.FoodItemID).ToList();

            //remove all userfood Items associated with this FoodItem
            _context.UserFoodItems.RemoveRange(userFoodItems);
            //remove the fooditem
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

        public IQueryable<FoodItem> GetFoodItemsForUser(ApplicationUser user)
        {
            var userFoodItems = _context.UserFoodItems.Where(x => x.AppUser.Id == user.Id).Select(x => x.FoodItems);
            return userFoodItems;
        }
    }
}
