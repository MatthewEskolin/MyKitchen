using System.Net.Http.Json;
using MealMentor.Shared.DTO;
using Microsoft.Extensions.Logging;

namespace MealMentor.Client.API
{
    public class MealMentorAPIClient(HttpClient httpClient,ILogger<MealMentorAPIClient> logger)
    {
        //inject HttpClient
        private HttpClient HttpClient { get; set; } = httpClient;

        private ILogger<MealMentorAPIClient> _logger = logger;

        public async Task<MealDTO?> CreateMeal(MealDTO meal)
        {
            var result = await HttpClient.PostAsJsonAsync("api/meals", meal);
            MealDTO? rtn;

            try
            {
                rtn = await result.Content.ReadFromJsonAsync<MealDTO>();
            }
            catch(Exception e)
            {
                logger.LogError(e,"ERROR");
                return null;
            }

            return rtn;
        }

        public async Task<MealDTO?> GetMeal(int mealId)
        {
            MealDTO? result = await HttpClient.GetFromJsonAsync<MealDTO>($"api/meals/{mealId}");
            return result;
        }

        public async Task<List<MealImageDTO>?> GetImages(int mealId)
        {
            var mealImages = await HttpClient.GetFromJsonAsync<List<MealImageDTO>>($"api/meals/{mealId}/images");
            return mealImages;
        }

        public async Task<bool> UpdateMealName(int mealID, string mealName)
        {
            var response = await HttpClient.PutAsJsonAsync($"api/meals/{mealID}/name", mealName);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMeal(MealDTO meal)
        {
            var response = await HttpClient.PutAsJsonAsync($"api/meals/{meal.MealID}", meal);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMeal(int mealID)
        {
            var response = await HttpClient.DeleteAsync($"api/meals/{mealID}");
            return response.IsSuccessStatusCode;
        }

        //public async Task<bool> DeleteFoodItems(int mealID, int mealFoodItemID)
        //{
        //    var response = await HttpClient.DeleteAsync($"api/meals/{mealID}/fooditems/{mealFoodItemID}");
        //    return response.IsSuccessStatusCode;
        //}

        public async Task<bool> UploadFile(MultipartFormDataContent content)
        {
            var response = await HttpClient.PostAsync("/MealBuilder/UploadFile", content);

            return response.IsSuccessStatusCode;

        }

        public async Task<List<MealDTO>> GetMeals()
        {
            var result = await HttpClient.GetFromJsonAsync<List<MealDTO>>($"api/getmeals");
            return result;
        }
    }
}
