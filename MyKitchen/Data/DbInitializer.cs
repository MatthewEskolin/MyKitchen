using System.Linq;

namespace MyKitchen.Data
{
    public class DbInitializer
    {
        private readonly InitializeApplicationDbContext _ctx;

        public DbInitializer(InitializeApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Initialize()
        {
            //RUN MIGRATIONS 
            //context.Database.Migrate();
            //context.Database.EnsureCreated();

            InitializeFoodItems(_ctx);
            InitializeFoodGroups(_ctx);



        }

        private static void InitializeFoodGroups(InitializeApplicationDbContext context)
        {
            if (context.FoodGroups.Any())
            {
                return;            }

            var seedFoodGroups = new FoodGroup[]
            {
                new(){Name = "Vegetables"},
                new(){Name = "Fruits"},
                new(){Name = "Grains"},
                new(){Name = "Protein"},
                new(){Name = "Dairy"},
                new(){Name = "Oils"},
                new(){Name = "Other Calories"}
            };

            foreach (var item in seedFoodGroups)
            {
                context.FoodGroups.Add(item);
            }

            context.SaveChanges();
        }

        private static void InitializeFoodItems(InitializeApplicationDbContext context)
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
