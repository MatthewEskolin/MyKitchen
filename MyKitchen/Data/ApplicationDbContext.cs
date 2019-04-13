using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyKitchen.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {


        


        }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<LastCookedMeal> LastCookedMeal { get; set; }


        public DbSet<FoodItem> FoodItems { get; set; }


    }
}
