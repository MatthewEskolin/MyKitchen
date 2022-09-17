using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyKitchen.Data;

namespace MyKitchen.Models.BL
{
    public class UserInfo
    {
            //the current user can be pulled from the HttpContext -> this should happen once per request which is handles by IsScoped at service registration
            private UserManager<ApplicationUser> manager {get; set;}

            public ApplicationUser User {get; set;}

            public UserInfo()
            {

            }

            public UserInfo(IHttpContextAccessor contextAccessor,UserManager<ApplicationUser> userManager){

                manager = userManager;
                User = manager.GetUserAsync(contextAccessor.HttpContext.User).GetAwaiter().GetResult();

        }
    }


}