using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Models;
using MyKitchen.Services;

namespace MyKitchen.Controllers
{
    [Authorize]
    public class WhatShouldIEatController : Controller
    {
        private IFoodReccomendationService FoodRecService { get; }

         public WhatShouldIEatController(IFoodReccomendationService pFoodRecService)
        {
            FoodRecService = pFoodRecService;
        }


        public IActionResult DisplayCurrentPrediction()
        {
            var viewModel = new DisplayCurrentPredictionViewModel() {FoodEntityName = FoodRecService.GetNextRecommendation()};
            return View(viewModel);
        }


        public IActionResult ShowItem()
        {

            var viewModel = new DisplayCurrentPredictionViewModel() { FoodEntityName = FoodRecService.GetNextRecommendation() };
            return PartialView(viewModel);
        }

        public IActionResult FoodAccepted()
        {
            return View();
        }
    }
}