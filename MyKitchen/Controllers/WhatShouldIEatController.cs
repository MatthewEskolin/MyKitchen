using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyKitchen.Controllers
{
    public class WhatShouldIEatController : Controller
    {
        public IActionResult DisplayCurrentPrediction()
        {
            return View();
        }
    }
}