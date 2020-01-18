using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data;
using MyKitchen.Data.Calendar;

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



        [HttpPost]
        public JsonResult SaveNewEvent([FromBody] Events event1)
        {

            //Save new Event to DB
            ctx.Events.Add(event1);
            ctx.SaveChanges();

            return new JsonResult(true);


        }

        [HttpPost]
        public JsonResult UpdateEvent([FromBody]Events event1)
        {
            if(event1.EventID < 1) {  return new JsonResult(false);}

            var updateEvent = ctx.Events.FirstOrDefault(x => x.EventID == event1.EventID);
            updateEvent.Start = event1.Start;
            ctx.SaveChanges();

            return new JsonResult(true);
        }


        [HttpPost]
        public JsonResult ClearMonth([FromBody] int month)
        {
            var smonth = month + 1;

            List<Events> monthEvents = ctx.Events.Where(x => x.Start.Month == smonth).ToList();
            ctx.Events.RemoveRange(monthEvents);
            ctx.SaveChanges();

            return new JsonResult(true);
        }







    }
}