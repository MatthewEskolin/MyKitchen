using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Models.BL;

namespace MyKitchen.Models
{
    public class EfMealRepository : IMealRepository
    {
        private ApplicationDbContext context;
        private UserInfo _user;

        public EfMealRepository(UserInfo user, ApplicationDbContext ctx)
        {
            context = ctx;
            _user = user;
        }

        public Task<int> Add(Meal meal)
        {
            context.Meals.Add(meal);

            if (meal.MealFoodItems != null)
            {
                //add meal items/
                foreach (var foodItem in meal.MealFoodItems)
                {
                    //add items to meal
                }
            }

            return context.SaveChangesAsync();
        }

        public Task<Meal> FindAsync(int id)
        {
            var meal = new Meal();

            IIncludableQueryable<Meal, FoodItem> wackyEntity = context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems);
            
            throw new NotImplementedException();
        }

        public Meal Find(int id)
        {

            IIncludableQueryable<Meal, FoodItem> wackyEntity = context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems);

            return wackyEntity.FirstOrDefault(x => x.MealID == id);

        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(Meal foodItem)
        {
            SaveChanges();
        }

        public void Remove(Meal meal)
        {


            //Remove From All Meals That have this item in them.
            var userMealFoodItems = context.MealFoodItems.Where(x => x.MealId == meal.MealID).ToList();

            //remove from calendar of events and history.
            var mealEvents = context.Events.Where(x => x.MealID == meal.MealID).ToList();

            context.Events.RemoveRange(mealEvents);
            context.MealFoodItems.RemoveRange(userMealFoodItems);
            context.Remove(meal);
        }

        public Meal GetRandomItem()
        {
            throw new NotImplementedException();
        }


        private IEnumerable<Meal> GetEnumerable()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return context.Meals.Count();
        }


        public Meal GetMealById(int mealId)
        {
            var meal = context.Meals.Where(x => x.MealID == mealId).Include(x => x.MealFoodItems).FirstOrDefault();
            return meal;

        }

        public IQueryable<Meal> GetMeals()
        {
            var _context = this.context;

            //figure out how to include a list of all food items in each meal as part of this query and look how it is performing the query.
            var cresult = (from meals in _context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems)
                           where meals.AppUser.Id == this._user.User.Id select meals).AsQueryable();

            return cresult;


        }


        public (IEnumerable<Meal> meals,PagingInfo pagingInfo) SearchMeals(int pageNum, int pageSize, string orderBy, MealSearchArgs searchArgs)
        {
            var _context = this.context;

            //load meals with details for each food in the meal.
            var cresult = (from meals in _context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems)
                           where meals.AppUser.Id == this._user.User.Id select meals);

  
            //Where Clause Conditions

            if (searchArgs != null)
            {

                if (!String.IsNullOrEmpty(searchArgs.MealName))
                {
                    cresult = cresult.Where(x => x.MealName.ToUpper().Contains(searchArgs.MealName.ToUpper()));
                }

                if (searchArgs.ShowQueuedOnly)
                {
                    cresult = cresult.Where(x => x.IsQueued);
                }

            }


            //Order By Conditions
            if(orderBy.EndsWith("_desc"))
            {
                var orderByProp = orderBy.Substring(0,orderBy.Length - "_desc".Length);
                cresult = cresult.OrderByDescending(e => EF.Property<object>(e, orderByProp));
            }
            else
            {
                cresult = cresult.OrderBy(e => EF.Property<object>(e, orderBy));
            }

            cresult = cresult.Skip((pageNum - 1) * pageSize).Take(pageSize);


            //need to set the total item count;
            var pagingInfo = new PagingInfo() { CurrentPage = pageNum,ItemsPerPage = pageSize,TotalItems = CountForUser(searchArgs)};

            return (cresult, pagingInfo);
        }


        public IEnumerable<Meal> GetFavoriteMeals()
        {
            
            //load meals with details for each food in the meal.

            var results = (from meals in
                    context.Meals.Include(x => x.MealFoodItems)
                                 .ThenInclude(x => x.FoodItems)
                where meals.AppUser.Id == this._user.User.Id
                where meals.IsFavorite
                select meals);

            return results;
        }


        public int CountForUser(MealSearchArgs searchArgs)
        {
            var _context = this.context;
            var cresult = (from meals in _context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems)
                           where meals.AppUser.Id == this._user.User.Id select meals).AsQueryable();

            if (searchArgs != null)
            {
                if(!String.IsNullOrEmpty(searchArgs.MealName))
                {
                    cresult = cresult.Where(x => x.MealName.Contains(searchArgs.MealName));
                }

                if (searchArgs.ShowQueuedOnly)
                {
                    cresult = cresult.Where(x => x.IsQueued);
                }
            }


                           

            return cresult.Count();
        }


    }
}