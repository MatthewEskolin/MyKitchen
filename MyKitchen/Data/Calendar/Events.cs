using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKitchen.Data.Calendar
{
    public class Events
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
      
        [Required]
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string ThemeColor { get; set; }

        [Required]
        public bool IsFullDay { get; set; }

        public ApplicationUser AppUser {get; set;}

        [ForeignKey("FoodItems")]
        public int? FoodItemID { get; set; }

        [ForeignKey("Meals")]
        public int? MealID {get; set;}

        public DateTime CreateDate { get; set; }

        public virtual FoodItem FoodItems { get; set; }

        public virtual Meal Meal {get; set;}


        [NotMapped]
        public string itemType
        {
            get
            {

                if (this.MealID == null)
                {
                    return "FOOD ITEM";
                }
                else
                {
                    return "MEAL";
                }
            }
        }


        [NotMapped]
        public int itemId 
        {
            get{

                if(this.MealID != null)
                {
                    return this.MealID.Value;
                }

                if(this.FoodItemID != null)
                {
                    return this.FoodItemID.Value;
                }

                return 0;

            }
        }



    }
    
}
