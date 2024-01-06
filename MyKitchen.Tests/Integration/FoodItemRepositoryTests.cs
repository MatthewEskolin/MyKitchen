using System;
using System.Globalization;
using System.Linq;
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


        [Fact(Skip="In Development")]
        public void CanAddUserFoodItem()
        {
 
            //arrange
                //dependencies
                    //user 
            //dependencies
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<EFFoodItemRepository>();


                   // UserManager<ApplicationUser> userManager


            //act


            //assert
            Assert.True(true);
        }


    }

}