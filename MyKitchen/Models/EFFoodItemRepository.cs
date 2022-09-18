using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
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

        //Returns all Food Items in the DB - should only be used when we don't care about this user who created the item
        public IQueryable<FoodItem> FoodItems => _context.FoodItems.Include(x => x.FoodGroup);
        
       //Add's a new FoodItem to the globa database - these food items are not yet associated with a user. 
        public Task<int> Add(FoodItem foodItem)
        {
            _context.FoodItems.Add(foodItem);
            _logger.LogInformation($"added food item {foodItem.FoodItemName}");
            return _context.SaveChangesAsync();
        }

        //Adds an association between a food item and a user.
        public async Task<int> AddFoodForUser(IUserInfo user, FoodItem foodItem)
        {

            var userFoodItem = new MyKitchen.Data.UserFoodItem();
            userFoodItem.FoodItemID = foodItem.FoodItemID;

            //TODO get rid of this can we use interface
            userFoodItem.AppUser = user as ApplicationUser;

            _context.UserFoodItems.Add(userFoodItem);
            return await _context.SaveChangesAsync();
        }



        public Task<FoodItem> Find(int id)
        {
            #region Example Code - Logging 
            // _logger.LogTrace("TRACE TEST - search for item");
            // _logger.LogDebug("DEUB TEST - search for item");
            // _logger.LogInformation("Information TEST - search for item");
            // _logger.LogWarning("WARNING TEST - search for item");
            // _logger.LogError("ERROR TEST - search for item");
            // _logger.LogCritical("CRITICAL TEST - search for item");
            #endregion
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
            //Remove Related Records in Appropriate Order
            //Not sure if this is how repositories are suppoesed to work, but need to get this working today.
            
            //Remove User-Food Records     
            var userFoodItems = _context.UserFoodItems.Where(x => x.FoodItemID == foodItem.FoodItemID).ToList();

            //Remove From All Meals That have this item in them.
            var userMealFoodItems = _context.MealFoodItems.Where(x => x.FoodItemId == foodItem.FoodItemID).ToList();

            //remove from calendar of events and history.
            var foodEvents = _context.Events.Where(x => x.FoodItemID == foodItem.FoodItemID).ToList();

            _context.UserFoodItems.RemoveRange(userFoodItems);
            _context.MealFoodItems.RemoveRange(userMealFoodItems);
            _context.Events.RemoveRange(foodEvents);
            _context.FoodItems.Remove(foodItem);

            SaveChangesAsync();
            
        }

        public FoodItem GetRandomItem()
        {
            return _context.FoodItems.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }

        public IEnumerable<FoodItem> GetFoodItems()
        {
            return FoodItems.AsEnumerable();
        }
        public IQueryable<FoodItem> GetFoodItemsForUser(IUserInfo user)
        {
            
            // return _context.FoodItems.Include(x => x.FoodGroup).Where(_context.UserFoodItems.Where);

            var cresult = (from userFoodItmes in _context.UserFoodItems
                              join foodItems in _context.FoodItems.Include(x => x.FoodGroup)
                              on userFoodItmes.FoodItemID equals foodItems.FoodItemID
                              where userFoodItmes.AppUser.Id == user.Id
                              select foodItems).AsQueryable();

            return cresult;





            // var userFoodItems = _context.UserFoodItems.Where(x => x.AppUser.Id == user.Id).Select(x => x.FoodItems
            // .Include(x => x.FoodGroups)).ToLIst();
            // return userFoodItems;
        }
        IQueryable<FoodItem> IFoodItemRepository.GetFoodItems()
        {
            return FoodItems.AsQueryable();
        }

        public async Task RemoveByIdAsync(int id)
        {
            var foodItem  = await Find(id);
            Remove(foodItem);
        }

    }
}
