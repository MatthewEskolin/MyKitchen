namespace MyKitchen.Data
{
    ///Used for Seeding Database, so only entities that need to be seeded need be included in this context. This allows us to register as a Transiet Scoped object, and DI will work before HttpRequest is received

    public class InitializeApplicationDbContext :DbContext
   {

        private IConfiguration Config;


            public InitializeApplicationDbContext(DbContextOptions<InitializeApplicationDbContext> options, IConfiguration cofnig): base(options)
        {
            this.Config = cofnig;

        }

        public InitializeApplicationDbContext(DbContextOptions<InitializeApplicationDbContext> options) { }


        public InitializeApplicationDbContext(){}


        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodGroup> FoodGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FoodItem>().HasOne(x => x.FoodGroup).WithMany(y => y.FoodItem);

        }


   }
}
