using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyKitchen.Data;

namespace MyKitchen.Controllers
{
    public interface IMealRepository
    {
        Task<int> Add(Meal foodItem);

        Task<Meal> FindAsync(int id);
        Meal Find(int mealId);

        Task SaveChangesAsync();

        int SaveChanges();
        void Update(Meal foodItem);
        void Remove(Meal foodItem);
        Meal GetRandomItem();
        IEnumerable<Meal> GetMeals();
        int Count();
        IQueryable<Meal> GetMealsForUser(ApplicationUser user);
        int CountForUser(ApplicationUser user);
    }
}