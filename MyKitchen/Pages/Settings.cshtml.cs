using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyKitchen.Pages
{

    public class MealImageDisplay//:MJESelectList
    {
        public string ImagePath { get; set; }
        public bool IsSelected { get; set; }
    }

    public class SettingsModel : PageModel
    {

        [BindProperty]
        public string  Image { get; set; }
        public List<MealImageDisplay> Images { get; set; } = new();

        public string SystemMessage { get; set; }

        public SettingsModel()
        {

        }

        public void OnGet()
        {
            //Initialize Settings Page
            LoadFilesFromDb();

            Image = Images.FirstOrDefault(x => x.IsSelected)?.ImagePath;


        }

        public void OnPostSaveSettings()
        {

            SystemMessage = "Settings Saved";
            LoadFilesFromDb();
        }

        public void LoadFilesFromDb()
        {
            var image1 = @"healing food.png";
            var image2 = @"plate-with-orange-pattern.svg";

            Images.Add(new MealImageDisplay(){ImagePath = image1});
            Images.Add(new MealImageDisplay(){ImagePath = image2});

            Images[0].IsSelected = true;

        }
    }
}
