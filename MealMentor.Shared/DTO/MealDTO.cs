using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMentor.Shared.DTO
{
    public class MealDTO
    {
        public int? MealID { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? Comments { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsQueued { get; set; }
        public string? Recipe { get; set; }

        public List<MealFoodItemDTO> MealFoodItems { get; set; } = new();

    }

    public class MealFoodItemDTO
    {
        public string Name { get; set; }
        public int MealFoodItemId { get; set; }
    }
}
