using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKitchen.Controllers
{
    [Authorize]
    public class MealBuilderController : Controller
    {
        private readonly IFoodItemRepository foodItemRepository;
        private readonly IMealRepository mealRepository;

        public MealBuilderController(IFoodItemRepository foodItemRepo,IMealRepository mealRepo)
        {
            foodItemRepository = foodItemRepo;
            mealRepository = mealRepo;
        }

        // GET: /<controller>/
        public IActionResult Index(int foodItemPage = 1, int mealListPage = 1)
        {
            var viewModel1 = new MealBuilderIndexViewModel()
            {
                FoodItems = foodItemRepository.GetFoodItems(),
                Meals = mealRepository.GetMeals(),
                FoodItemPagingInfo = new PagingInfo(){CurrentPage = foodItemPage,ItemsPerPage = 50,TotalItems = foodItemRepository.FoodItems.Count()},
                MealListPagingInfo = new PagingInfo() { CurrentPage = mealListPage,ItemsPerPage = 15,TotalItems = mealRepository.Count()}
            };

            return View(viewModel1);
        }
    }
}
