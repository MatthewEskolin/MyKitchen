using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exceptionless.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyKitchen.Controllers;
using MyKitchen.Services;

namespace MyKitchen.Pages
{

    //public class MealImageDisplay//:MJESelectList
    //{
    //    public string ImagePath { get; set; }
    //    public bool IsSelected { get; set; }
    //}


    public class SettingsModel : BasePageModel
    {
        private ILogger<SettingsModel> Logger { get; set; }

        [BindProperty]
        public string  Image { get; set; }
        public List<string> Images { get; set; } = new();

        public string SystemMessage { get; set; }

        public SettingsModel(ILogger<SettingsModel> logger, MyKitchen.Models.BL.UserInfo user)  
        {
            Logger = logger;
            this.UserInfo = user;

        }

        public void OnGet()
        {
            //Initialize Settings Page
            LoadFilesFromDb();

        }

        public MyKitchen.Models.BL.UserInfo UserInfo { get; set; }

        public async Task OnPostSaveSettings([FromServices] IMyKitchenDataService ctx)
        {
            Logger.LogInformation($"{UserInfo.User.MealImage}");
            Logger.LogInformation($"{UserInfo.User.Id}");


            await ctx.UpdateSettingsAsync(Image);

            SystemMessage = "Settings Saved";
            LoadFilesFromDb();

            
            Logger.LogInformation($"{UserInfo.User.MealImage}");
            Logger.LogInformation($"{UserInfo.User.Id}");

            //will user be loaded from db on each request?

        }

        public void LoadFilesFromDb()
        {
            var image1 = @"healing food.png";
            var image2 = @"plate-with-orange-pattern.svg";

            Images.Add(image1);
            Images.Add(image2);

            Image = String.IsNullOrEmpty(UserInfo.User.MealImage) ? Images[0] : UserInfo.User.MealImage;

        }
    }
}
