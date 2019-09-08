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


        public Task<int> Add(Meal foodItem)
        {

            throw new NotImplementedException();
        }

        public Task<Meal> Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
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
    }
}