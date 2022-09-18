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
        Meal Find(int mealId);

        Task SaveChangesAsync();
        int SaveChanges();
        void Update(Meal meal);
        void Remove(Meal foodItem);
        int Count();

        (IEnumerable<Meal> meals, PagingInfo pagingInfo) GetMeals(int pageNum, int pageSize, string mealNameSearch,string orderBy);
        IEnumerable<Meal> GetFavoriteMeals();
    }
}