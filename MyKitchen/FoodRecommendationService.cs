using MyKitchen.Models;

namespace MyKitchen
{
    public class FoodRecommendationService:IFoodReccomendationService
    {
        private IFoodItemRepository repo;
        public FoodRecommendationService(IFoodItemRepository prepo)
        {
            repo = prepo;

        }

        public string ServiceName { get; set; }
        public string GetNextRecommendation()
        {
            //get random item
            var foodItem = repo.GetRandomItem();
            return foodItem.FoodItemName;
        }
    }
}