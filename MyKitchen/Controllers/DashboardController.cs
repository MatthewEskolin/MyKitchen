using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyKitchen.Controllers
{
    [Authorize]
    public class DashboardController:Controller
    {
        public IActionResult Index()
        {
            //calculate the right metrics

            //the goal of this app is to eat healthy and delicious foods for every meal, and to minimize time spent planning and thinking about food.

            //create this as a customizable dashboard?

            //! Alert - you have 0 meals planned for the next 5 days

            //check to see if meals are planned for the next 5 days, and display an error message if nothign is planned...
            //else pull the data from "events" and show it in a dashboard...
            return View();



        }


        
    }
}
