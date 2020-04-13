using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyKitchen.Data
{
    public class FoodGroup
    {
        [Key]
        public int FoodGroupID { get; set; }
        public string Name { get; set; }

        public ICollection<FoodItem> FoodItem { get; set; }


    }
}
