using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Models;

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
    }
}