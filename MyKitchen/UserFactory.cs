namespace MyKitchen;

public class UserFactory
{
    public static ApplicationUser GetUser(IServiceProvider serviceProvider)
    {
        var accessor = serviceProvider.GetService<IHttpContextAccessor>();
        var manager = serviceProvider.GetService<UserManager<ApplicationUser>>();
        if (manager != null)
            if (accessor != null)
            {
                var user = manager.GetUserAsync(accessor.HttpContext.User).GetAwaiter().GetResult();

                return user;
            }

        throw new Exception("Failure getting Application User object");
    }
}