namespace MyKitchen
{
    public interface IFoodReccomendationService
    {
        string ServiceName { get; set; }
        string GetNextRecommendation();

    }
}