using Microsoft.AspNetCore.Identity;

namespace MyKitchen.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FavoriteFood { get; set; }
    }

}
