using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Moq;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Models;
using MyKitchen.Models.FoodItems;
using Xunit;

namespace MyKitchen.Tests
{
    public class FoodItemTests
    {
        [Fact]
        public void CanChangeFoodItemName()
        {
            var foodItem = new FoodItem();

            foodItem.FoodItemName = "Chicken";

            Assert.Equal("Chicken", foodItem.FoodItemName);

        }
    }

    public class FoodItemControllerTests
    {
        [Theory]
        [ClassData(typeof(FoodItemTestData))]
        public void IndexActionModelIsComplete(FoodItem[] foodItems)
        {
            //arrange
            var mock = new Mock<IFoodItemRepository>();
            var dbmock = new Mock<IMyKitchenDataContext>();
            mock.SetupGet(x => x.FoodItems).Returns(foodItems.AsQueryable());

            var dbset = MockDbSetFactory.Create<FoodItem>(foodItems);
            dbmock.SetupGet(x => x.FoodItems).Returns(dbset.Object);
            var controller = new FoodItemsController(mock.Object, dbmock.Object);


            //act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as FoodItemIndexViewModel;
            var modelenum = model.FoodItems;

            //assert
            Assert.Equal(modelenum,mock.Object.FoodItems.AsEnumerable(),Comparer.Get<FoodItem>((p1,p2) => p1.FoodItemName == p2.FoodItemName));

        }

        [Fact]
        public void RepositoryPropertyCalledTwice()
        {
            //arrange
            var mock = new Mock<IFoodItemRepository>();

        }


    }

    public class FoodItemTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {GetTestFoodItems()};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private FoodItem[] GetTestFoodItems()
        {
            var item = new FoodItem[]
            {

                new FoodItem() {FoodItemID = 1, FoodItemName = "Test Item 1"},
                new FoodItem() {FoodItemID = 2, FoodItemName = "Test Item 2"},
                new FoodItem() {FoodItemID = 3, FoodItemName = "Test Item 3"},
                new FoodItem() {FoodItemID = 4, FoodItemName = "Test Item 4"},
                new FoodItem() {FoodItemID = 5, FoodItemName = "Test Item 5"},
                new FoodItem() {FoodItemID = 6, FoodItemName = "Test Item 6"},
                new FoodItem() {FoodItemID = 7, FoodItemName = "Test Item 7"},
                new FoodItem() {FoodItemID = 8, FoodItemName = "Test Item 8"},
                new FoodItem() {FoodItemID = 9, FoodItemName = "Test Item 9"},
                new FoodItem() {FoodItemID = 10, FoodItemName = "Test Item 10"},

            };

            return item;

        }





    }
}
