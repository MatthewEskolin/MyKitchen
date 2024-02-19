using System;
using MyKitchen.Data.Calendar;

namespace MyKitchen.Controllers
{
    public partial class CalendarController
    {
        public class FullCalendarEvent
        {
            //mirroring fullcalendar object properties

            public string id { get; set; }
            public string  title { get; set; }
            public string description { get; set; }
            public DateTime start { get; set; }
            public DateTime? end { get; set; }
            public string color { get; set; }
            public bool allDay { get; set; }
            public int eventID { get; set; }

            public int? mealID {get; set;}
            public int? foodItemID {get; set;}

            public string itemType {get; set;}

            public int itemId {get; set;}

            public static FullCalendarEvent FromEvent(Events evt)
            {
                var fullCalendarEvent = new FullCalendarEvent
                {
                    id = evt.EventID.ToString(), 
                    title = evt.Subject,
                    description = evt.Description,
                    start = evt.Start,
                    end = evt?.End,
                    color = evt.ThemeColor,
                    eventID = evt.EventID,
                    allDay = evt.IsFullDay,
                    mealID = evt.MealID,
                    foodItemID = evt.FoodItemID,
                    itemType = evt.itemType,
                    itemId = evt.itemId
                };

                return fullCalendarEvent;
            }

        }




    }
}