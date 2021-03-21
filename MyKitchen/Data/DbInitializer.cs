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
            // Uncomment the below line to run migrations during App Startup
            // context.Database.Migrate();

            //Seed Tables with Initial Data.

            InitializeFoodItems(context);
            InitializeFoodGroups(context);


        }



        private static void InitializeFoodGroups(ApplicationDbContext context)
        {
            if (context.FoodGroups.Any())
            {
                return;//DB has been seeded.
            }

            var seedFoodGroups = new FoodGroup[]
            {
                new FoodGroup(){Name = "Vegetables"},
                new FoodGroup(){Name = "Fruits"},
                new FoodGroup(){Name = "Grains"},
                new FoodGroup(){Name = "Protein"},
                new FoodGroup(){Name = "Dairy"},
                new FoodGroup(){Name = "Oils"},
                new FoodGroup(){Name = "Other Calories"}
            };

            foreach (var item in seedFoodGroups)
            {
                context.FoodGroups.Add(item);
            }

            context.SaveChanges();
        }

        private static void InitializeFoodItems(ApplicationDbContext context)
        {
            if (context.FoodItems.Any())
            {
                return; // DB has been seeded
            }


            var seedFoodItems = new FoodItem[]
            {
                new FoodItem() {Cost = 0.00M, FoodDescription = "Romaine Lettuce", FoodItemName = "Romaine Lettuce"},
                new FoodItem() {Cost = 0.00M, FoodDescription = "Baked Beans", FoodItemName = "Canned Baked Beans"},
                new FoodItem() {Cost = 0.00M, FoodDescription = "Cage Free Egg", FoodItemName = "Egg - Scrambled"},
                new FoodItem()
                    {Cost = 0.00M, FoodDescription = "Cage Free Egg", FoodItemName = "Little Sizzlers Sausage"}


            };

            foreach (var itme in seedFoodItems)
            {
                context.FoodItems.Add(itme);
            }

            context.SaveChanges();
        }
    }
}
