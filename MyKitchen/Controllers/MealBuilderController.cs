using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKitchen.Controllers
{
    public class MealBuilderController : Controller
    {
        IFoodItemRepository 

        public MealBuilderController(IFoodItemRepository foodItemRepo)
        {

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
