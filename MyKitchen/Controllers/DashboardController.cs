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

            return View();



        }


        
    }
}
