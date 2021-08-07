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
        [Fact]
        public void RepositoryPropertyCalledTwice()
        {
            //Arrange
            var mock = new Mock<IFoodItemRepository>();
            var dbmock = new Mock<IMyKitchenDataContext>();
            var usermock = new Mock<UserInfo>();
            mock.Setup(x => x.GetFoodItems()).Returns(new[] { new FoodItem {FoodItemName = "test 1"} }.AsQueryable);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<FoodItemsController>>();

            var controller = new FoodItemsController(mock.Object, dbmock.Object,mockLogger.Object,usermock.Object);

            //Act
            var result = controller.Index();

            //Assert
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