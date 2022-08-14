using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MyKitchen.Services
{


    public interface IMealImageService {

        public void SaveImage(IFormFile file,int mealId);
        public List<string> LoadImages(int mealId);
    }

}