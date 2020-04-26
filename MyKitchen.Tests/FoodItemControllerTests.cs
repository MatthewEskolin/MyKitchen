using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Models;
using MyKitchen.Models.FoodItems;
using Xunit;

namespace MyKitchen.Tests
{
    public class FoodItemControllerTests
    {
        //BEGIN CODE EXAMPLE
        //Below is an example of how to use test data in an xUnit Test         

        // [Theory(Skip="Example For Test Data Class - not a valid test")]
        // [ClassData(typeof(FoodItemTestData))]
        // public void IndexActionModelIsComplete(FoodItem[] foodItems)
        // {
        //     //arrange
        //     var mock = new Mock<IFoodItemRepository>();
        //     var dbmock = new Mock<IMyKitchenDataContext>();
        //     mock.SetupGet(x => x.FoodItems).Returns(foodItems.AsQueryable());

        //     var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<FoodItemsController>>();


        //     var dbset = MockDbSetFactory.Create<FoodItem>(foodItems);
        //     dbmock.SetupGet(x => x.FoodItems).Returns(dbset.Object);
        //     var controller = new FoodItemsController(mock.Object, dbmock.Object,mockLogger.Object);


        //     //act
        //     var model = (controller.Index() as ViewResult)?.ViewData.Model as FoodItemIndexViewModel;
        //     var modelenum = model.FoodItems;

        //     //assert
        //     Assert.Equal(modelenum,mock.Object.FoodItems.AsEnumerable(),Comparer.Get<FoodItem>((p1,p2) => p1.FoodItemName == p2.FoodItemName));

        // }
        // END CODE EXAMPLE

        [Fact]
        public void RepositoryPropertyCalledTwice()
        {
            //called once for entities - once for count

            //arrange
            var mock = new Mock<IFoodItemRepository>();
            var dbmock = new Mock<IMyKitchenDataContext>();
            mock.SetupGet(x => x.FoodItems).Returns(new[] { new FoodItem {FoodItemName = "test 1"} }.AsQueryable);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<FoodItemsController>>();

            var controller = new FoodItemsController(mock.Object, dbmock.Object,mockLogger.Object);

            //act
            var result = controller.Index();

            //assert
            mock.VerifyGet(x => x.FoodItems,Times.AtMost(2));

        }

        [Fact]
        public void Can_Paginate()
        {
            //arrange
            //arrange
            var mock = new Mock<IFoodItemRepository>();
            var dbmock = new Mock<IMyKitchenDataContext>();
            mock.SetupGet(x => x.FoodItems).Returns(new[]
            {
                new FoodItem() {FoodDescription = "FI1"},
                new FoodItem() {FoodDescription = "FI2"},
                new FoodItem() {FoodDescription = "FI3"},
                new FoodItem() {FoodDescription = "FI4"},
                new FoodItem() {FoodDescription = "FI5"},
            }.AsQueryable);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<FoodItemsController>>();


            var controller = new FoodItemsController(mock.Object, dbmock.Object,mockLogger.Object) {PageSize = 2};


            //act
            var result = (controller.Index(3) as ViewResult)?.ViewData.Model as FoodItemIndexViewModel;

            //assert
            FoodItem[] foodItemArray = result.FoodItems.ToArray();
            Assert.True(foodItemArray.Length == 2);
            Assert.Equal("FI4", foodItemArray[0].FoodDescription);
            Assert.Equal("FI5", foodItemArray[1].FoodDescription);

        }

    }
}