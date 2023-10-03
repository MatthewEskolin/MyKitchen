using MyKitchen.Data;
using MyKitchen.Models;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MyKitchen.Tests
{
    public class SharedTestContext : IDisposable
    {
        public SharedTestContext()
        {
                
                var services = new ServiceCollection();

                services.AddLogging();
                
                services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

                //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( WithTestDatabase.MyConnectionString));
                services.AddTransient<EFFoodItemRepository>();
                services.AddTransient<EfMealRepository>();

                var sp = services.BuildServiceProvider();

                TestFoodItemRepository = sp.GetService<EFFoodItemRepository>();
                TestMealRepository = sp.GetService<EfMealRepository>();
                TestUserManager = sp.GetService<UserManager<ApplicationUser>>();
                ApDbContext = sp.GetService<ApplicationDbContext>();

                


        }

        public EFFoodItemRepository TestFoodItemRepository { get; private set; }
        public EfMealRepository TestMealRepository { get; private set; }
        public UserManager<ApplicationUser> TestUserManager { get; private set; }
        public ApplicationDbContext ApDbContext { get; private set; }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }

    }


    public class DataIntegrityTests : IClassFixture<SharedTestContext>{


        
            SharedTestContext Fixture;

            public DataIntegrityTests(SharedTestContext fixture)
            {
                this.Fixture = fixture;
            }

        [Fact]
        //All Foods Associated with a meal also need to be associated with the user who owns the meal.
        public void MealUsersCheck()
        {
            var badRecords = Fixture.ApDbContext.vwsMealItems.Any(x => x.UserFoodItemsAppUser == null);
            Assert.True(!badRecords);

        }


        //check changes

    }


    public class RegistrationTests :IClassFixture<SharedTestContext>{



            SharedTestContext Fixture;

            public RegistrationTests(SharedTestContext fixture)
            {
                this.Fixture = fixture;
            }


            [Fact]
            public void RegisterNewUser1()
            {
                //append guid so test coan be repeated
                var guid = Guid.NewGuid().ToString();
                var newUser = new ApplicationUser(){Email = $"{guid}@test.com",UserName = $"{guid}@test.com"};
                var result =  Fixture.TestUserManager.CreateAsync(newUser, "aA11111!").GetAwaiter().GetResult();

                Assert.True(result.Succeeded);

            }

            [Fact]
            public void RegisterNewUser2()
            {
                //append guid so test coan be repeated
                var guid = Guid.NewGuid().ToString();
                var newUser = new ApplicationUser(){Email = $"{guid}@test.com", UserName=$"{guid}@test.com"};
                var result =  Fixture.TestUserManager.CreateAsync(newUser, "aA11111!").GetAwaiter().GetResult();

                Assert.True(result.Succeeded);


            }



            [Fact]
            public void DeleteUser()
            {
                //append guid so test coan be repeated
                var deleteUser = Fixture.TestUserManager.FindByEmailAsync("mjeskolin@gmail.com").GetAwaiter().GetResult();
                var result = Fixture.TestUserManager.DeleteAsync(deleteUser).GetAwaiter().GetResult();
                Assert.True(result.Succeeded);
            }



            [Fact]
            public void FoodItemsAreUserIsolated()
            {

                var guid1 = Guid.NewGuid();
                var guid2 = Guid.NewGuid();

                Assert.True(true);
                if(Fixture.ApDbContext.Users.Count() < 2){
                    Assert.True(false, "Inconclusive - need 2 users in DB");
                }

                var users = Fixture.ApDbContext.Users.Take(2).ToList();

                //get user 1
                //get user 2
                var user1 = Fixture.TestUserManager.FindByIdAsync(users[0].Id).GetAwaiter().GetResult();
                var user2 = Fixture.TestUserManager.FindByIdAsync(users[1].Id).GetAwaiter().GetResult();;

                //food item 1
                //food item 2
                var foodItem1 = new FoodItem(){
                    FoodItemName = guid1.ToString(),
                    FoodDescription = "Test Item 3" + guid1
                };

                var foodItem2 = new FoodItem(){
                    FoodItemName = guid2.ToString(),
                    FoodDescription = "Test Item 3" + guid2
                };

                //add food item for user 1
                Fixture.TestFoodItemRepository.Add(foodItem1).GetAwaiter().GetResult();
                Fixture.TestFoodItemRepository.AddFoodForUser(user1,foodItem1).GetAwaiter().GetResult();

                //get food items for user 2
                Fixture.TestFoodItemRepository.Add(foodItem2).GetAwaiter().GetResult();;
                Fixture.TestFoodItemRepository.AddFoodForUser(user2,foodItem2).GetAwaiter().GetResult();

                //Assert
                Assert.True(Fixture.ApDbContext.FoodItems.Any(x => x.FoodItemID == foodItem1.FoodItemID));
                Assert.True(Fixture.ApDbContext.FoodItems.Any(x => x.FoodItemID == foodItem2.FoodItemID));

                //Assert User Food Items
                Assert.True(Fixture.ApDbContext.UserFoodItems.Any(x => x.FoodItemID == foodItem1.FoodItemID && x.AppUser.Id == user1.Id));
                Assert.True(Fixture.ApDbContext.UserFoodItems.Any(x => x.FoodItemID == foodItem2.FoodItemID && x.AppUser.Id == user2.Id));
    }
}
}
