using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyKitchen.Data;

namespace MyKitchen.BL
{
    public class UserInfo
    {
        //the current user can be pulled from the HttpContext -> this should happen once per request which is handles by IsTransient in the constructor.
            public UserManager<ApplicationUser> manager {get; set;}

            public ApplicationUser User {get; set;}

            public UserInfo(IHttpContextAccessor contextAccessor){

                User = manager.GetUserAsync(contextAccessor.HttpContext.User).GetAwaiter().GetResult();

        }
    }
}