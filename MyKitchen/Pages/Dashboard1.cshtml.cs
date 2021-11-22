using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyKitchen.Controllers;
using MyKitchen.Data;

namespace MyApp.Namespace
{
    public class DashboardModel : PageModel
    {

        public DashboardModel(UserInfo usr, IMealRepository mealRepository, ApplicationDbContext ctx)
        {
            //what claims principal is already here?
            var user = this.User.ToString();
            //this.CurrentUser = usr;
            this.MealRepo = mealRepository;
            this.DbContext = ctx;

          //Need User

        }

        public ApplicationDbContext DbContext { get; set; }

        protected IMealRepository MealRepo { get; set; }

        protected UserInfo CujrrentUser { get; set; }

        public List<Meal> TodaysMeals { get; set; }

        public List<Meal> TomorrowsMeals { get; set; }

        public void OnGet()
        {
            //Load Today's Meals and Tomorrows Meals

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
