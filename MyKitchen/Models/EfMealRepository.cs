using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            if (meal.FoodItems != null)
            {
                //add meal items
                foreach (var foodItem in meal.FoodItems)
                {
                    //add items to meal
                }
            }

            return context.SaveChangesAsync();
        }

        public Task<Meal> Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Meal foodItem)
        {
            throw new NotImplementedException();
        }

        public void Remove(Meal foodItem)
        {
            throw new NotImplementedException();
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
            var meal = context.Meals.FirstOrDefault(x => x.MealID == mealId);
            return meal;

        }
    }
}