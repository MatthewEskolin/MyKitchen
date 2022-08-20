using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyKitchen.Pages
{
    public class MealImageDisplay
    {
        public string ImagePath { get; set; }
        public bool IsSelected { get; set; }
    }

    public class SettingsModel : PageModel
    {
        public List<string> Images { get; set; } = new();

        public SettingsModel()
        {
            //Load List of Icons
            var image1 = @"healing food.png";
            var image2 = @"plate-with-orange-pattern.svg";

            Images.Add(image1);
            Images.Add(image2);

        }

        public void OnGet()
        {
            //Show as radio button list


        }
    }
}
