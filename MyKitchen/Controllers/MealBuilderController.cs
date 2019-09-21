using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data;
using MyKitchen.Models;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKitchen.Controllers
{
    [Authorize]
    public class MealBuilderController : Controller
    {

        private readonly IFoodItemRepository foodItemRepository;
        private readonly IMealRepository mealRepository;
        private ApplicationDbContext context;

        public MealBuilderController(IFoodItemRepository foodItemRepo,IMealRepository mealRepo,ApplicationDbContext ctx)
        {
            context = ctx;
            foodItemRepository = foodItemRepo;
            mealRepository = mealRepo;
        }

        // GET: /<controller>/
        public IActionResult Index(int foodItemPage = 1, int mealListPage = 1)
        {
            var viewModel1 = new MealBuilderIndexViewModel()
            {
                Meals = mealRepository.GetMeals(),
                MealListPagingInfo = new PagingInfo() { CurrentPage = mealListPage,ItemsPerPage = 15,TotalItems = mealRepository.Count()}
            };

            return View(viewModel1);
        }

        public IActionResult Create()
        {
            var mealFactory = new MealFactory(context);
            Meal meal = mealFactory.NewMeal();

            var viewModel = new MealBuilderCreateViewModel()
            {
                FoodItems = foodItemRepository.GetFoodItems()
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewMeal(Meal newMeal)
        {
            if (ModelState.IsValid)
            {
                await mealRepository.Add(newMeal);

            }

            return RedirectToAction("Index");

        }
    }
}
