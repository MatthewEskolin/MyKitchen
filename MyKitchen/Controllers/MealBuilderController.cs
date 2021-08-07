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
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using MyKitchen.Services;
using Utilities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MyKitchen.Controllers
{
    [Authorize]
    public class MealBuilderController : Controller
    {
        private readonly IFoodItemRepository foodItemRepository;
        private readonly IMealRepository mealRepository;
        private ApplicationDbContext context;

        private UserInfo CurrentUser { get; set; }

        IWebHostEnvironment _env {get; set;}
        public IConfiguration _configuration { get; private set; }
        IMealImageService ImageService {get; set;}

        public MealBuilderController(IMealImageService imageService,
                                     IWebHostEnvironment env, 
                                     IFoodItemRepository foodItemRepo, 
                                     IMealRepository mealRepo, 
                                     ApplicationDbContext ctx, 
                                     IConfiguration configuration,
                                     UserInfo user)
        {
            CurrentUser = user;
            context = ctx;
            foodItemRepository = foodItemRepo;
            mealRepository = mealRepo;
            _env = env;
            _configuration = configuration;
            ImageService = imageService;

        }

        public int PageSize = 15;

        public IActionResult Index(int currentPage = 1)
        {
            HttpContext.Session.SetInt32("editMealName", 0); 

            //use default sort
            //MealName is the default sort

            Expression<Func<Meal, bool>> orderBy = x => x.IsFavorite;

            var result = mealRepository.GetMealsForUser(currentPage, PageSize, this.CurrentUser.User,string.Empty);
            
            var viewModel = new MealBuilderIndexViewModel()
            {
                Meals = result.meals,
                MealListPagingInfo = result.pagingInfo
            };

            return View(viewModel);
        }


        public IActionResult Index2(string newSort,int currentPage = 1,bool toggleSort = false, [FromForm]string currentSort = null)
        {
            HttpContext.Session.SetInt32("editMealName", 0); 

            string orderBy = this.GetMealsSort(newSort,currentSort,toggleSort);

            var result = mealRepository.GetMealsForUser2(currentPage, PageSize, this.CurrentUser.User,string.Empty,orderBy);
            
            var viewModel = new MealBuilderIndexViewModel()
            {
                Meals = result.meals,
                MealListPagingInfo = result.pagingInfo
            };

            //trim the _desc to get the lookup for sort order
            var lookup = Utilities.GridUtilities.Trim_desc(orderBy);
            viewModel.SortState[lookup] = orderBy;

            return View("Index",viewModel);
        }

        private string GetMealsSort(string newSort, string currentSort, bool toggle)
        {
            var rtn = string.Empty;
            
            if(!String.IsNullOrEmpty(currentSort))
            {
                //get sort state from model
                rtn = currentSort;
            }
            else
            {
                //MealName is the default sort
                rtn = "MealName";
            }

            //sort order is the "new" sort - only triggered if user clicks on a sorting link
            if(!string.IsNullOrEmpty(newSort)){

                rtn = toggle ? Utilities.GridUtilities.ToggleAscDesc(newSort) : newSort;
            }

            return rtn;
        }

        public IActionResult SearchMeals([FromForm]string searchText)
        {
            var result = mealRepository.GetMealsForUser(1,PageSize,this.CurrentUser.User,searchText);
            
            var viewModel = new MealBuilderIndexViewModel()
            {
                Meals = result.meals,
                MealListPagingInfo = result.pagingInfo
            };

            return View("Index",viewModel);
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

            // return RedirectToAction("Details");

            return Redirect($"/MealBuilder/MealDetails/{meal.MealID}?editMode=false");
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

        [Route("MealBuilder/MealDetails/{mealID}")]
        public IActionResult MealDetails(int mealID, [FromQuery]bool editMode)
        {
            var meal = mealRepository.Find(mealID);
            
            var images = ImageService.LoadImages(mealID);

            var viewModel = new MealBuilderMealDetails_VM();

            viewModel.Meal = meal;
            viewModel.EditMealMode = editMode;
            viewModel.MealImages = images;


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

                Meals = mealRepository.GetMealsForUser(pageNum, PageSize, this.CurrentUser.User,String.Empty).meals,
                MealListPagingInfo = new PagingInfo() { CurrentPage = 1, ItemsPerPage = 15, TotalItems = mealRepository.Count() }
            };

            return View("Index", viewModel1);
        }

        public IActionResult GetImage([FromQuery]string imageName)
        {
                BlobServiceClient blobServiceClient = new(_configuration.GetConnectionString("saMyKitchen"));
                BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("fileuploads");
                BlobClient blobClient = blobContainerClient.GetBlobClient(imageName);

                var memoryStream = new MemoryStream();
                blobClient.DownloadTo(memoryStream);

                return File(memoryStream.GetBuffer(),"image/jpg");


                // var image = System.IO.File.OpenRead("C:\\test\\random_image.jpeg");
                // return File(image, "image/jpeg");


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

        //Set a meal as a favorite
        [HttpPost]
        [Route("MealBuilder/SetFavorite/{mealId}/{isFav}")]
        public IActionResult SetFavorite(int mealId, int isFav){

            //update meal to the appropriate value
            var meal = mealRepository.Find(mealId);
            meal.IsFavorite = CUtilities.IntToBool(isFav);
            mealRepository.Update(meal);

            return new EmptyResult();
            
        }

        //Delete A Food Item from the selected meal.
        public IActionResult DeleteFoodItemFromMeal([FromForm]int MealID,[FromQuery]int mealFoodItemId)
        {
            // get Meal Entity and remove the food Item from the meal.
            var meal = mealRepository.Find(MealID);
            meal.RemoveFoodItemFromMeal(mealFoodItemId);
            mealRepository.SaveChanges();

            return RedirectToAction("MealDetails", new {mealId = MealID});
        }


        [HttpPost]
        public IActionResult UploadFile(IList<IFormFile> files, [FromForm]int MealID)
        {
            foreach (IFormFile source in files)
            {
                this.ImageService.SaveImage(source,MealID);
            }

            var images = this.ImageService.LoadImages(MealID);

            return PartialView("_ImageList",images);
        }



    }

}


