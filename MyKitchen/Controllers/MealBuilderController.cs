using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Azure.Storage.Blobs;
using MyKitchen.Data;
using MyKitchen.Models;
using MyKitchen.Models.BL;
using MyKitchen.Models.Meals;
using MyKitchen.Services;
using NetGeneralLibrary.Utilities;

namespace MyKitchen.Controllers
{
    //Bind Classes


    [Authorize]
    public class MealBuilderController : Controller
    {
        private readonly IFoodItemRepository _foodItemRepository;

        private readonly IMealRepository _mealRepository;

        private readonly IMealImageService _imageService;

        private readonly IUserInfo _user;

        private readonly IHttpContextAccessor _contextAccessor;

        private string DefaultSortProperty {get; set;} = "MealName";

        private readonly IConfiguration _configuration;



        public MealBuilderController(IMealImageService imageService,
                                     IFoodItemRepository foodItemRepo, 
                                     IMealRepository mealRepo, 
                                     IConfiguration configuration,
                                     IUserInfo user,
                                     IHttpContextAccessor httpContextAccessor)
        {
            //CurrentUser = user;
            _foodItemRepository = foodItemRepo;
            _mealRepository = mealRepo;
            _configuration = configuration;
            _imageService = imageService;
            _contextAccessor = httpContextAccessor;

            _user = user;

        }

        public int PageSize = 15;

        public MealSearchArgs SearchArgs { get; set; }

        //Model Binding Classes



        [HttpGet]
        public IActionResult Index(MealBuilderIndexViewModel model)
        {
            //Reset Session State
            _contextAccessor.HttpContext?.Session.SetInt32("editMealName", 0); 

            string orderBy = this.GetMealsSort(model.NewSort,model.CurrentSort,model.ToggleSort);

            var result = _mealRepository.SearchMeals(model.CurrentPage, PageSize, orderBy, SearchArgs);
            
            var viewModel = new MealBuilderIndexViewModel()
            {

                Meals = result.meals,
                MealListPagingInfo = result.pagingInfo,
                
                
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

        public IActionResult SearchMeals(MealBuilderIndexViewModel model )
        {
            var searchArgs = model.DashboardSearchArgs;

            var result = _mealRepository.SearchMeals(1,PageSize,DefaultSortProperty, searchArgs);

            model.Meals = result.meals;
            model.MealListPagingInfo = result.pagingInfo;

            //var viewModel = new MealBuilderIndexViewModel()
            //{
            //    Meals = result.meals,
            //    MealListPagingInfo = result.pagingInfo
            //};

            return View("Index",model);
        }


        public IActionResult Create()
        {
            var mealFactory = new MyKitchen.Data.MealFactory();
            Meal meal = mealFactory.NewMeal();

            var viewModel = new MealBuilderCreateViewModel()
            {
                FoodItems = _foodItemRepository.GetFoodItems().ToList()
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewMeal(MealBuilderCreateViewModel model)
        {
            var meal = model.Meal;

            //get applicationuser from IUser
            //TODO get rid of this..
            var appUser = _user as ApplicationUser;

            meal.AppUser = appUser;

            if (ModelState.IsValid)
            {
                await _mealRepository.Add(meal);
            }

            // return RedirectToAction("Details");

            return Redirect($"/MealBuilder/MealDetails/{meal.MealID}?editMode=false");
        }

        public IActionResult SelectFoodItemsForMeal(int mealId, int currentPage = 1)
        {
            //possible to prevent user from passing their own arguments.

            var PageSize = 10;
            var foodItems = _foodItemRepository.GetFoodItemsForUser(this._user);

            var meal = _mealRepository.Find(mealId);
            var viewModel = new MealBuilderSelectFoodItemsViewModel()
            {
                FoodItems = foodItems.OrderBy(x => x.FoodItemName).Skip((currentPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = currentPage, ItemsPerPage = PageSize, TotalItems = _foodItemRepository.GetFoodItemsForUser(this._user).Count() },
                TheMeal = meal

            };

            return View(viewModel);
        }

        public IActionResult AddToMeal(int currentPage, int mealId, int id)
        {
            var meal = _mealRepository.Find(mealId);
            FoodItem foodItem = _foodItemRepository.Find(id).GetAwaiter().GetResult();

            if (meal.ContainsFoodItem(foodItem.FoodItemID))
            {
                ModelState.AddModelError(string.Empty, "This Food Item has already been added to this meal");
            }
            else
            {
                meal.AddFoodItemToMeal(foodItem.FoodItemID);
                _mealRepository.SaveChanges();
                ViewBag.Message = $"{foodItem.FoodItemName} Added to Meal.";
            }

            var PageSize = 10;

            var viewModel = new MealBuilderSelectFoodItemsViewModel()
            {
                FoodItems = _foodItemRepository.GetFoodItemsForUser(this._user).OrderBy(x => x.FoodItemName).Skip((currentPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = currentPage, ItemsPerPage = PageSize, TotalItems = _foodItemRepository.GetFoodItems().Count() },
                TheMeal = meal

            };

            return View("SelectFoodItemsForMeal", viewModel);
        }

        [Route("MealBuilder/MealDetails/{mealID}")]
        public IActionResult MealDetails(int mealID, [FromQuery]bool editMode)
        {
            var meal = _mealRepository.Find(mealID);
            
            var images = _imageService.LoadImages(mealID);

            var viewModel = new MealBuilderMealDetails_VM();

            viewModel.Meal = meal;
            viewModel.EditMealMode = editMode;
            viewModel.MealImages = images;


            return View(viewModel);

        }

        public async Task<IActionResult> AddToQueue(int mealID)
        {
            var meal = _mealRepository.Find(mealID);

            //security check, users can only  modify their own meals
            if (meal.AppUser.Id != _user.Id)
            {
                //set system error message
                return RedirectToAction("Index");
            }

            meal.IsQueued = true;
            _mealRepository.Update(meal);
            await _mealRepository.SaveChangesAsync();

            return RedirectToAction("Index");

        }



        public async Task<IActionResult> RemoveFromQueue(int mealID)
        {
            var meal = _mealRepository.Find(mealID);

            //security check, users can only  modify their own meals
            if (meal.AppUser.Id != _user.Id)
            {
                //set system error message
                return RedirectToAction("Index");
            }

            meal.IsQueued = false;
            _mealRepository.Update(meal);
            await _mealRepository.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        public IActionResult DeleteMeal(int mealid)
        {
            var meal = _mealRepository.Find(mealid);
            _mealRepository.Remove(meal);
            _mealRepository.SaveChanges();

            //possible to get previous page number here?
            var pageNum = 1;

            //TempData[""]
            var viewModel1 = new MealBuilderIndexViewModel()
            {

                Meals = _mealRepository.SearchMeals(pageNum, PageSize, DefaultSortProperty, this.SearchArgs).meals,
                MealListPagingInfo = new PagingInfo() { CurrentPage = 1, ItemsPerPage = 15, TotalItems = _mealRepository.Count() }
            };

            return View("Index", viewModel1);
        }

        public async Task<IActionResult> GetImage([FromQuery]string imageName)
        {

                BlobServiceClient blobServiceClient = new(_configuration.GetConnectionString("saMyKitchen"));
                BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("fileuploads");
                BlobClient blobClient = blobContainerClient.GetBlobClient(imageName);

                if(await blobClient.ExistsAsync())
                {
                    var memoryStream = new MemoryStream();
                    blobClient.DownloadTo(memoryStream);
                    return File(memoryStream.GetBuffer(),"image/jpg");

                }

                return NotFound($"Image {imageName} not found");

        }

        public IActionResult UpdateMeal([FromForm] Meal meal)
        {
            //TODO how to update using repository method, don't know...

            var mealRec = _mealRepository.Find(meal.MealID);
            mealRec.Recipe = meal.Recipe;
            _mealRepository.SaveChanges();
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
            var meal = _mealRepository.Find(pmeal.Meal.MealID);
            meal.MealName = pmeal.Meal.MealName;
            _mealRepository.SaveChanges();

            var viewModel = new MealBuilderMealDetails_VM();

            viewModel.Meal = meal;
            viewModel.EditMealMode = false;

            return View("MealDetails",viewModel);

       } 

       public IActionResult Cancel_SaveMealName([FromForm]MealBuilderMealDetails_VM pmeal){

            var meal = _mealRepository.Find(pmeal.Meal.MealID);


            var viewModel = new MealBuilderMealDetails_VM
            {
                Meal = meal
            };

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
            var meal = _mealRepository.Find(mealId);
            meal.IsFavorite = CUtilities.IntToBool(isFav);
            _mealRepository.Update(meal);

            return new EmptyResult();
            
        }

        //Delete A Food Item from the selected meal.
        public IActionResult DeleteFoodItemFromMeal([FromForm]int MealID,[FromQuery]int mealFoodItemId)
        {
            // get Meal Entity and remove the food Item from the meal.
            var meal = _mealRepository.Find(MealID);
            meal.RemoveFoodItemFromMeal(mealFoodItemId);
            _mealRepository.SaveChanges();

            return RedirectToAction("MealDetails", new {mealId = MealID});
        }


        [HttpPost]
        public IActionResult UploadFile(IList<IFormFile> files, [FromForm]int MealID)
        {
            foreach (IFormFile source in files)
            {
                this._imageService.SaveImage(source,MealID);
            }

            var images = this._imageService.LoadImages(MealID);

            return PartialView("_ImageList",images);
        }



    }

    public class MealSearchArgs
    {
        public string MealName { get; set; }
        public bool ShowQueuedOnly { get; set; }
    }

}


