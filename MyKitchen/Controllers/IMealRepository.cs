using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyKitchen.Data;
using MyKitchen.Models;

namespace MyKitchen.Controllers
{
    public interface IMealRepository
    {
        Task<int> Add(Meal foodItem);

        Task<Meal> FindAsync(int id);
        Meal Find(int mealId);

        Task SaveChangesAsync();

        int SaveChanges();
        void Update(Meal meal);
        void Remove(Meal foodItem);
        Meal GetRandomItem();
        IEnumerable<Meal> GetMeals();
        int Count();
        // IQueryable<Meal> GetMealsForUser(ApplicationUser user);

        (IEnumerable<Meal> meals, PagingInfo pagingInfo) GetMealsForUser(int pageNum, int pageSize, ApplicationUser user,string mealNameSearch,string orderBy);

        IEnumerable<Meal> GetFavoriteMealsForUser(ApplicationUser user);


        int CountForUser(ApplicationUser user,string mealName);
    }
}