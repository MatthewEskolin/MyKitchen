using MyKitchen.Data;
using MyKitchen.Models;
using Xunit;
using System.Linq;
using Moq;
using MyKitchen.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyKitchen.Models.BL;
using MyKitchen.Services;

namespace MyKitchen.Tests
{

    public class MealBuilderControllerTests {

        [Fact]
        public void Index_PopulateViewModel()
        {
            //Tests to make sure the Meal Repository when called by the MealBuilderController is being populated using the a test page number
            //and test page size.
            var testPageSize = 3;


            var mkMealRepo = new Mock<IMealRepository>();
            var mkFoodItemRepo = new Mock<IFoodItemRepository>();
            var mkImageService = new Mock<IMealImageService>();
            var mkIuserinfo = new Mock<IUserInfo>();
            var mkUserInfo = new Mock<UserInfo>();
            var mkContextAccessor = new Mock<IHttpContextAccessor>();

            var mkConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

            var meals = new List<Meal>(){
                new Meal(){MealName = "Meal1"},
                new Meal(){MealName = "Meal2"},
            };

            var info = new PagingInfo();

            mkMealRepo.Setup(x => x.GetMeals(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),It.IsAny<string>()))
                        .Returns((meals,info));




            var controller = new MealBuilderController(mkImageService.Object,
                                                       mkFoodItemRepo.Object, 
                                                       mkMealRepo.Object,
                                                       mkConfiguration.Object,
                                                       mkIuserinfo.Object,
                                                       mkContextAccessor.Object)
                                                      
                                                       {PageSize = testPageSize};

            //act

            var result = (controller.Index(new MealBuilderIndexViewModel()) as ViewResult)?.ViewData.Model as MealBuilderIndexViewModel;

            Meal[] mealArray = result.Meals.ToArray();
            Assert.True(mealArray.Length == 2);
            Assert.Equal("Meal1", mealArray[0].MealName);
            Assert.Equal("Meal2", mealArray[1].MealName);
        }


        //

}
}
