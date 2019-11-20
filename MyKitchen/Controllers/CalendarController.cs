using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data;
using Newtonsoft.Json;

namespace MyKitchen.Controllers
{
    public class CalendarController : Controller
    {
        //allow food items or meals to be added to the calendar



        private ApplicationDbContext ctx { get; set; }


        public int PageSize = 10;

        public CalendarController(ApplicationDbContext context)
        {
            ctx = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            var events = ctx.Events.ToList();
            return new JsonResult(events);
        }

        public JsonResult GetAvailableItems()
        {
            var items = ctx.vwsMealsAndFoodItems.ToList();
            return new JsonResult(items);
        }


        //public JsonResult Get




    }
}