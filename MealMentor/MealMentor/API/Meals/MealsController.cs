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

        private readonly  MealMentorDbContext _dbContext; 
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
                mealID = m.MealID,
                name = m.MealName ?? "",
                Comments = m.Comments,
                IsFavorite = m.IsFavorite,
                IsQueued = m.IsQueued,
                Recipe = m.Recipe
            });

            return Ok(mealDtos);
        }


        //// GET: api/<MealsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<MealsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<MealsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<MealsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MealsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }




}
