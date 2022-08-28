using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyKitchen.Data;
using MyKitchen.Data.Calendar;

namespace MyKitchen.Controllers
{
    public interface IMyKitchenDataContext
    {
        DbSet<Meal> Meals { get; set; }

        DbSet<LastCookedMeal> LastCookedMeal { get; set; }

        DbSet<MealFoodItems> MealFoodItems { get; set; }
        DbSet<FoodItem> FoodItems { get; set; }

        DbSet<Events> Events { get; set; }

        DbSet<vwsMealsAndFoodItems> vwsMealsAndFoodItems { get; set; }
        DbSet<FoodGroup> FoodGroups { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);



    }
}