using MyKitchen.Data;
using MyKitchen.Models;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Moq;
using MyKitchen.Controllers;
using MyKitchen.BL;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MyKitchen.Tests
{

    public class MealBuilderControllerTests :IClassFixture<SharedTestContext>{



        SharedTestContext Fixture;

        public MealBuilderControllerTests(SharedTestContext fixture)
        {
            this.Fixture = fixture;
        }

        [Fact]
        public void Index_PopulateViewModel()
        {
            //Tests to make sure the Meal Repository when called by the MealBuilderController is being populated using the a test page number
            //and test page index.

            var testPageIndex = 3;

            var userMock = new Mock<UserInfo>();
            var applicationUserMock = new Mock<ApplicationUser>();

            var mkMealRepo = new Mock<IMealRepository>();
            var mkFoodItemRepo = new Mock<IFoodItemRepository>();
            var dbmock = new Mock<ApplicationDbContext>();

            var meals = new List<Meal>(){
                new Meal(){MealName = "Meal1"},
                new Meal(){MealName = "Meal2"},
            };

            var info = new PagingInfo();

            var controller = new MealBuilderController(mkFoodItemRepo.Object, mkMealRepo.Object,dbmock.Object,userMock.Object) {PageSize = 3};
            mkMealRepo.Setup(x => x.GetMealsForUser(controller.PageSize, testPageIndex, userMock.Object.User)).Returns((meals,info));

            //act
            var result = (controller.Index(testPageIndex) as ViewResult)?.ViewData.Model as MealBuilderIndexViewModel;

            Meal[] mealArray = result.Meals.ToArray();
            Assert.True(mealArray.Length == 2);
            Assert.Equal("Meal1", mealArray[0].MealName);
            Assert.Equal("Meal2", mealArray[1].MealName);
        }





}
}
