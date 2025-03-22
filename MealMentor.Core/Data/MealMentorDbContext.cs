using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MealMentor.Core.Data;

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

    public DbSet<MealEntity> Meals { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

         
    }
}