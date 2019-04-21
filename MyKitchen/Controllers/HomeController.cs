using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data;
using MyKitchen.Models;

namespace MyKitchen.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Let's store in the database the time we last cooked our own meal, and track how many meals we cooked overtime

            var newVariable = 5;
            ViewBag.BagItem1 = "Browser Link Change Test";
            return View();
        }

        public IActionResult Privacy()
        {
            //Time Since Last Meal
            return View();
        }

        public IActionResult NewGroceryTrip()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult NewGroceryTrip(FoodItem newFood)
        {

          
            return View();
        }
    }
}
