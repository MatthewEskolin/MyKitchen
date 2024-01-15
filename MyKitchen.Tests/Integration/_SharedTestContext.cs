using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyKitchen.Data;
using MyKitchen.Models;
using MyKitchen.Models.BL;

namespace MyKitchen.Tests.Integration;

[UsedImplicitly]
public class _SharedTestContext : IDisposable
{
    public _SharedTestContext()
    {
                
        var services = new ServiceCollection();

        services.AddLogging();
                
        services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        //get connection string from appsettings.json
        var config = new ConfigurationBuilder()
            .AddJsonFile("AppSettings.json")
            .Build();

        var conn = GetConnectionString();
        string GetConnectionString()
        {
            var viveConn = config.GetConnectionString("VIVE");
            if (viveConn == null) throw new Exception("Connection String is Null");
            return viveConn;
        }

        services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(conn); });
        services.AddTransient<EFFoodItemRepository>();
        services.AddTransient<EfMealRepository>();
        services.AddTransient<UserManager<ApplicationUser>>();
        services.AddTransient(ImplementationFactory);


        //mock the IhttpContextAccessor, and return the test user from the db when calling getuserasync
        //services.AddHttpContextAccessor();
        //services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, TestUserClaimsPrincipalFactory>();



        var sp = services.BuildServiceProvider();

        TestFoodItemRepository = sp.GetService<EFFoodItemRepository>();
        TestMealRepository = sp.GetService<EfMealRepository>();
        TestUserManager = sp.GetService<UserManager<ApplicationUser>>();
        ApDbContext = sp.GetService<ApplicationDbContext>();
    }

    private UserInfo ImplementationFactory(IServiceProvider arg)
    {
        var userManager = arg.GetService<UserManager<ApplicationUser>>();
        return new UserInfo(userManager,TestUserEmail);
    }

    public EFFoodItemRepository TestFoodItemRepository { get; private set; }
    public EfMealRepository TestMealRepository { get; private set; }
    public UserManager<ApplicationUser> TestUserManager { get; private set; }
    public ApplicationDbContext ApDbContext { get; private set; }

    public string TestUserEmail { get; set; } = "matteskolin@gmail.com";

    public void Dispose()
    {
        // ... clean up test data from the database ...
    }

}