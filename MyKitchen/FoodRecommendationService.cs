using System;
using System.Linq;
using MyKitchen.Data;

    namespace MyKitchen
{
    public class FoodRecommendationService:IFoodReccomendationService
    {
        private ApplicationDbContext ctx;
        public FoodRecommendationService(ApplicationDbContext pctx)
        {
            ctx = pctx;
        }

        public string ServiceName { get; set; }
        public string GetNextRecommendation()
        {
            IQueryable<vwsMealsAndFoodItems> items = ctx.vwsMealsAndFoodItems.AsQueryable();//.     return _context.FoodItems.OrderBy(x => Guid.NewGuid()).FirstOrDefault(); ToList();
            var rec = SelectRandItem(items);
            return rec.ItemName;
        }

        private T SelectRandItem<T>(IQueryable<T> queryable)
        {
            return queryable.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}