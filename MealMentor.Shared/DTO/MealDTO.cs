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
        public required int mealID { get; set; }
        public required string name { get; set; }
        public string? Comments { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsQueued { get; set; }
        public string? Recipe { get; set; }

    }
}
