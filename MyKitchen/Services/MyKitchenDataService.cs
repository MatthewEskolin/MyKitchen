using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;
using MyKitchen.Models.BL;

namespace MyKitchen.Services;

public class MyKitchenDataService : IMyKitchenDataService
{
    public ApplicationDbContext _ctx { get; set; }
    private ILogger _Logger;

    private MyKitchen.Models.BL.UserInfo  _userInfo;
    private readonly UserManager<ApplicationUser> _userManager;

    public MyKitchenDataService(ILogger<MyKitchenDataService> logger, ApplicationDbContext ctx, UserInfo userInfo, UserManager<ApplicationUser> userManager)
    {
        _Logger = logger;
        _ctx = ctx;
        _userInfo = userInfo;
        _userManager = userManager;
    }

    public async Task UpdateSettingsAsync(string settings)
    {
        var user = await _userManager.FindByIdAsync(_userInfo.User.Id);

        user.MealImage = settings;
        user.FavoriteFood = "food test";

        user.PhoneNumber = "222";

        IdentityResult result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            //Success
        }
        else
        {
            _Logger.LogError($"Failed to Update User {result.Errors}");
        }



    }

    public async Task TestSQLConnectivity()
    {
        _Logger.LogInformation($"Testing SQL Connectivity to {_ctx.Database.GetConnectionString()}");

        await _ctx.Database.OpenConnectionAsync();
        await _ctx.Database.CloseConnectionAsync();

        _Logger.LogInformation("DB Connection Sucess");

    }



}