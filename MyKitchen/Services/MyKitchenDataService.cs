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
        try
        {
            using (SqlConnection connection = new SqlConnection("Server=VIVE\\SQLEXPRESS;Initial Catalog=MyKitchen;Trusted_Connection=true;TrustServerCertificate=True"))
            {
                connection.Open();
                Console.WriteLine("Connection successful!");

                // Perform additional database operations here

                connection.Close();
            }

            Console.WriteLine("Database connectivity test successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to the database: {ex.Message}");
        }


        _Logger.LogInformation($"START LOG{_ctx.Database.GetConnectionString()}");

        _ctx.Database.SetConnectionString("Server=mykitchen.database.windows.net;Initial Catalog=MyKitchen;Persist Security Info=False;User ID=matteskolin;Password=sdf234F@#f!!3456AB;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        await _ctx.Database.OpenConnectionAsync();
        await _ctx.Database.CloseConnectionAsync();

    }



}