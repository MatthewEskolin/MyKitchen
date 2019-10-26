using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
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
