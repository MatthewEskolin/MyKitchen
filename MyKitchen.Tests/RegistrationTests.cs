using MyKitchen.Data;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MyKitchen.Tests
{
    public class RegistrationTests {


            [Fact]
            public void RegisterNewUser()
            {

            var services = new ServiceCollection();

            services.AddLogging();
            
            services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( WithTestDatabase.MyConnectionString));
   

            var um = services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
            var newUser = new ApplicationUser(){Email = "test1@test.com"};
            var result =  um.CreateAsync(newUser, "111111").GetAwaiter().GetResult();


            }




    }

}
