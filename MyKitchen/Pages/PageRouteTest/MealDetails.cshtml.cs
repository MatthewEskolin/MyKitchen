using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyKitchen.Controllers;
using MyKitchen.Data;
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

        private IMealImageService ImageService { get; set; }

        private IMealRepository MealRepo { get; set; }

        //Properties Bound from Forms
        [BindProperty]
        public string MealName { get; set; }
        [BindProperty]
        public int MealId { get; set; }



        //Flags
        public bool EditMealMode { get; set; }
        public bool MealSavedMsg { get; set; }

        public Meal Meal { get; set; }
        public List<string> Images { get; set; }

        //OnGet is Required Here by Convention
        public void OnGetShowDetails(int mealId)
        {
            LoadPageProperties(mealId);
        }

        protected void LoadPageProperties(int mealId)
        {
            this.Meal = MealRepo.Find(mealId);
            Images = ImageService.LoadImages(mealId);
        }

        public void OnGetEditMealName(int mealId)
        {
            EditMealMode = true;
            LoadPageProperties(mealId);
            this.MealName = Meal.MealName;

        }

        public void OnPostSaveMealName()
        {
            LoadPageProperties(this.MealId);

            this.Meal.MealName = this.MealName;
            MealRepo.SaveChanges();

            EditMealMode = false;
        }

        public void OnPostUpdateMeal([FromForm] Meal meal)
        {
            LoadPageProperties(meal.MealID);
            this.Meal.Recipe = meal.Recipe;
            MealRepo.SaveChanges();

            //TODO Show Meal Saved Banner
            //show meal saved = true;
            MealSavedMsg = true;

        }


    }
}
