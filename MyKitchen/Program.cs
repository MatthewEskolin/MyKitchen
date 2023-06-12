using System.Configuration;

namespace MyKitchen;

[UsedImplicitly]
public class Program
{
    private static WebApplicationBuilder _builder;
    private static IWebHostEnvironment _env;


    public static async Task Main(string[] args)
    {
        //we should be able to run in PROD mode
        //Use to set to production
        //setx ASPNETCORE_ENVIRONMENT Production /M
        //to login az login --tenant ba6f651f-9fe1-4675-a765-278887f18618

        InitBuilder(args);

        InitEnvironment();

        AddKeyVault();

        AddDbContext();

        _builder.Services.AddScoped<UserInfo>();

        _builder.Services.AddScoped<IUserInfo>(UserFactory.GetUser);

        _builder.Services.AddTransient<IMyKitchenDataContext>(provider => provider.GetService<ApplicationDbContext>());

        _builder.Services.AddTransient<IFoodItemRepository, EFFoodItemRepository>();

        _builder.Services.AddTransient<IFoodReccomendationService, FoodRecommendationService>();

        _builder.Services.AddTransient<IMealRepository, EfMealRepository>();

        _builder.Services.AddTransient<IEmailSender, EmailSender>();

        _builder.Services.AddTransient<CalendarService>();

        _builder.Services.AddTransient<IMealImageService, AzureBlobMealImageService>();

        _builder.Services.AddScoped<IGroceryListService, GroceryListService>();



        //Add Singleton Services
        _builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        //Add Other Services
        //Add Identity Services
        _builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        _builder.Services.AddTransient<IMyKitchenDataService, MyKitchenDataService>();

        //-- Razor Pages
        AddRazorPages();

        //-- MVC Controllers
        _builder.Services.AddControllersWithViews().AddNewtonsoftJson();

        //-- Session Support
        AddSession();

        //-- Authentication and social logins
        AddAuthentication();

        //Configure Other Options
        ConfigureCookies();

        ConfigureIdentity();

        _builder.Services.AddExceptionless(_builder.Configuration["Exceptionless:ApiKey"].TraceErrorIfNullOrEmpty("Exceptionless.ApiKey"));

        //SeedDatabase();
        var app = _builder.Build();

        //Logging is now available!
        app.Logger.LogInformation("App is Built! Logging Ready!");

        app.UseExceptionless();

        if (_env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            //TODO Test this and see where it redirects! could be very helpful whey debuggin prod errors
            app.UseExceptionHandler("/Error");
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        app.UseStaticFiles();

        app.UseCookiePolicy();

        app.UseSession();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<EndpointLoggerMiddleware>();

        app.UseEndpoints(endPoints =>
        {

            //for attribute routing
            endPoints.MapControllers();

            endPoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=DisplayCurrentPrediction}/{id?}"
            );
            endPoints.MapControllerRoute(
                name: "index_default",
                pattern: "{controller}/{action=Index}");

            endPoints.MapControllerRoute(
                name: "foodItemDetail",
                pattern: "{controller=FoodItems}/{action=Details}/{id}"
            );

            endPoints.MapRazorPages();
        });

        app.Logger.LogCritical("Calling app.RunAsync");

        await app.RunAsync();


    }

    private static void AddAuthentication()
    {
        var auth = _builder.Services.AddAuthentication();

        if (_env.IsDevelopment())
        {
            //disable social logins in dev
        }
        else
        {
            var facebookAppId = _builder.Configuration["Authentication:Facebook:AppId"].TraceErrorIfNullOrEmpty("AppId");
            var facebookAppSecret = _builder.Configuration["Authentication:Facebook:AppSecret"].TraceErrorIfNullOrEmpty("AppSecret");
            var googleClientId = _builder.Configuration["Authentication:Google:ClientId"].TraceErrorIfNullOrEmpty("ClientID");
            var googleClientSecret = _builder.Configuration["Authentication:Google:ClientSecret"].TraceErrorIfNullOrEmpty("ClientSecret");

            auth.AddFacebook(options =>
                {
                    options.AppId = facebookAppId;
                    options.AppSecret = facebookAppSecret;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = googleClientId;
                    options.ClientSecret = googleClientSecret;
                });


        }
    }

    private static void AddSession()
    {
        _builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(100);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    private static void AddRazorPages()
    {
        var rp = _builder.Services.AddRazorPages();
        if (_env.IsDevelopment())
        {
            rp.AddRazorRuntimeCompilation();
        }
    }

    private static void ConfigureIdentity()
    {
        _builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedAccount = true;
            }
        );

    }


    private static void AddDbContext()
    {
        _builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            Trace.WriteLine($"ConnectionString={_builder.Configuration.GetConnectionString("DefaultConnection")}");
            options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection")!);
        });
    }

    private static void ConfigureCookies()
    {
        //add services here
        _builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
    }

    private static void AddKeyVault()
    {
        //configure the configuration to use a different Key Vault depending on whether we are in dev or prod
        var tokenProvider = new AzureServiceTokenProvider();


        // Get the access token
        //var token = tokenProvider.GetAccessTokenAsync("https://management.azure.com/").GetAwaiter().GetResult();


        //// Decode the access token
        //JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

        //// Get the username claim
        //var username = jwtToken.Claims.Where(c => c.Type == "unique_name").FirstOrDefault();


        //if (!string.IsNullOrEmpty(username))
        //{
        //    Console.WriteLine("Username: " + username);
        //}
        //else
        //{
        //    Console.WriteLine("Failed to retrieve the username from the access token.");
        //}



        var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));
        var keyUri = $"https://{_builder.Configuration["keyVaultName"]}.vault.azure.net/";


        try
        {
            _builder.Configuration.AddAzureKeyVault(keyUri, keyVaultClient, new DefaultKeyVaultSecretManager());
            Trace.WriteLine($"Key Vault Uri: {keyUri} successfully connected..");

        }
        catch (Exception ex)
        {
            Trace.WriteLine($"key vault failed to connect {ex} ");
            Environment.Exit(0);

        }


    }

    private static void InitEnvironment()
    {
        _env = _builder.Environment;
        Trace.WriteLine($"Environment = {_env.EnvironmentName}");
    }

    private static void InitBuilder(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

    }


    //private static void SeedDataBase()
    //{
    //    //using IServiceScope scope = Host.Services.CreateScope();
    //    //IServiceProvider services = scope.ServiceProvider;

    //    //try
    //    //{
    //    //    var context = services.GetRequiredService<MyKitchen.Data.ApplicationDbContext>();
    //    //    DbInitializer.Initialize(context);

    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    var logger = services.GetRequiredService<ILogger<Program>>();
    //    //    logger.LogError(ex, "An error occurred while seeding the database.");
    //    //}
    //}

}














