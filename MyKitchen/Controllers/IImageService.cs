using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyKitchen.Data;
using MyKitchen.Models;

namespace MyKitchen.Controllers
{


    public interface IMealImageService {

        public void SaveImage(IFormFile file,int mealId);
        public List<string> LoadImages(int mealId);
    }

}