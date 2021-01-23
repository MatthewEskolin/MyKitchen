using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyKitchen.BL;
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
            var usermock = new Mock<UserInfo>();
            mock.Setup(x => x.GetFoodItems()).Returns(new[] { new FoodItem {FoodItemName = "test 1"} }.AsQueryable);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<FoodItemsController>>();

            var controller = new FoodItemsController(mock.Object, dbmock.Object,mockLogger.Object,usermock.Object);

            //act
            var result = controller.Index();

            //assert
            mock.Verify(x => x.GetFoodItems(),Times.AtMost(2));

        }
#region
        [Fact]
        public void Can_Paginate()
        {
            //TODO figure out how to moq the user
            //https://justintimecoder.com/useful-skills-with-moq/
            

            //unit tests are not supposed to rely on DB

            //arrange
            //arrange
            var testPageSize = 2;
            var testPageIndex = 3;

            var userMock = new Mock<UserInfo>();
            var applicationUserMock = new Mock<ApplicationUser>();
            var mock = new Mock<IFoodItemRepository>();
            var dbmock = new Mock<IMyKitchenDataContext>();
            mock.Setup(x => x.GetFoodItems()).Returns(new[]
            {
                new FoodItem() {FoodDescription = "FI1"},
                new FoodItem() {FoodDescription = "FI2"},
                new FoodItem() {FoodDescription = "FI3"},
                new FoodItem() {FoodDescription = "FI4"},

                //These should appear on Page 3
                new FoodItem() {FoodDescription = "FI5"},
                new FoodItem() {FoodDescription = "FI6"},
            }.AsQueryable);


            mock.Setup(x => x.GetFoodItemsForUser(userMock.Object.User)).Returns(new[]
            {
                new FoodItem() {FoodDescription = "FI1"},
                new FoodItem() {FoodDescription = "FI2"},
                new FoodItem() {FoodDescription = "FI3"},
                new FoodItem() {FoodDescription = "FI4"},

                //These should appear on Page 3
                new FoodItem() {FoodDescription = "FI5"},
                new FoodItem() {FoodDescription = "FI6"},
            }.AsQueryable);


            // mock.SetupGet(x => x.GetFoodItemsForUser).Returns


            // userMock.SetupGet(x => x.UserV

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<FoodItemsController>>();
            var controller = new FoodItemsController(mock.Object, dbmock.Object,mockLogger.Object,userMock.Object) {PageSize = testPageSize};

            //act
            var result = (controller.Index(testPageIndex) as ViewResult)?.ViewData.Model as FoodItemIndexViewModel;

            //assert
            FoodItem[] foodItemArray = result.FoodItems.ToArray();
            Assert.True(foodItemArray.Length == 2);
            Assert.Equal("FI5", foodItemArray[0].FoodDescription);
            Assert.Equal("FI6", foodItemArray[1].FoodDescription);

        }

    }

    #endregion
}