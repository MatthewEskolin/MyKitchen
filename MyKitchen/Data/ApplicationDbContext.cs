using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyKitchen.Controllers;
using MyKitchen.Data.Calendar;

namespace MyKitchen.Data
{
    //For Changing  Connection String to run producction efcore migrations
    //the connection string is stored in user secrets so we don't have to store it in source control
    public class ScaffoldProdContext : ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Read Connection String from environment variable


            string scaffoldConnStr = @"Data Source=mykitchen.database.windows.net;Database=MyKitchen;Integrated Security=false;User ID=matteskolin;Password=gIdzkGEfkcIcNXtElb4T!!;";
            optionsBuilder.UseSqlServer(scaffoldConnStr);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> , IMyKitchenDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public ApplicationDbContext(){}

        public DbSet<Meal> Meals { get; set; }

        public DbSet<LastCookedMeal> LastCookedMeal { get; set; }

        public DbSet<MealFoodItems> MealFoodItems { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<FileUpload> FileUploads {get; set;}


        public DbSet<UserFoodItem> UserFoodItems {get; set;}

        public DbSet<GroceryListItem> GroceryListItems {get; set;}

        public DbSet<Events> Events { get; set; }

        public DbSet<vwsMealsAndFoodItems> vwsMealsAndFoodItems { get; set; }

        public DbSet<vwsMealItems> vwsMealItems {get; set;}

        public virtual DbSet<vwsUserMealsAndFoodItem> vwsUserMealsAndFoodItems { get; set;}

        public DbSet<FoodGroup> FoodGroups { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MealFoodItems>(x => { x.HasIndex(y => new {y.FoodItemId, y.MealId}).IsUnique(); });

            builder.Entity<FoodItem>().HasOne(x => x.FoodGroup).WithMany(y => y.FoodItem);

            builder.Entity<vwsMealsAndFoodItems>().HasNoKey();

            builder.Entity<vwsMealItems>().HasNoKey();

            builder.Entity<vwsUserMealsAndFoodItem>().HasNoKey();

        }




    }

}
