using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;
using MyKitchen.Models;
using Xunit;

namespace MyKitchen.Tests.Integration
{
    /// <summary>
    /// Test Live Database Connection through the EFFoodItemRepository
    /// </summary>
    public class FoodItemRepositoryTests
    {
        private IConfiguration _config;
        private ServiceProvider _serviceProvider;
        private ILoggerFactory _factory;


        private void Configure()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")
                .Build();

            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            _factory = _serviceProvider.GetService<ILoggerFactory>();

        }

        [Fact]
        public void CanAddFoodItem()
        {
            Configure();

            var logger = _factory.CreateLogger<EFFoodItemRepository>();

            WithTestDatabase wtd = new WithTestDatabase(_config.GetConnectionString("VIVE"));


             wtd.Run(context => {

                var repo = new EFFoodItemRepository(context,logger);

                var datetime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

                var foodItemName = "CanAddFoodItem " + datetime; 

                var newFoodItem = new FoodItem(){
                    FoodItemName = "CanAddFoodItem " + datetime,
                    FoodDescription = "Test Description"
                };

                //act
                repo.Add(newFoodItem).Wait();;

                //assert
                var pass = context.FoodItems.Any(x => x.FoodItemName == foodItemName);
                Assert.True(pass);

                //cleanup
                var item = context.FoodItems.FirstOrDefault(x => x.FoodItemName == foodItemName);
                context.FoodItems.Remove(item);
                context.SaveChanges();

             });

        }


        //[Fact(Skip="In Development")]
        [Fact]
        public  void CanAddUserFoodItem()
        {
            Configure();

            var logger = _factory.CreateLogger<EFFoodItemRepository>();

            WithTestDatabase wtd = new WithTestDatabase(_config.GetConnectionString("VIVE"));


            wtd.Run( context =>
            {

                var repo = new EFFoodItemRepository(context, logger);

                var datetime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

                var foodItemName = "CanAddFoodItem " + datetime;

                var newFoodItem = new FoodItem()
                {
                    FoodItemName = "CanAddFoodItem " + datetime,
                    FoodDescription = "Test Description"
                };


                var user = new ApplicationUser()
                {
                    //use ID of a valid test user
                    Id = "92e49dc3-a9db-4c00-93b0-4c969242c8c1",
                    UserName = "matteskolin@gmail.com",
                    Email = "matteskolin@gmail.com"
                };

                context.Users.Attach(user);

                repo.Add(newFoodItem).Wait();
                repo.AddFoodForUser(user, newFoodItem).Wait();

                //assert
                var pass = context.FoodItems.Any(x => x.FoodItemName == foodItemName);
                Assert.True(pass);

                //cleanup
                var item = context.FoodItems.FirstOrDefault(x => x.FoodItemName == foodItemName);
                if (item != null) context.FoodItems.Remove(item);
                context.SaveChanges();

            });
        }


    }

}