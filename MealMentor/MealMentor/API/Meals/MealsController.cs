using MealMentor.Core.Data;
using MealMentor.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace MealMentor.API.Meals
{
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly MealMentorDbContext _dbContext;

        public MealsController(MealMentorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/getmeals")]
        [HttpGet]
        public ActionResult<IEnumerable<MealDTO>> Get()
        {
            List<Meal> meals = _dbContext.Meals.ToList();
            var mealDtos = meals.Select(m => new MealDTO
            {
                MealID = m.MealID,
                Name = m.MealName ?? "",
                Comments = m.Comments,
                IsFavorite = m.IsFavorite,
                IsQueued = m.IsQueued,
                Recipe = m.Recipe
            });

            return Ok(mealDtos);
        }

        [Route("api/meals")]
        [HttpPost]
        public ActionResult<MealDTO> CreateMeal(MealDTO meal)
        {
            var newMeal = new Meal
            {
                MealName = meal.Name,
                Comments = meal.Comments,
                IsFavorite = meal.IsFavorite,
                IsQueued = meal.IsQueued,
                Recipe = meal.Recipe
            };

            _dbContext.Meals.Add(newMeal);
            _dbContext.SaveChanges();
            meal.MealID = newMeal.MealID;

            return Ok(meal);
        }

        [Route("api/meals/{mealId}")]
        [HttpGet]
        public ActionResult<MealDTO> GetMeal(int mealId)
        {
            var meal = _dbContext.Meals.Find(mealId);
            if (meal == null)
            {
                return NotFound();
            }

            var mealDto = new MealDTO
            {
                MealID = meal.MealID,
                Name = meal.MealName ?? "",
                Comments = meal.Comments,
                IsFavorite = meal.IsFavorite,
                IsQueued = meal.IsQueued,
                Recipe = meal.Recipe
            };

            return Ok(mealDto);
        }

        [Route("api/meals/{mealId}/images")]
        [HttpGet]
        public ActionResult<IEnumerable<MealImageDTO>> GetImages(int mealId)
        {
            //var meal = _dbContext.Meals.Include(m => m.MealFoodItems).FirstOrDefault(m => m.MealID == mealId);
            //if (meal == null)
            //{
            //    return NotFound();
            //}

            //var mealImages = meal.MealFoodItems.Select(mfi => new MealImageDTO
            //{
            //    ImagePath = mfi.ImagePath
            //}).ToList();


            var images = new List<MealImageDTO>()
            {
                new(){ImagePath = string.Empty}
            };

            //give me some mealimagedtos with a path to any image in our site
            

            
            return Ok(images);

        }

        [Route("api/meals/{mealId}/name")]
        [HttpPut]
        public ActionResult UpdateMealName(int mealId, [FromBody] string mealName)
        {
            var meal = _dbContext.Meals.Find(mealId);
            if (meal == null)
            {
                return NotFound();
            }

            meal.MealName = mealName;
            _dbContext.SaveChanges();

            return NoContent();
        }

        [Route("api/meals/{mealId}")]
        [HttpPut]
        public ActionResult UpdateMeal(int mealId, MealDTO mealDto)
        {
            var meal = _dbContext.Meals.Find(mealId);
            if (meal == null)
            {
                return NotFound();
            }

            meal.MealName = mealDto.Name;
            meal.Comments = mealDto.Comments;
            meal.IsFavorite = mealDto.IsFavorite;
            meal.IsQueued = mealDto.IsQueued;
            meal.Recipe = mealDto.Recipe;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [Route("api/meals/{mealId}")]
        [HttpDelete]
        public ActionResult DeleteMeal(int mealId)
        {
            var meal = _dbContext.Meals.Find(mealId);
            if (meal == null)
            {
                return NotFound();
            }

            _dbContext.Meals.Remove(meal);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [Route("api/meals/{mealId}/fooditems/{mealFoodItemId}")]
        [HttpDelete]
        public ActionResult DeleteFoodItems(int mealId, int mealFoodItemId)
        {
            //var meal = _dbContext.Meals.Include(m => m.MealFoodItems).FirstOrDefault(m => m.MealID == mealId);
            //if (meal == null)
            //{
            //    return NotFound();
            //}

            //var foodItem = meal.MealFoodItems.FirstOrDefault(mfi => mfi.MealFoodItemID == mealFoodItemId);
            //if (foodItem == null)
            //{
            //    return NotFound();
            //}

            //meal.MealFoodItems.Remove(foodItem);
            //_dbContext.SaveChanges();

            return NoContent();
        }
    }




}
