using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyKitchen.Controllers;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class EfMealRepository : IMealRepository
    {
        private ApplicationDbContext context;

        public EfMealRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public Task<int> Add(Meal meal)
        {
            context.Meals.Add(meal);

            if (meal.MealFoodItems != null)
            {
                //add meal items
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

        public IEnumerable<Meal> GetMeals()
        {
            return context.Meals.AsEnumerable();
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

        public IQueryable<Meal> GetMealsForUser(ApplicationUser user)
        {
            var _context = this.context;

            //figure out how to include a list of all food items in each meal as part of this query and look how it is performing the query.
            var cresult = (from meals in _context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems)
                           where meals.AppUser.Id == user.Id select meals).AsQueryable();

            return cresult;


        }


        public (IEnumerable<Meal> meals,PagingInfo pagingInfo) GetMealsForUser(int pageNum, int pageSize, ApplicationUser user,string mealName,string orderBy)
        {
            var _context = this.context;

            //load meals with details for each food in the meal.
            var cresult = (from meals in _context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems)
                           where meals.AppUser.Id == user.Id select meals);

  
            //Where Clause Conditions

            if(!String.IsNullOrEmpty(mealName)){
                cresult = cresult.Where(x => x.MealName.ToUpper().Contains(mealName.ToUpper()));
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
            var pagingInfo = new PagingInfo() { CurrentPage = pageNum,ItemsPerPage = pageSize,TotalItems = CountForUser(user,mealName)};

            return (cresult, pagingInfo);
        }





        public int CountForUser(ApplicationUser user,string mealName)
        {
            var _context = this.context;
            var cresult = (from meals in _context.Meals.Include(x => x.MealFoodItems).ThenInclude(x => x.FoodItems)
                           where meals.AppUser.Id == user.Id select meals).AsQueryable();
                           
                           
            if(!String.IsNullOrEmpty(mealName))
            {
                cresult = cresult.Where(x => x.MealName.Contains(mealName));
         
            }

            return cresult.Count();
        }


    }
}