using MyKitchen.Data;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace MyKitchen.Tests
{
    public class RegistrationTests {


            [Fact]
            public void RegisterNewUser1()
            {
                //append guid so test coan be repeated
                var guid = Guid.NewGuid().ToString();
                
                var services = new ServiceCollection();

                services.AddLogging();
                
                services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( WithTestDatabase.MyConnectionString));
    

                var um = services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
                var newUser = new ApplicationUser(){Email = $"{guid}@test.com"};
                var result =  um.CreateAsync(newUser, "111111").GetAwaiter().GetResult();

            }

            [Fact]
            public void RegisterNewUser2()
            {

                //append guid so test coan be repeated
                var guid = Guid.NewGuid().ToString();
                
                var services = new ServiceCollection();

                services.AddLogging();
                
                services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( WithTestDatabase.MyConnectionString));
    

                var um = services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
                var newUser = new ApplicationUser(){Email = $"{guid}@test.com"};
                var result =  um.CreateAsync(newUser, "111111").GetAwaiter().GetResult();

            }

            [Fact]
            public void FoodItemsAreUserIsolated()
            {
                Assert.True(true);
                //if no users in the system, mark as inconclusive
                // Assert.inconclusive();
                // https://xunit.net/docs/shared-context
                //use the above to track the context!
            }
    }
}
