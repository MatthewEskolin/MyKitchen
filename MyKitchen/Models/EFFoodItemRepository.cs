using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Models
{
    public class EFFoodItemRepository:IFoodItemRepository
    {
        private ApplicationDbContext context;
        public EFFoodItemRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<FoodItem> FoodItems => context.FoodItems;
    }
}
