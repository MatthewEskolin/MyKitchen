using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Models.BL;
using MyKitchen.Services;

namespace MyKitchen.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private UserInfo CurrentUser { get; set; }
        private IMealRepository MealRepo { get; set; }
        private CalendarService CalendarService { get; set; }
        private ApplicationDbContext DbContext { get; set; }
        public List<Meal> TodaysMeals { get; set; }
        public List<Meal> TomorrowsMeals { get; set; }
        public List<Meal> FavoriteMeals { get; set; }

        //Meal Items
        public List<FoodItem> TodaysItems { get; set; }
        public List<FoodItem> TomorrowsItems { get; set; }

        public bool TodayHasItems => TodaysItems.Count > 0 || TodaysMeals.Count > 0;
        public bool TomorrowHasItems => TomorrowsItems.Count > 0 || TomorrowsMeals.Count > 0;


        public DashboardModel(UserInfo usr,ApplicationDbContext ctx, CalendarService calendarService,IMealRepository mealRepo)
        {
            this.CalendarService = calendarService;
            this.DbContext = ctx;
            this.CurrentUser = usr;
            this.MealRepo = mealRepo;
        }

        public void OnGet()
        {
            //Get Meals
            var upcomingMeals = CalendarService.GetUpcomingMeals();

            //Get Food Items
            var upcomingItems = CalendarService.GetUpcomingItems();

            //Get Favorite Meals
            FavoriteMeals = MealRepo.GetFavoriteMeals().ToList();

            TodaysMeals = upcomingMeals.TodaysMeals;
            TomorrowsMeals = upcomingMeals.TomorrowsMeals;

            TodaysItems = upcomingItems.TodaysItems;
            TomorrowsItems = upcomingItems.TomorrowsItems;


            //How do we transform events into meals
            //technically, we c




            //Load Today's Mejals and Tomorrows Meals

            //get events -> then get meals
            //this.DbContext.Events




            //calculate the right metrics

            //the goal of this app is to eat healthy and delicious foods for every meal, and to minimize time spent planning and thinking about food.

            //create this as a customizable dashboard?

            //! Alert - you have 0 meals planned for the next 5 days

            //check to see if meals are planned for the next 5 days, and display an error message if nothign is planned...
            //else pull the data from "events" and show it in a dashboard...
        }
    }
}
