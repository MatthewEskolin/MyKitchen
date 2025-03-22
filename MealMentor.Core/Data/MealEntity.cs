using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MealMentor.Core.Data
{
    public class MealEntity
    {

        [Key] public int MealID { get; set; }
        public string? Comments { get; set; }

        //public ApplicationUser AppUser { get; set; }

        [Display(Name = "Meal Name")]
        [MaxLength(100)]
        public string? MealName { get; set; } 

        public bool IsFavorite { get; set; }

        public bool IsQueued { get; set; }

        public string? Recipe { get; set; } 
        //public ICollection<MealFoodItems> MealFoodItems { get; set; }

        public MealEntity()
        {
            //  FoodItems = new List<FoodItem>();
        }


    }
}
