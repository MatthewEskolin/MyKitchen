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


        public  CalendarService(ApplicationDbContext ctx, UserInfo usrinfo)
        {
            this.UserInfo = usrinfo;
            this.DbContext = ctx;
        }

        public (List<Meal> TodaysMeals,List<Meal> TomorrowsMeals) GetUpcomingMeals()
        {
            var today = DateTime.Now.Date;
            var tomrrow = DateTime.Now.Date.AddDays(1);


            var upcomingMeals = DbContext.Events.Include(x => x.Meal).Where(x => x.Start.Date == today || x.Start.Date == tomrrow).ToList();

            var todaysMeals = upcomingMeals.Where(x => x.Start == today).Select(x => x.Meal).ToList();
            var tommorowsMeals = upcomingMeals.Where(x => x.Start == tomrrow).Select(x => x.Meal).ToList();

            return (todaysMeals, tommorowsMeals);

        }

}
}
