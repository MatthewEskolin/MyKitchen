using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data.Calendar;

namespace MyKitchen.Controllers
{

    [Authorize]
    public partial class CalendarController : Controller
    {
        private UserInfo CurrentUser { get; }
        private ApplicationDbContext ctx { get; set; }

        public CalendarController(ApplicationDbContext context,UserInfo user)
        {
            CurrentUser = user;
            ctx = context;
        }

        public IActionResult Index()
        {
            return View(new SearchArgs());
        }

        public IActionResult SearchForItems([FromForm]SearchArgs searchArgs)
        {
            #region commented
            //if (!String.IsNullOrEmpty(Request.Form["action"]))
            //{
            //    var action = Request.Form["action"][0];

            //    switch (action)
            //    {
            //        case "next":
            //            //calculate new searchargs
            //            //searchArgs.PageIndex++;
            //            break;

            //        case "previous":
            //            //calculate new searchargs
            //            //searchArgs.PageIndex--;
            //            break;

            //        default: throw new Exception("Invalid Form Submit");
            //    }
            //};
            #endregion

            var items = ctx.vwsUserMealsAndFoodItems.AsQueryable();

            items = items.Where(x => x.AppUserId == CurrentUser.User.Id);

            if(!String.IsNullOrEmpty(searchArgs.SearchText))
            {
                items = items.Where(x => x.ItemName.Contains(searchArgs.SearchText));
            }

            if(searchArgs.CbShowMealsOnly == "on")
            {
                items = items.Where(x => x.ItemType == "MEAL");
            }

            if (searchArgs.CbShowQueuedOnly== "on")
            {
                //items = items.Where(x => x.)
                throw new NotImplementedException(
                    "Where does the on  and off come from? can we consolidate these parameters into a single searchargs class?");

            }

            //paging 
            var skipItems = (searchArgs.PageIndex - 1) * searchArgs.PageSize;
            items = items
                .Skip(skipItems)
                .Take(searchArgs.PageSize);


            var rtn = items.ToList();

            return new JsonResult(rtn);
        }


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
            var searchArgs = new SearchArgs();

            var items = ctx.vwsUserMealsAndFoodItems.Where(x => x.AppUserId == CurrentUser.User.Id);

            //paging
            var skipItems = (searchArgs.PageIndex - 1) * searchArgs.PageSize;
            items = items
                .Skip(skipItems)
                .Take(searchArgs.PageSize);


            var rtn = items.ToList();

            return new JsonResult(rtn);
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
    public class SearchModel
    {
        public string SearchText { get; set; }
        public string CbShowMealsOnly { get; set; }
        public string CbShowQueuedOnly { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int TotalPages { get; set; } = 10;
        public bool ShowNextButton()
        {
            return PageIndex < TotalPages;
        }
        public bool ShowPreviousButton()
        {
            return PageIndex > 1;
        }


        public int ModelTest { get; set; }

    }

}