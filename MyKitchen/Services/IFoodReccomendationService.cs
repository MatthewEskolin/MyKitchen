namespace MyKitchen.Services
{
    public interface IFoodReccomendationService
    {
        string ServiceName { get; set; }
        string GetNextRecommendation();

    }
}