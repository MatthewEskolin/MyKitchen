using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MealMentor.Core.Data
{

    public class MealMentorDbContext : DbContext
    {
        private IConfiguration _config = null!;

        public MealMentorDbContext(DbContextOptions<MealMentorDbContext> options, IConfiguration config): base(options)
        {
            this._config = config;
        }

        public MealMentorDbContext(DbContextOptions<MealMentorDbContext> options):base(options)
        {
            //initialize with options
        }


        public MealMentorDbContext(){}

        public DbSet<Meal> Meals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

         
        }
    }

    public class Meal
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

        public Meal()
        {
            //  FoodItems = new List<FoodItem>();
        }


    }
}
