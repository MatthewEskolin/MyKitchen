using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyKitchen.Data
{
    //not a single ingredient but a component of a meal
    public class FoodItem
    {

        [Key]
        public int FoodItemID {get; set;}
        public string FoodItemName { get; set; }

        public string ResturantName { get; set; }


        public string FoodDescription { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cost { get; set; }




    }
}
