using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data;
using MyKitchen.Models;
using NonFactors.Mvc.Grid;



namespace MyKitchen.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext ctx { get; set; }
        public HomeController(ApplicationDbContext pctx)
        {
            var errorarray = new int[4];
          //  var runtimerror = errorarray[34];

            ctx = pctx;
        }
        public IActionResult Index()
        {
            //Let's store in the database the time we last cooked our own meal, and track how many meals we cooked overtime
            //var ctx = new DataContext
          //  var ctx = new ApplicationDbContext();



            var newVariable = 5;
            ViewBag.BagItem1 = "Browser Link Change Test";

            //CAN WE CREATE A model contains food items and a lost


            var viewModel = new IndexViewModel()
            {
                FoodItemList = ctx.FoodItems,
                

            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult SelectFoodItem(int id)
        {

            var item = ctx.FoodItems.FirstOrDefault(x => x.FoodItemID == id);
            return View(item);

        }

        [HttpGet]
        public PartialViewResult IndexGrid()
        {
            // Only grid query values will be available here.
            return PartialView("_IndexGrid", ctx.FoodItems);
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
        public IActionResult SaveNewFood(IndexViewModel viewModel1 )
        {
            FoodItem newItem = viewModel1.NewFoodItem;
                       
            ctx.Add(newItem);
            ctx.SaveChanges();

            var newModel = new IndexViewModel()
            {
                FoodItemList = ctx.FoodItems

            };

            return View("Index", newModel);

    
        }


        [HttpPost]
        public IActionResult NewGroceryTrip(FoodItem newFood)
        {

          
            return View();
        }
    }
}
