﻿namespace MyKitchen;

[UsedImplicitly]
public class UserFactory
{
    public static ApplicationUser GetUser(IServiceProvider serviceProvider)
    {
        var accessor = serviceProvider.GetService<IHttpContextAccessor>();
        var manager = serviceProvider.GetService<UserManager<ApplicationUser>>();

        if (manager != null && accessor?.HttpContext != null)
        {
            var user = manager.GetUserAsync(accessor.HttpContext.User).GetAwaiter().GetResult();
            return user;
        }

        throw new Exception("Failure getting Application User object");
    }
}