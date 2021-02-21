using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyKitchen.Data;
using MyKitchen.Models;
using System.Threading.Tasks;
using MyKitchen.Models.Meals;
using MyKitchen.BL;
using System;

namespace MyKitchen.Controllers
{
    [Authorize]
    public class MealBuilderController : Controller
    {

        private readonly IFoodItemRepository foodItemRepository;
        private readonly IMealRepository mealRepository;
        private ApplicationDbContext context;

        private UserInfo CurrentUser { get; set; }

        public MealBuilderController(IFoodItemRepository foodItemRepo, IMealRepository mealRepo, ApplicationDbContext ctx, UserInfo user)
        {
            CurrentUser = user;
            context = ctx;
            foodItemRepository = foodItemRepo;
            mealRepository = mealRepo;
        }

        public int PageSize = 15;

        // public IActionResult Index(int pageNum = 1)
        // {
        //     var viewModel1 = new MealBuilderIndexViewModel()
        //     {

        //          Meals = mealRepository.GetMealsForUser(this.CurrentUser.User).OrderBy(x => x.MealName).Skip((pageNum - 1) * PageSize).Take(PageSize),
        //          MealListPagingInfo = new PagingInfo() { CurrentPage = pageNum,ItemsPerPage = 15,TotalItems = mealRepository.CountForUser(CurrentUser.User)}
        //     };

        //     return View(viewModel1);
        // }


        public IActionResult Index(int pageNum = 1)
        {
            HttpContext.Session.SetInt32("editMealName", 0); 

            var result = mealRepository.GetMealsForUser(pageNum, PageSize, this.CurrentUser.User);
            var viewModel = new MealBuilderIndexViewModel()
            {
                Meals = result.meals,
                MealListPagingInfo = result.pagingInfo
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var mealFactory = new MyKitchen.Data.MealFactory(context);
            Meal meal = mealFactory.NewMeal();

            var viewModel = new MealBuilderCreateViewModel()
            {
                FoodItems = foodItemRepository.GetFoodItems().ToList()
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewMeal(MealBuilderCreateViewModel model)
        {
            var meal = model.Meal;
            meal.AppUser = CurrentUser.User;

            if (ModelState.IsValid)
            {
                await mealRepository.Add(meal);
            }

            return RedirectToAction("Index");
        }

        public IActionResult SelectFoodItemsForMeal(int mealId, int currentPage = 1)
        {
            //possible to prevent user from passing their own arguments.

            var PageSize = 10;
            var foodItems = foodItemRepository.GetFoodItemsForUser(this.CurrentUser.User);

            var meal = mealRepository.Find(mealId);
            var viewModel = new MealBuilderSelectFoodItemsViewModel()
            {
                FoodItems = foodItems.OrderBy(x => x.FoodItemName).Skip((currentPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = currentPage, ItemsPerPage = PageSize, TotalItems = foodItemRepository.GetFoodItemsForUser(CurrentUser.User).Count() },
                TheMeal = meal

            };

            return View(viewModel);
        }

        public IActionResult AddToMeal(int currentPage, int mealId, int id)
        {
            var meal = mealRepository.Find(mealId);
            FoodItem foodItem = foodItemRepository.Find(id).GetAwaiter().GetResult();

            if (meal.ContainsFoodItem(foodItem.FoodItemID))
            {
                ModelState.AddModelError(string.Empty, "This Food Item has already been added to this meal");
            }
            else
            {
                meal.AddFoodItemToMeal(foodItem.FoodItemID);
                mealRepository.SaveChanges();
                ViewBag.Message = $"{foodItem.FoodItemName} Added to Meal.";
            }

            var PageSize = 10;

            var viewModel = new MealBuilderSelectFoodItemsViewModel()
            {
                FoodItems = foodItemRepository.GetFoodItemsForUser(CurrentUser.User).OrderBy(x => x.FoodItemName).Skip((currentPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = currentPage, ItemsPerPage = PageSize, TotalItems = foodItemRepository.GetFoodItems().Count() },
                TheMeal = meal

            };

            return View("SelectFoodItemsForMeal", viewModel);
        }

        public IActionResult MealDetails(int mealID, [FromQuery]bool editMode)
        {
            var meal = mealRepository.Find(mealID);
            var viewModel = new MealBuilderMealDetails_VM();
            viewModel.Meal = meal;
            viewModel.EditMealMode = editMode;

            return View(viewModel);

        }

        public IActionResult DeleteMeal(int mealid)
        {
            var meal = mealRepository.Find(mealid);
            mealRepository.Remove(meal);
            mealRepository.SaveChanges();


            //possible to get previous page number here?
            var pageNum = 1;

            //TempData[""]
            var viewModel1 = new MealBuilderIndexViewModel()
            {

                Meals = mealRepository.GetMealsForUser(pageNum, PageSize, this.CurrentUser.User).meals,
                MealListPagingInfo = new PagingInfo() { CurrentPage = 1, ItemsPerPage = 15, TotalItems = mealRepository.Count() }
            };

            return View("Index", viewModel1);
        }

        public IActionResult UpdateMeal([FromForm] Meal meal)
        {
            //TODO how to update using repository method, don't know...

            var mealRec = mealRepository.Find(meal.MealID);
            mealRec.Recipe = meal.Recipe;
            mealRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditMealName([FromQuery] int mealID)
        {

            //TODO - Convert this to TempData when we understand how to turn it on.
            HttpContext.Session.SetInt32("editMealName", 1);

            return RedirectToAction("MealDetails", new { mealId = mealID , editMode = true});
        }
        
       public IActionResult SaveMealName([FromForm]MealBuilderMealDetails_VM pmeal){

            //Update the Meal Name in the DB, and stay on the Details Page.
            var meal = mealRepository.Find(pmeal.Meal.MealID);
            meal.MealName = pmeal.Meal.MealName;
            mealRepository.SaveChanges();

            var viewModel = new MealBuilderMealDetails_VM();

            viewModel.Meal = meal;
            viewModel.EditMealMode = false;

            return View("MealDetails",viewModel);

       } 

       public IActionResult Cancel_SaveMealName([FromForm]MealBuilderMealDetails_VM pmeal){

            var meal = mealRepository.Find(pmeal.Meal.MealID);


            var viewModel = new MealBuilderMealDetails_VM();
            viewModel.Meal = meal;

            return View("MealDetails",viewModel);

       }
        
        
        public IActionResult Edit()
        {
            throw new System.NotImplementedException();
        }


        //Delete A Food Item from the selected meal.
        public IActionResult DeleteFoodItemFromMeal(int mealFoodItemId)
        {
            mealRepository.add

            RedirectToAction("Details", new {mealId = });
        }


    }

}


