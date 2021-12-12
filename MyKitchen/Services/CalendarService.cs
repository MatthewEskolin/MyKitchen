using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MyKitchen.Data;
using MyKitchen.Models.BL;


namespace MyKitchen.Services
{
    public class CalendarService
    {
        private ApplicationDbContext DbContext { get; set; }
        private UserInfo UserInfo { get; set; }

        protected DateTime Today = DateTime.Now.Date;
        protected DateTime Tomorrow = DateTime.Now.Date.AddDays(1);


        public  CalendarService(ApplicationDbContext ctx, UserInfo usrinfo)
        {
            this.UserInfo = usrinfo;
            this.DbContext = ctx;
        }

        public (List<Meal> TodaysMeals,List<Meal> TomorrowsMeals) GetUpcomingMeals()
        {


            var upcomingMeals = DbContext.Events.Include(x => x.Meal).Where(x => (x.Start.Date == Today || x.Start.Date == Tomorrow ) && x.MealID != null).ToList();

            var todaysMeals = upcomingMeals.Where(x => x.Start.Date == Today).Select(x => x.Meal).ToList();
            var tommorowsMeals = upcomingMeals.Where(x => x.Start.Date == Tomorrow).Select(x => x.Meal).ToList();

            return (todaysMeals, tommorowsMeals);

        }

        public (List<FoodItem> TodaysItems, List<FoodItem> TomorrowsItems) GetUpcomingItems()
        {
            var upcomingItems = DbContext.Events.Include(x => x.FoodItem).Where(x => (x.Start.Date == Today || x.Start.Date == Tomorrow) && x.MealID == null).ToList();

            var todaysItems = upcomingItems.Where(x => x.Start.Date == Today).Select(x => x.FoodItem).ToList();
            var tommorowsItems = upcomingItems.Where(x => x.Start.Date == Tomorrow).Select(x => x.FoodItem).ToList();

            return (todaysItems, tommorowsItems);

        }

}
}
