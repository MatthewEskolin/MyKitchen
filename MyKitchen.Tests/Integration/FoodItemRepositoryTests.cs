using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;
using MyKitchen.Models;
using Xunit;

namespace MyKitchen.Tests.Integration
{
    public class FoodItemRepositoryTests
    {

        [Fact]
        public void CanAddFoodItem()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<EFFoodItemRepository>();

             WithTestDatabase.Run((ApplicationDbContext context) => {

                var repo = new EFFoodItemRepository(context,logger);
                var datetime = DateTime.Now.ToString();
                var foodItemName = "CanAddFoodItem " + datetime; 

                var newFoodItem = new FoodItem(){
                    FoodItemName = "CanAddFoodItem " + datetime,
                    FoodDescription = "Test Description"
                };

                repo.Add(newFoodItem).Wait();;

                Assert.True(context.FoodItems.Any(x => x.FoodItemName == foodItemName));

            });

            //make sure item has been added.
            


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