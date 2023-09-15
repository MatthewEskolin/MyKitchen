
namespace MyKitchen;

[UsedImplicitly]
public class Program
{
    private static WebApplicationBuilder _builder;
    private static IWebHostEnvironment _env;
    private static WebApplication _app;

    public static async Task Main(string[] args)
    {
        _app = BuildApp(args);

        LogTester.TestLog(_app.Logger);

        ConfigureApp();
        SeedDataBase();
        await _app.RunAsync();
    }

    private static void ConfigureApp()
    {
        _app.UseExceptionless();

        if (_env.IsDevelopment())
        {
            _app.UseDeveloperExceptionPage();
        }
        else
        {
            //TODO Test this and see where it redirects! could be very helpful whey debuggin prod errors
            _app.UseExceptionHandler("/Error");
            _app.UseHttpsRedirection();
        }

        _app.UseRouting();

        _app.UseStaticFiles();

        _app.UseCookiePolicy();

        _app.UseSession();

        _app.UseAuthentication();

        _app.UseAuthorization();

        _app.UseMiddleware<EndpointLoggerMiddleware>();

        _app.UseEndpoints(endPoints =>
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

        _app.Logger.LogInformation("Calling app.RunAsync");
    }

    private static WebApplication BuildApp(string[] args)
    {
        InitBuilder(args);

        InitEnvironment();



        AddKeyVault();

        if (_env.IsDevelopment())
        {
            AddLocalConfig();
        }

        AddDbContext();

        _builder.Services.AddScoped<UserInfo>();

        _builder.Services.AddScoped<IUserInfo>(UserFactory.GetUser);

        _builder.Services.AddTransient<IMyKitchenDataContext>(provider => provider.GetService<ApplicationDbContext>());

        _builder.Services.AddTransient<IFoodItemRepository, EFFoodItemRepository>();

        _builder.Services.AddTransient<IFoodReccomendationService, FoodRecommendationService>();

        _builder.Services.AddTransient<IMealRepository, EfMealRepository>();

        _builder.Services.AddTransient<IEmailSender, EmailSender>();

        _builder.Services.AddTransient<CalendarService>();

        _builder.Services.AddTransient<DbInitializer>();

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

        //Logging
        _builder.Logging.AddProvider(new CustomDebugLoggerProvider());

        WebApplication app = _builder.Build();

        app.Logger.LogInformation("App is built, logging now available");

        return app;

    }

    private static void AddLocalConfig()
    {
        _builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);


        //DEBUG CONNECTION STRING
        Trace.WriteLine($"ConnectionString={_builder.Configuration.GetConnectionString("DefaultConnection")}");


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

        //resolve options of type InitializeApplicationDbContext, transiently..
        _builder.Services.AddTransient(provider =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<InitializeApplicationDbContext>();
            optionsBuilder.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection"));
            return optionsBuilder.Options;
        });


        //DEBUG CONNECTION STRING
        Trace.WriteLine($"ConnectionString={_builder.Configuration.GetConnectionString("DefaultConnection")}");

        _builder.Services.AddDbContext<InitializeApplicationDbContext>(options =>
        {
            Trace.WriteLine($"ConnectionString={_builder.Configuration.GetConnectionString("DefaultConnection")}");
            options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection")!);
        }, ServiceLifetime.Transient);

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
        var tokenProvider = new AzureServiceTokenProvider();


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

    private static void SeedDataBase()
    {
        try
        {
            _app.Logger.LogInformation("Initializing Database..");

            var dbInitializer = _app.Services.GetRequiredService<DbInitializer>();
            dbInitializer.Initialize();
        }
        catch (Exception ex)
        {
            _app.Logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }

}














