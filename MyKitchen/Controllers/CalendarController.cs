using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.BL;
using MyKitchen.Data;
using MyKitchen.Data.Calendar;

namespace MyKitchen.Controllers
{
    //Food items and meals to be added to the calendar. Each user has their own calendar. 
    public partial class CalendarController : Controller
    {
        public UserInfo CurrentUser { get; }
        private ApplicationDbContext ctx { get; set; }
        public int PageSize = 10;

        public CalendarController(ApplicationDbContext context,UserInfo user)
        {
            CurrentUser = user;
            ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchForItems([FromForm]string searchText)
        {
            var items = ctx.vwsUserMealsAndFoodItems.Where(x => x.AppUserId == CurrentUser.User.Id && x.ItemName.Contains(searchText)).ToList();;
            return new JsonResult(items);
        }


        //Gets all events in the database - not filtered by user - this shouldn't be used to view a users events.
        public JsonResult GetEvents()
        {
            var events = ctx.Events.ToList();
            return new JsonResult(events);
        }

        public JsonResult GetEventsFeed()
        {
            var events = ctx.Events.ToList().Select(FullCalendarEvent.FromEvent);
            return new JsonResult(events);
        }

        //Gets Available Items for the current User.
        public JsonResult GetAvailableItems()
        {
            var items = ctx.vwsUserMealsAndFoodItems.Where(x => x.AppUserId == CurrentUser.User.Id).ToList();;
            return new JsonResult(items);
        }

        [HttpPost]
        public JsonResult SaveNewEvent([FromBody] Events event1)
        {

            //Save new Event to DB for current user
            event1.AppUser = CurrentUser.User;
            ctx.Events.Add(event1);
            ctx.SaveChanges();

            return new JsonResult(event1.EventID);
        }

        [HttpPost]
        public JsonResult UpdateEvent([FromBody]Events event1)
        {
            if(event1.EventID < 1) {  return new JsonResult(false);}

            //use current user - add user id to event table
            var updateEvent = ctx.Events.FirstOrDefault(x => x.EventID == event1.EventID);

            updateEvent.Start = event1.Start;
            ctx.SaveChanges();

            return new JsonResult(true);
        }

        [HttpPost]
        public JsonResult ClearMonth([FromBody]  CalendarCommand cmd)
        {
            //TODO this should also filter events by user, or it will be deleting events for all users.
            var smonth = cmd.Month + 1;

            List<Events> monthEvents = ctx.Events.Where(x => x.Start.Month == smonth).ToList();
            ctx.Events.RemoveRange(monthEvents);
            ctx.SaveChanges();

            return new JsonResult(true);
        }

        [HttpPost]
        public JsonResult RemoveEvent([FromBody]Events deleteEvent)
        {
            //Deletes a single event from the calendar
            var removeEvent = ctx.Events.Where(x => x.EventID == deleteEvent.EventID).FirstOrDefault();
            ctx.Events.Remove(removeEvent);
            ctx.SaveChanges();

            return new JsonResult(true);
        }
    }
}