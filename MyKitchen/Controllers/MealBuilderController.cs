using System.Linq;
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
        public async Task<IActionResult> SaveNewMeal(MealBuilderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await mealRepository.Add(model.Meal);

            }

            return RedirectToAction("Index");

        }

        public IActionResult SelectFoodItemsForMeal(int mealId,int currentPage = 1)
        {

            var PageSize = 10;

            var meal = mealRepository.GetMealById(mealId);
            var viewModel = new MealBuilderSelectFoodItemsViewModel()
            {
                FoodItems = foodItemRepository.FoodItems.OrderBy(x => x.FoodItemName).Skip((currentPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = currentPage, ItemsPerPage = PageSize, TotalItems = foodItemRepository.FoodItems.Count() },
                TheMeal = meal
                
            };

            return View(viewModel);
        }

        public IActionResult AddToMeal(MealBuilderSelectFoodItemsViewModel model,int mealId,int id)
        {
            var meal = mealRepository.GetMealById(mealId);
            FoodItem foodItem = foodItemRepository.Find(id).GetAwaiter().GetResult();
            meal.AddFoodItem(foodItem);
            mealRepository.SaveChangesAsync();


            return RedirectToAction("SelectFoodItemsForMeal",new { mealId = meal.MealID} );
        }
    }
}
