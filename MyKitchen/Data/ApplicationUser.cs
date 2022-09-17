using Microsoft.AspNetCore.Identity;
using MyKitchen.Models.BL;

namespace MyKitchen.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser,IUserInfo
    {
        public string FavoriteFood { get; set; }

        public string MealImage { get; set; } 


    }

    public interface IUserInfo
    {
        string Id { get; set; }
    }

}
