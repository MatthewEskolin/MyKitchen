using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyKitchen.Controllers;
using MyKitchen.Models;
using MyKitchen.Services;

namespace MyKitchen.Pages.PageRouteTest
{
    public class MealDetailsModel : PageModel
    {
        public MealDetailsModel(IMealRepository mealRo,IMealImageService imageService )
        {
            this.MealRepo = mealRo;
            this.ImageService = imageService;

        }

        public IMealImageService ImageService { get; set; }

        public IMealRepository MealRepo { get; set; }

        public string TestProperty { get; set; }

        //OnGet is Required Here to 
        public void OnGetShowDetails(int mealId)
        {

            throw new NotImplementedException();
            //load meal model using what we have in Meal Details

            //var meal = MealRepo.Find(mealId);
            
            //var images = ImageService.LoadImages(mealID);

            //var viewModel = new MealBuilderMealDetails_VM();

            //viewModel.Meal = meal;
            //viewModel.EditMealMode = editMode;
            //viewModel.MealImages = images;


            //return View(viewModel);
            //TestProperty = mealId.ToString();
        }

        public void OnPostShowDetails()
        {
            int t = 2134;
            t++;

        }
    }
}
