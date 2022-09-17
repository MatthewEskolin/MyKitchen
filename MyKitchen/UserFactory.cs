using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyKitchen.Data;

namespace MyKitchen;

public class UserFactory
{

    public static ApplicationUser GetUser(IServiceProvider serviceProvider)
    {
        IHttpContextAccessor accessor = serviceProvider.GetService<IHttpContextAccessor>();
        UserManager<ApplicationUser> manager = serviceProvider.GetService<UserManager<ApplicationUser>>();
        if (manager != null)
        {
            if (accessor != null)
            {
                var user = manager.GetUserAsync(accessor.HttpContext.User).GetAwaiter().GetResult();

                return user;
            }
        }

        throw new Exception("Failure getting Application User object");
    }
}