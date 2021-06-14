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
using Microsoft.AspNetCore.Hosting;
using MyKitchen.Services;

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
            var mkImageService = new Mock<IMealImageService>();
            var mkHostingEnv = new Mock<IWebHostEnvironment>();
            var mkConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

            var meals = new List<Meal>(){
                new Meal(){MealName = "Meal1"},
                new Meal(){MealName = "Meal2"},
            };

            var info = new PagingInfo();

            var controller = new MealBuilderController(mkImageService.Object,mkHostingEnv.Object,mkFoodItemRepo.Object, mkMealRepo.Object,dbmock.Object,mkConfiguration.Object,userMock.Object) {PageSize = 3};
            mkMealRepo.Setup(x => x.GetMealsForUser(controller.PageSize, testPageIndex, userMock.Object.User,string.Empty)).Returns((meals,info));

            //act
            var result = (controller.Index(testPageIndex) as ViewResult)?.ViewData.Model as MealBuilderIndexViewModel;

            Meal[] mealArray = result.Meals.ToArray();
            Assert.True(mealArray.Length == 2);
            Assert.Equal("Meal1", mealArray[0].MealName);
            Assert.Equal("Meal2", mealArray[1].MealName);
        }





}
}
