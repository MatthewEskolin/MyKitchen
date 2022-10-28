using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data;
using MyKitchen.Models;
using MyKitchen.Models.Home;

namespace MyKitchen.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext Ctx { get; set; }

        public HomeController(ApplicationDbContext pctx)
        {
            Ctx = pctx;
        }

        [Route("Home")]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexVM();
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult SelectFoodItem(int id)
        {

            var item = Ctx.FoodItems.FirstOrDefault(x => x.FoodItemID == id);
            return View(item);

        }

        [HttpGet]
        public PartialViewResult IndexGrid()
        {
            // Only grid query values will be available here.
            return PartialView("_IndexGrid", Ctx.FoodItems);
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
        public IActionResult SaveNewFood(HomeIndexVM viewModel1 )
        {
            FoodItem newItem = viewModel1.NewFoodItem;
                       
            Ctx.Add(newItem);
            Ctx.SaveChanges();

            var newModel = new HomeIndexVM()
            {
                FoodItemList = Ctx.FoodItems.ToList()

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
