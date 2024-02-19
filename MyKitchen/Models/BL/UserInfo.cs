using System.Security.Claims;
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

            public UserInfo(UserManager<ApplicationUser> userManager,string userEmail)
            {
                manager = userManager;

                //create claimsprincipal for user with email address
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.Email, userEmail));
                var claimsPrincipal = new ClaimsPrincipal(claims);

                User = manager.GetUserAsync(claimsPrincipal).GetAwaiter().GetResult();

            }

        public UserInfo(IHttpContextAccessor contextAccessor,UserManager<ApplicationUser> userManager)
            {
                manager = userManager;

                User = manager.GetUserAsync(contextAccessor.HttpContext.User).GetAwaiter().GetResult();




            }




    }


}