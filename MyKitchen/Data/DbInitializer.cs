using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyKitchen.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (context.FoodItems.Any())
            {
                return; // DB has been seeded
            }


            var seedFoodItems = new FoodItem[]
            {
                new FoodItem() {Cost = 0.00M, FoddDescription = "Romaine Lettuce", FoodItemName = "Romaine Lettuce"},
                new FoodItem() {Cost = 0.00M, FoddDescription = "Baked Beans", FoodItemName = "Canned Baked Beans"},
                new FoodItem() {Cost = 0.00M, FoddDescription = "Cage Free Egg", FoodItemName = "Egg - Scrambled"},
                new FoodItem()
                    {Cost = 0.00M, FoddDescription = "Cage Free Egg", FoodItemName = "Little Sizzlers Sausage"}


            };

            foreach (var itme in seedFoodItems)
            {
                context.FoodItems.Add(itme);
            }

            context.SaveChanges();

        }
    }
}
