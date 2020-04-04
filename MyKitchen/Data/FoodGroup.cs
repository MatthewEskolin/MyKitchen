using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    public class UserFoodItem
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        public int UserFoodItemID {get; set;}

        public ApplicationUser AppUser {get; set;}

        [ForeignKey("FoodItems")]
        public int FoodItemID { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual FoodItem FoodItems { get; set; }

    }
}
