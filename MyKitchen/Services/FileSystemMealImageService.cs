using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyKitchen.Data;

namespace MyKitchen.Services
{
    public class FileSystemMealImageService:IMealImageService
    {

        IWebHostEnvironment _env;
        ApplicationDbContext _ctx;

        public FileSystemMealImageService(ApplicationDbContext ctx, IWebHostEnvironment env)
        {
            _env = env;
            _ctx = ctx;
        }

        public void SaveImage(IFormFile source,int mealID)
        {
                 string uploaded_filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                //Build New FileName

                var randomPart_forUniqueName = Guid.NewGuid().ToString();;

                var fileName = $"{_env.WebRootPath}\\uploads\\{mealID}_{randomPart_forUniqueName}_{Path.GetExtension(uploaded_filename)}";

                using (FileStream output = System.IO.File.Create(fileName)){
                    source.CopyTo(output);
                }

                var newFileUpload = new FileUpload();

                var pathRemoved =  Path.GetFileName(fileName);

                //TODO maybe use a new name in case of duplicate file name uploads
                newFileUpload.OriginalFileName = uploaded_filename;
                newFileUpload.FileName = pathRemoved;
                newFileUpload.FileEntityID = mealID;
                newFileUpload.EntityType = "Meal";
                _ctx.FileUploads.Add(newFileUpload);
                _ctx.SaveChanges();
        }
        public List<string> LoadImages(int mealId)
        {
            var fileUploads = _ctx.FileUploads.Where(x => x.EntityType == "Meal" && x.FileEntityID == mealId).ToList();

            var fileNames = fileUploads.Select(x => x.FileName).ToList();

            var imageList = fileNames.Select(x => $"/Uploads/{x}").ToList();



            // var imageList =  new List<string>();
            // imageList.Add (this._env.ContentRootPath + "\\uploads\\" + mealId + ".jpg");

            // var x = Directory.GetCurrentDirectory();

            return imageList;

        }

    }

}