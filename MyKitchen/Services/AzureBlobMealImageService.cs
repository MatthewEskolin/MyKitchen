using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace MyKitchen.Services
{
    public class AzureBlobMealImageService:IMealImageService{


        IWebHostEnvironment _env;
        ApplicationDbContext _ctx;
        IConfiguration _configuration;

        public AzureBlobMealImageService(ApplicationDbContext ctx, IWebHostEnvironment env,IConfiguration configuration)
        {
            _env = env;
            _ctx = ctx;
            _configuration = configuration;
        }

        public void SaveImage(IFormFile source,int mealID)
        {

            //Save Record of Image to SQL Database. We will store the actual image in Azure Blob Storage.

                 string uploaded_filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                //Build New FileName - use guid for uniqueness in case there are multiple images per meal.
                var guid = Guid.NewGuid().ToString();;
                var fileName = $"{_env.WebRootPath}\\uploads\\{mealID}_{guid}_{Path.GetExtension(uploaded_filename)}";
                var fileNameShort =  Path.GetFileName(fileName);

                var newFileUpload = new FileUpload(){
                    OriginalFileName = uploaded_filename,
                    FileName = fileNameShort,
                    FileEntityID = mealID,
                    EntityType = "Meal"
                };

                _ctx.FileUploads.Add(newFileUpload);
                _ctx.SaveChanges();


                //Save the File to Azure Blob Storage
                // using (FileStream output = System.IO.File.Create(fileName)){
                //     source.CopyTo(output);
                // }

                BlobServiceClient blobServiceClient = new(_configuration.GetConnectionString("saMyKitchen"));
                BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("fileuploads");
                
                BlobClient blobClient = blobContainerClient.GetBlobClient(fileNameShort);

                //TODO Log Uploading of the new blob
                var stream = source.OpenReadStream();

                //Async - use the upload async method here.
                blobClient.Upload(source.OpenReadStream());
                stream.Close();



        }
        public List<string> LoadImages(int mealId)
        {
            var fileUploads = _ctx.FileUploads.Where(x => x.EntityType == "Meal" && x.FileEntityID == mealId).ToList();

            var fileNames = fileUploads.Select(x => x.FileName).ToList();

            var imageList = fileNames.Select(x =>$"/MealBuilder/GetImage?imageName={x}").ToList();
            return imageList;

        }





    }

}