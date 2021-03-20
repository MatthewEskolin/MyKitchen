using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyKitchen.Data
{
    public class FileUpload
    {
        [Key]
        public int FileUploadID {get; set;}

        //MealID
        public int FileEntityID  {get; set;}

        //"Meal"
        [StringLength(300)]
        public string EntityType {get; set;}

        [StringLength(300)]
        public string FileName {get; set;}

        [StringLength(300)]
        public string OriginalFileName {get; set;}

        public DateTime CreateDate {get; set;}


    }
}
